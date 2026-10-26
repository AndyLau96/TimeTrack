using OfficeOpenXml;
using TimeTrack.Models;
using System.Text.Json;

namespace TimeTrack
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            CheckAndCreateDesktopShortcut();
        }

        private void btnOpenDialog_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                txtFilePath.Text = filePath;
            }
        }

        private void CreateShortcutCore(string shortcutPath)
        {
            string targetPath = Environment.ProcessPath!;
            string powershellCommand = $"$WshShell = New-Object -ComObject WScript.Shell; " +
                                       $"$Shortcut = $WshShell.CreateShortcut('{shortcutPath}'); " +
                                       $"$Shortcut.TargetPath = '{targetPath}'; " +
                                       $"$Shortcut.Save()";

            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -WindowStyle Hidden -Command \"{powershellCommand}\"",
                CreateNoWindow = true,
                UseShellExecute = false
            };

            System.Diagnostics.Process.Start(psi);
        }

        private void CheckAndCreateDesktopShortcut()
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string shortcutPath = Path.Combine(desktopPath, "TimeTrack.lnk");

                string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TimeTrack");
                string flagFilePath = Path.Combine(appDataFolder, "shortcut_asked.flag");

                if (!Directory.Exists(appDataFolder))
                {
                    Directory.CreateDirectory(appDataFolder);
                }

                if (!File.Exists(flagFilePath))
                {
                    DialogResult dialogResult = MessageBox.Show(
                        "Do you want to create a shortcut on the Desktop for easy access?",
                        "Create Desktop Shortcut",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (dialogResult == DialogResult.Yes)
                    {
                        CreateShortcutCore(shortcutPath);
                        File.WriteAllText(flagFilePath, "YES");
                    }
                    else
                    {
                        File.WriteAllText(flagFilePath, "NO");
                    }
                }
                else if (File.ReadAllText(flagFilePath) == "YES" && !File.Exists(shortcutPath))
                {
                    CreateShortcutCore(shortcutPath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error creating shortcut: " + ex.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilePath.Text))
            {
                MessageBox.Show("Please select an Excel file before proceeding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtSavePath.Text))
            {
                MessageBox.Show("Please select a file save location before proceeding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string folderPath = txtSavePath.Text;
            string dateStr = DateTime.Now.ToString("yyyy-MM-dd");
            string finalSavePath = Path.Combine(folderPath, $"{dateStr}.xlsx");
            int counter = 1;
            while (File.Exists(finalSavePath))
            {
                finalSavePath = Path.Combine(folderPath, $"{dateStr}({counter}).xlsx");
                counter++;
            }
            try
            {
                var lstData = ReadExcelFile(txtFilePath.Text);
                if (lstData.Count == 0)
                {
                    MessageBox.Show("The selected Excel file is empty or has an invalid format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var result = ProcessAttendance(lstData);
                ExportToNewExcelFile(finalSavePath, result);
                MessageBox.Show($"Data exported successfully!\nSaved at: {finalSavePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private List<Dictionary<string, string>> ReadExcelFile(string filePath)
        {
            var dataList = new List<Dictionary<string, string>>();
            ExcelPackage.License.SetNonCommercialPersonal("LuuTuanHue");
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                if (worksheet.Dimension == null)
                {
                    return dataList;
                }
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;
                for (int row = 2; row <= rowCount; row++)
                {
                    var dataDict = new Dictionary<string, string>();
                    for (int col = 1; col <= colCount; col++)
                    {
                        string value = worksheet.Cells[row, col].Text.Trim();
                        switch (col)
                        {
                            case 1:
                                dataDict["EmployeeId"] = value;
                                break;
                            case 2:
                                dataDict["EmployeeName"] = value;
                                break;
                            case 3:
                                dataDict["Department"] = value;
                                break;
                            case 4:
                                dataDict["WorkingDate"] = value;
                                break;
                            case 5:
                                dataDict["DayOfWeek"] = value;
                                break;
                            default:
                                dataDict[$"Time{col}"] = value;
                                break;
                        }
                    }
                    dataList.Add(dataDict);
                }
            }
            return dataList;
        }

        private List<AttendanceResult> ProcessAttendance(List<Dictionary<string, string>> dataList)
        {
            var results = new List<AttendanceResult>();
            string appDataFolder = AppDomain.CurrentDomain.BaseDirectory;
            string jsonPath = Path.Combine(appDataFolder, "Rules.json");

            if (!File.Exists(jsonPath))
            {
                if (!Directory.Exists(appDataFolder)) Directory.CreateDirectory(appDataFolder);
                var defaultPolicy = new AttendancePolicy
                {
                    WorkStartTime = new TimeOnly(8, 0, 0),
                    WorkEndTime = new TimeOnly(17, 0, 0),
                    LunchTime = new TimeSpan(1, 0, 0),
                    RequiredWorkingHours = new TimeSpan(8, 0, 0)
                };
                File.WriteAllText(jsonPath, JsonSerializer.Serialize(defaultPolicy));
            }
            string json = File.ReadAllText(jsonPath);
            AttendancePolicy policy = JsonSerializer.Deserialize<AttendancePolicy>(json)!;

            foreach (var row in dataList)
            {
                TimeOnly? checkIn = null;
                TimeOnly? checkOut = null;
                var punchTimes = new List<TimeOnly>();
                foreach (var item in row)
                {
                    if (!item.Key.StartsWith("Time"))
                        continue;

                    if (TimeOnly.TryParse(item.Value, out var time))
                    {
                        punchTimes.Add(time);
                    }
                }

                if (punchTimes.Count > 0)
                {
                    checkIn = punchTimes.Min();
                    checkOut = punchTimes.Max();
                }
                var result = new AttendanceResult
                {
                    EmployeeId = row.ContainsKey("EmployeeId") ? row["EmployeeId"] : "",
                    EmployeeName = row.ContainsKey("EmployeeName") ? row["EmployeeName"] : "",
                    Department = row.ContainsKey("Department") ? row["Department"] : null,
                    WorkingDate = DateOnly.Parse(row["WorkingDate"]),
                    DayOfWeek = DateOnly.Parse(row["WorkingDate"]).DayOfWeek.ToString(),
                    CheckIn = checkIn,
                    CheckOut = checkOut,
                    IsPresent = punchTimes.Count > 0
                };

                result.WorkingHours = 0;
                result.IsQualified = false;
                if (result.DayOfWeek != "Saturday" && result.DayOfWeek != "Sunday")
                {
                    result.IsMissingPunch = punchTimes.Count > 0 && checkIn == checkOut;
                    if (checkIn.HasValue && checkIn.Value > policy.WorkStartTime)
                    {
                        result.IsLate = true;
                        TimeSpan lateMinutes = checkIn.Value - policy.WorkStartTime;
                        result.LateMinutes = lateMinutes.TotalMinutes;
                    }

                    if (checkOut.HasValue && checkOut.Value < policy.WorkEndTime)
                    {
                        result.IsEarlyLeave = true;
                        TimeSpan earlyMinutes = policy.WorkEndTime - checkOut.Value;
                        result.EarlyLeaveMinutes = earlyMinutes.TotalMinutes;
                    }

                    if (checkIn.HasValue && checkOut.HasValue && checkIn.Value != checkOut.Value)
                    {
                        TimeSpan presenceTime = checkOut.Value - checkIn.Value;
                        double calculatedHours = presenceTime.TotalHours - policy.LunchTime.TotalHours;
                        result.WorkingHours = Math.Max(0, Math.Round(calculatedHours, 2));
                        result.IsQualified = result.WorkingHours >= policy.RequiredWorkingHours.TotalHours;
                    }
                }

                results.Add(result);
            }
            return results;
        }

        private void btnWeightSetting_Click(object sender, EventArgs e)
        {
            AttendaceRules rulesForm = new AttendaceRules();
            rulesForm.ShowDialog();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string savePath = folderBrowserDialog1.SelectedPath;
                txtSavePath.Text = savePath;
            }
        }

        private void ExportToNewExcelFile(string savePath, List<AttendanceResult> results)
        {
            ExcelPackage.License.SetNonCommercialPersonal("LuuTuanHue");

            FileInfo newFile = new FileInfo(savePath);

            if (newFile.Exists)
            {
                newFile.Delete();
            }

            using (var package = new ExcelPackage(newFile))
            {
                var worksheet = package.Workbook.Worksheets.Add("Detail");
                string[] headers = {
                    "Employee ID", "Employee Name", "Department", "Work Date", "Day Of Week",
                    "Check In", "Check Out", "Is Present", "Is Late", "Late Minutes",
                    "Is Early Leave", "Early Leave Minutes", "Actual Working Hours", "Pass",
                    "Missing Punch"
                };

                for (int col = 1; col <= headers.Length; col++)
                {
                    worksheet.Cells[1, col].Value = headers[col - 1];
                    worksheet.Cells[1, col].Style.Font.Bold = true;

                    worksheet.Cells[1, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, col].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                int currentRow = 2;
                foreach (var res in results)
                {
                    worksheet.Cells[currentRow, 1].Value = res.EmployeeId;
                    worksheet.Cells[currentRow, 2].Value = res.EmployeeName;
                    worksheet.Cells[currentRow, 3].Value = res.Department;
                    worksheet.Cells[currentRow, 4].Value = res.WorkingDate.ToString("yyyy-MM-dd");
                    worksheet.Cells[currentRow, 5].Value = res.DayOfWeek;

                    worksheet.Cells[currentRow, 6].Value = res.CheckIn?.ToString("HH:mm:ss");
                    worksheet.Cells[currentRow, 7].Value = res.CheckOut?.ToString("HH:mm:ss");

                    worksheet.Cells[currentRow, 8].Value = res.IsPresent == true ? "Yes" : "No";
                    if (res.DayOfWeek != "Saturday" && res.DayOfWeek != "Sunday")
                    {
                        worksheet.Cells[currentRow, 9].Value = res.IsLate == true ? "X" : "";
                        worksheet.Cells[currentRow, 10].Value = res.LateMinutes;
                        worksheet.Cells[currentRow, 11].Value = res.IsEarlyLeave == true ? "X" : "";
                        worksheet.Cells[currentRow, 12].Value = res.EarlyLeaveMinutes;
                        worksheet.Cells[currentRow, 13].Value = res.WorkingHours;
                        worksheet.Cells[currentRow, 14].Value = res.IsQualified == true ? "Yes" : "No";
                        worksheet.Cells[currentRow, 15].Value = res.IsMissingPunch ? "Missed punch" : "";

                        if (res.IsLate == true)
                        {
                            worksheet.Cells[currentRow, 9, currentRow, 10].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[currentRow, 9, currentRow, 10].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Lavender);
                        }

                        if (res.IsEarlyLeave == true)
                        {
                            worksheet.Cells[currentRow, 11, currentRow, 12].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[currentRow, 11, currentRow, 12].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                        }

                        if (res.IsMissingPunch)
                        {
                            worksheet.Cells[currentRow, 1, currentRow, 15].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[currentRow, 1, currentRow, 15].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
                            worksheet.Cells[currentRow, 15].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                            worksheet.Cells[currentRow, 15].Style.Font.Bold = true;
                        }
                    }

                    currentRow++;
                }
                if (worksheet.Dimension != null) worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var wsOverview = package.Workbook.Worksheets.Add("Overview");
                string[] overviewHeaders = { "Employee ID", "Employee Name", "Department", "Late Count", "Late Minutes", "Early Leave Count", "Early Leave Minutes", "Missing Punch Count" };
                for (int col = 1; col <= overviewHeaders.Length; col++)
                {
                    wsOverview.Cells[1, col].Value = overviewHeaders[col - 1];
                    wsOverview.Cells[1, col].Style.Font.Bold = true;
                    wsOverview.Cells[1, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    wsOverview.Cells[1, col].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCyan);
                }

                var summaryData = results
                    .Where(r => r.DayOfWeek != "Saturday" && r.DayOfWeek != "Sunday")
                    .GroupBy(r => new { r.EmployeeId, r.EmployeeName, r.Department })
                    .Select(g => new
                    {
                        EmployeeId = g.Key.EmployeeId,
                        EmployeeName = g.Key.EmployeeName,
                        Department = g.Key.Department,
                        LateCount = g.Count(r => r.IsLate == true),
                        LateMinutes = g.Sum(r => r.LateMinutes ?? 0),
                        EarlyLeaveCount = g.Count(r => r.IsEarlyLeave == true),
                        EarlyLeaveMinutes = g.Sum(r => r.EarlyLeaveMinutes ?? 0),
                        MissingPunchCount = g.Count(r => r.IsMissingPunch)
                    }).ToList();
                int overviewRow = 2;
                foreach (var item in summaryData)
                {
                    wsOverview.Cells[overviewRow, 1].Value = item.EmployeeId;
                    wsOverview.Cells[overviewRow, 2].Value = item.EmployeeName;
                    wsOverview.Cells[overviewRow, 3].Value = item.Department;
                    wsOverview.Cells[overviewRow, 4].Value = item.LateCount;
                    wsOverview.Cells[overviewRow, 5].Value = item.LateMinutes;
                    wsOverview.Cells[overviewRow, 6].Value = item.EarlyLeaveCount;
                    wsOverview.Cells[overviewRow, 7].Value = item.EarlyLeaveMinutes;
                    wsOverview.Cells[overviewRow, 8].Value = item.MissingPunchCount;
                    overviewRow++;
                }
                if (wsOverview.Dimension != null) wsOverview.Cells[wsOverview.Dimension.Address].AutoFitColumns();
                package.Workbook.Worksheets.MoveToStart("Overview");
                package.Save();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeTrack.Models;

namespace TimeTrack
{
    public partial class AttendaceRules : Form
    {
        public AttendaceRules()
        {
            InitializeComponent();
        }

        private void btnCancelRules_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Attendance rules changes have been canceled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void AttendaceRules_Load(object sender, EventArgs e)
        {
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
            var policy = JsonSerializer.Deserialize<AttendancePolicy>(json);
            if (policy != null)
            {
                startTime.Value = DateTime.Today.Add(policy.WorkStartTime.ToTimeSpan());
                endTime.Value = DateTime.Today.Add(policy.WorkEndTime.ToTimeSpan());
                lunchTime.Value = DateTime.Today.Add(policy.LunchTime);
                requiredTime.Value = DateTime.Today.Add(policy.RequiredWorkingHours);
            }
        }

        private void btnConfirmRules_Click(object sender, EventArgs e)
        {
            try
            {
                AttendancePolicy policy = new AttendancePolicy();
                policy.WorkStartTime = TimeOnly.FromDateTime(startTime.Value);
                policy.WorkEndTime = TimeOnly.FromDateTime(endTime.Value);
                policy.LunchTime = lunchTime.Value.TimeOfDay;
                policy.RequiredWorkingHours = requiredTime.Value.TimeOfDay;
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                };
                string jsonString = JsonSerializer.Serialize(policy, options);
                string appDataFolder = AppDomain.CurrentDomain.BaseDirectory;
                string jsonPath = Path.Combine(appDataFolder, "Rules.json");
                File.WriteAllText(jsonPath, jsonString);
                this.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("An error occurred while saving the time :" + ex.Message);
            }
        }

        private void CalculateRequiredTime()
        {
            try
            {
                TimeSpan star = startTime.Value.TimeOfDay;
                TimeSpan end = endTime.Value.TimeOfDay;
                TimeSpan lunch = lunchTime.Value.TimeOfDay;
                TimeSpan required = (end - star) - lunch;
                if (required.TotalHours < 0)
                {
                    requiredTime.Value = DateTime.Today;
                    return;
                }
                requiredTime.Value = DateTime.Today.Add(required);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while calculating the time:" + ex.Message);
            }
        }

        private void TimeSettings_ValueChanged(object sender, EventArgs e)
        {
            CalculateRequiredTime();
        }
    }
}

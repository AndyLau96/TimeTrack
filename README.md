# ⏱️ TimeTrack - Attendance Time Calculator

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![Platform](https://img.shields.io/badge/Platform-Windows%20Forms-lightgrey.svg)]()
[![License](https://img.shields.io/badge/License-MIT-green.svg)]()

> A lightweight, intuitive desktop application created by **Andy** to automate employee attendance time calculations from Excel timesheets and generate formatted, comprehensive attendance reports.

---

## 📌 Features

- **Automated Timesheet Calculation:** Reads raw employee attendance records from Excel files and computes exact working hours.
- **Customizable Attendance Rules:**
  - Standard work hours (Start / End times).
  - Configurable lunch break duration.
  - Minimum required daily working hours.
  - Persistent settings stored in `Rules.json`.
- **Comprehensive 2-Sheet Excel Report:**
  - **Sheet 1 (Overview):** Summary per employee including total working hours, count of late arrivals, and count of early departures.
  - **Sheet 2 (Details):** Day-by-day logs with conditional visual highlighting for late arrivals and early departures.
- **Desktop Shortcut Helper:** Automatic convenience feature to create quick access shortcuts.

---

## 🛠️ Tech Stack

- **Language & Framework:** C# (.NET 8.0 Windows)
- **UI Framework:** Windows Forms (WinForms)
- **Excel Engine:** [EPPlus](https://epplussoftware.com/) (v8.6.0)

---

## 📋 Default Attendance Rules

| Setting | Default Value | Description |
| :--- | :--- | :--- |
| **Work Start** | `08:00 AM` | Standard shift start time |
| **Work End** | `05:00 PM` | Standard shift end time |
| **Lunch Time** | `1 hour` | Break duration deducted from calculations |
| **Required Time** | `8 hours` | Target working hours per shift |

---

## 🚀 How to Use

1. **Configure Rules:** Click the **Rules** button to adjust shift times, lunch break duration, and minimum required hours (or keep default settings).
2. **Select Attendance File:** Click **Select Path** to choose the raw Excel attendance file.
3. **Choose Output Location:** Click **Save As** to specify where the final calculated report will be saved.
4. **Process:** Click **Confirm** to generate the finished report.

---

## 💻 Build from Source

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Windows OS (Windows 10/11)

### Steps
```bash
# Clone the repository
git clone https://github.com/AndyLau96/TimeTrack.git
cd TimeTrack

# Build the project
dotnet build

# Run the application
dotnet run
```

---

## 👤 Author

Created with ❤️ by **Andy** ([@AndyLau96](https://github.com/AndyLau96))

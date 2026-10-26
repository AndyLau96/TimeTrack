using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrack.Models
{
    public class AttendanceResult
    {
        public string EmployeeId { get; set; } = "";
        public string EmployeeName { get; set; } = "";
        public string? Department {  get; set; }
        public DateOnly WorkingDate { get; set; }
        public string? DayOfWeek { get; set; }
        public TimeOnly? CheckIn { get; set; }
        public TimeOnly? CheckOut { get; set;}
        public bool? IsPresent { get; set; }
        public bool? IsQualified { get; set; }
        public bool? IsLate { get; set; }
        public double? LateMinutes { get; set; }
        public bool? IsEarlyLeave { get; set; }
        public double? EarlyLeaveMinutes { get; set; }
        public double? WorkingHours { get; set; }
        public bool IsMissingPunch { get; set; }
    }
}

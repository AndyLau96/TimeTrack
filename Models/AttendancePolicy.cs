using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrack.Models
{
    public class AttendancePolicy
    {
        public TimeOnly WorkStartTime { get; set; }
        public TimeOnly WorkEndTime { get; set; }
        public TimeSpan LunchTime { get; set; }
        public TimeSpan RequiredWorkingHours { get; set; }
    }
}

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Shared.Components
{
    public partial class TimePicker
    {
        private const string AM = "AM";
        private const string PM = "PM";

        private int HourPicked { get; set; }
        private int Minute { get; set; }
        private string AmOrPm { get; set; }

        public TimeSpan Time => new TimeSpan(AmOrPm == AM ? HourPicked : HourPicked + 12, Minute, 0);

    }
}

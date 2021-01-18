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

        [Parameter]
        public TimeSpan? Default { get; init; }

        private int HourPicked { get; set; } = 1;
        private int Minute { get; set; } = 0;
        private string AmOrPm { get; set; } = AM;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if(Default is TimeSpan time)
            {
                HourPicked = time.Hours % 12;
                Minute = time.Minutes;
                AmOrPm = time.Hours > 12 ? PM : AM;
            }
        }

        public TimeSpan Time => new TimeSpan(AmOrPm == AM ? HourPicked : HourPicked + 12, Minute, 0);

    }
}

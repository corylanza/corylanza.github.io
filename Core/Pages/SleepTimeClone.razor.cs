using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Shared.Components;

namespace Core.Pages
{
    public partial class SleepTimeClone
    {
        private TimePicker Time1;
        private TimePicker Time2;

        private readonly List<TimeSpan> Results = new List<TimeSpan>();

        private void Calculate()
        {
            Results.Clear();
            Results.Add(Time1.Time.Subtract(new TimeSpan(1, 30, 0)));
            Results.Add(Time1.Time.Subtract(new TimeSpan(3, 00, 0)));
            Results.Add(Time1.Time.Subtract(new TimeSpan(4, 30, 0)));
            Results.Add(Time1.Time.Subtract(new TimeSpan(6, 00, 0)));
        }

        private void WhenToWakeUpIfYouFallAsleepAt(TimeSpan date)
        {

        }
    }
}

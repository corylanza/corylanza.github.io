using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Core.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Core.Pages
{
    public partial class SleepTimeClone
    {
        [NotNull]
        private TimePicker? Time1 { get; set; }
        [NotNull]
        private TimePicker? Time2 { get; set; }

        private static IEnumerable<(TimeSpan time, string cssClass)> Intervals {
            get {
                yield return (new TimeSpan(1, 30, 0), "text-warning");
                yield return (new TimeSpan(3, 00, 0), "text-warning");
                yield return (new TimeSpan(4, 30, 0), "text-info");
                yield return (new TimeSpan(6, 00, 0), "text-info");
                yield return (new TimeSpan(7, 30, 0), "text-success");
                yield return (new TimeSpan(9, 00, 0), "text-success");
            }
        }

        private async Task SetResult(RenderFragment result)
        {
            Result = result;
            await __jsRuntime.ScrollToAsync(".results");
        }

        // Needed because TimeSpan string formatting isn't great,
        // instead use any DateTime
        private readonly DateTimeOffset aDate = new DateTime(2020, 01, 01);
        private string TwelveHourTime(TimeSpan ts) => aDate.Add(ts).ToString("hh:mm tt");

        private RenderFragment? Result { get; set; }
    }
}

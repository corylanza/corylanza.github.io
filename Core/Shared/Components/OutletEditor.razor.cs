using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Shared.Components
{
    public partial class OutletEditor
    {
        [Parameter]
        public string? ID { get; init; }

        [Parameter]
        public RenderFragment? ChildContent { get; set;}

        public async Task<string> GetValue() => await JSRuntime.InvokeAsync<string>("getCodeEditorValue", new object[] { ID ?? "editor" });

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeAsync<string>("highlightCode", new object[] { ID ?? "editor" });
            }
        }
    }
}

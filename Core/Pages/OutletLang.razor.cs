using Core.Shared.Components;
using Outlet;
using Outlet.ForeignFunctions;
using Outlet.Operands;
using Outlet.StandardLib;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Pages
{
    public partial class OutletLang
    {
        [NotNull]
        // value set in razor component, will not be null
        private OutletEditor? Editor { get; set; }

        private string? CurrentExample { get; set; }
        private string? Output { get; set; }
        private string? ReturnValue { get; set; }
        private bool ShowOutput { get; set; }

        private const string success = "text-success";
        private const string failure = "text-danger";

        private string CheckCssClass = "";

        private ReplOutletProgram Program { get; set; }

        public OutletLang()
        {
            var browserInterface = new SystemInterface(
                stdin: GetInput,
                stdout: StdOut,
                stderr: StdError
            );
            Program = new ReplOutletProgram(browserInterface);
        }

        protected List<string> Errors { get; set; } = new List<string>();

        public async Task RunOutlet()
        {
            var input = await Editor.GetValue();
            Errors.Clear();
            Output = string.Empty;
            var result = Program.Run(Encoding.ASCII.GetBytes(input));
            if(!Errors.Any())
            {
                var outputString = result?.ToString();
                ReturnValue = $"Returned: {(string.IsNullOrWhiteSpace(outputString) ? "void" : outputString)}";
                ShowOutput = true;
            }
        }

        public async Task CheckOutlet()
        {
            var input = await Editor.GetValue();
            Errors.Clear();
            Program.Check(Encoding.ASCII.GetBytes(input));

            CheckCssClass = Errors.Count > 0 ? failure : success;
        }

        public void StdError(Exception ex)
        {
            Errors.Add($"{ex.GetType().FullName}:\n{ex.Message}\n{ex.StackTrace}");
        }

        public void StdOut(string output)
        {
            Output += $"StdOut: {output}";
        }

        public static string GetInput() => "";
    }
}

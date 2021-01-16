﻿using Core.Shared.Components;
using Outlet;
using Outlet.Operands;
using Outlet.StandardLib;
using Outlet.TreeViewer;
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

        public List<(string code, List<string> consoleOutput, Operand? result)> History = new List<(string, List<string>, Operand?)>();

        private const string success = "text-success";
        private const string failure = "text-danger";

        private string CheckCssClass = "";

        private ReplOutletProgram Program { get; set; }

        //private Node? AST { get; set; }

        public OutletLang()
        {
            var browserInterface = new SystemInterface(
                stdin: GetInput,
                stdout: ShowOutput,
                stderr: ShowError
            );
            Program = new ReplOutletProgram(browserInterface);
        }

        protected List<string> Errors { get; set; } = new List<string>();

        public async Task RunOutlet()
        {
            var input = await Editor.GetValue();
            Errors.Clear();
            var result = Program.Run(Encoding.ASCII.GetBytes(input));
            if(!Errors.Any())
            {
                History.Add((input, new List<string>(), null));
                var last = History.Last();
                last.result = result;
                History.RemoveAt(History.Count - 1);
                History.Add(last);
            }
        }

        public async Task CheckOutlet()
        {
            var input = await Editor.GetValue();
            Errors.Clear();
            Program.Check(Encoding.ASCII.GetBytes(input));

            CheckCssClass = Errors.Count > 0 ? failure : success;
        }

        public void ShowError(Exception ex)
        {
            Errors.Add(ex.Message);
        }

        public void ShowOutput(string output)
        {
            var (code, consoleOutput, result) = History.Last();
            consoleOutput.Add(output);
        }

        public string GetInput()
        {
            return "";
        }

        //public void ShowAST()
        //{
        //    AST = Program.GenerateAST();
        //}
    }
}

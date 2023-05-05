using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexAnalyzer.Models
{
    public class RegSyntax
    {
        public string? RegexSyntax { get; set; }
        public string? Output { get; set; }
        public RegSyntax(string regsyntax, string output)
        {
            RegexSyntax = regsyntax;
            Output = output;

        }


    }
}

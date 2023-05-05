using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.X11;
using ReactiveUI;
using RegexAnalyzer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexAnalyzer.ViewModels
{
    public class RegexAnalyzerViewModel : ViewModelBase
    {

        private string? _regexSyntax;
        private string? _regexOutput;
        public ObservableCollection<RegSyntax> _syntaxList = new ObservableCollection<RegSyntax>();
        private string[] arithmath = new string[] { "+", "-", "/", "*", "%" };
        int[] EF = new int[] { 0, 0, 0, 0, 1, 0, 0, 1, 0,0 };
        
        public FlatTreeDataGridSource<RegSyntax> Source { get; } //This allow View to retrive info from Source

        public RegexAnalyzerViewModel()
        {
            Source = new FlatTreeDataGridSource<RegSyntax>(_syntaxList) //Create a new FlatTreeDataGridSource that we'll use to set data of FlatTreeDataGrid
            {
                Columns =
                {
                    new TextColumn<RegSyntax, string>("Saida", x => x.RegexSyntax,new GridLength(6, GridUnitType.Star)), //This will create the column and set the data that will be used to fill this field, in this case x.RegexSyntax

                    new TextColumn<RegSyntax, string>("Resultado", x => x.Output, new GridLength(4, GridUnitType.Star)), //This will create the column and set the data that will be used to fill this field, in this case x.Output
                },

            };
        }


        public string? RegexSyntax
        {
            get => _regexSyntax ??= null;
            set
            {
                this.RaiseAndSetIfChanged(ref _regexSyntax, value); //When something is type on field this is called and update reference _regexSyntax

            }
        }

        public void SyntaxAnalyzer()
        {

            try
            {
                RegSyntax novousuario = new RegSyntax("ababcdede", "Sentença Valida");
                RegSyntax novousuario1 = new RegSyntax("+", "Operador Aritmético");
                RegSyntax novousuario2 = new RegSyntax("ccc", "Sentença Valida");
                RegSyntax novousuario3 = new RegSyntax("ababababde", "Sentença Valida");

                _syntaxList.Add(novousuario);
                _syntaxList.Add(novousuario1);
                _syntaxList.Add(novousuario2);
                _syntaxList.Add(novousuario3);//This will add new RegSyntax object to _syntaxList.
            }
            catch (Exception e)
            {
                Console.Write("Implementar");

            }



        }

        

    }
}


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
        public ObservableCollection<RegSyntax> _syntaxList = new ObservableCollection<RegSyntax>();
        private char[] arithmath = new char[] { '+', '-', '/', '*'};
        private int[] _ef = new int[] { 0, 0, 0, 0, 1, 0, 0, 1, 0, 0};
        private int[,] _stateMachine = new int[,] { 
            // A B C D E
            {1, -1, 4 , 6, -1}, //E0
            {-1, 2, -1, -1, -1}, //E1
            {3, -1, -1, -1, -1}, //E2
            {-1, 0, -1, -1, -1}, //E3
            {-1, -1, 5, 8, -1}, //E4 Estado final
            {-1, -1, 4, 6, -1}, //E5
            {-1, -1, -1, -1, 7}, //E6
            {-1, -1, -1, 8, -1}, //E7 Estado final
            {-1, -1, -1, -1, 9}, //E8
            {-1, -1, -1, 6, -1} //E9
        }; // Tabela de transição para a linguagem L

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

        private void CheckSyntax(bool isControlChar = false)
        {

        }
        public void SyntaxAnalyzer()
        {

            try
            {
                int currentIndex = 0;
                int state = 0;
                char symbol = _regexSyntax[currentIndex];
                string currentSyntax = "";
                bool invalidSyntax = false;
                bool invalidSymbol = false;
                while (currentIndex < _regexSyntax.Length)
                {
                    int column;
                    if (symbol == 'a')
                    {
                        column = 0;
                    }
                    else if (symbol == 'b')
                    {
                        column = 1;
                    }
                    else if (symbol == 'c')
                    {
                        column = 2;
                    }
                    else if (symbol == 'd')
                    {
                        column = 3;
                    }
                    else if (symbol == 'e')
                    {
                        column = 4;
                    }
                    else
                    {
                        column = -1;
                    }

                    if (column == -1)
                    {
                        if (char.IsControl(symbol) || arithmath.Contains(symbol) || char.IsWhiteSpace(symbol))
                        {
                            if (invalidSyntax || invalidSymbol)
                            {
                                if (invalidSyntax)
                                {
                                    RegSyntax syntax = new RegSyntax(currentSyntax, "ERRO – Sentença Inválida");
                                    _syntaxList.Add(syntax);
                                }
                                else
                                {
                                    RegSyntax syntax = new RegSyntax(currentSyntax, "ERRO – Símbolo(s) Inválido(s):");
                                    _syntaxList.Add(syntax);
                                }
                                currentSyntax = "";
                                invalidSymbol = false;
                                invalidSyntax = false;
                            }
                            else
                            {
                                if (!String.IsNullOrEmpty(currentSyntax))
                                {
                                    if (_ef[state] == 1)
                                    {
                                        RegSyntax syntax = new RegSyntax(currentSyntax, "Sentença Válida.");
                                        _syntaxList.Add(syntax);
                                    }
                                    else
                                    {
                                        RegSyntax syntax = new RegSyntax(currentSyntax, "ERRO – Sentença Inválida");
                                        _syntaxList.Add(syntax);
                                    }
                                }
                                currentSyntax = "";
                                invalidSymbol = false;
                                invalidSyntax = false;
                            }
                            if (arithmath.Contains(symbol))
                            {
                                RegSyntax syntax = new RegSyntax(symbol.ToString(), "Operador Aritmético.");
                                _syntaxList.Add(syntax);
                                currentSyntax = "";
                                invalidSymbol = false;
                                invalidSyntax = false;
                            }

                        }
                        else
                        {
                            currentSyntax += symbol.ToString();
                            if (currentSyntax.Length <= 1 && !invalidSyntax)
                            {
                                invalidSymbol = true;
                            }
                            else
                            {

                                invalidSyntax = true;
                            }
                        }
                    }
                    else
                    {
                        currentSyntax += symbol.ToString();
                        if (!invalidSyntax)
                        {
                            state = _stateMachine[state, column];
                            if(state == -1)
                            {
                                invalidSyntax= true;
                            }
                        }
                    }



                    if (currentIndex + 1 == _regexSyntax.Length)
                    {

                        if(state == -1)
                        {
                            if (invalidSymbol)
                            {
                                RegSyntax syntax = new RegSyntax(currentSyntax, "ERRO – Símbolo(s) Inválido(s):");
                                _syntaxList.Add(syntax);
                            }
                            else
                            {
                                RegSyntax syntax = new RegSyntax(currentSyntax, "ERRO – Sentença Inválida.");
                                _syntaxList.Add(syntax);
                            }
                        }
                        else if (_ef[state] == 1 & !invalidSyntax & !invalidSymbol)
                        {
                            RegSyntax syntax = new RegSyntax(currentSyntax, "Sentença Válida.");
                            _syntaxList.Add(syntax);
                        }
                        else
                        {
                            if (invalidSymbol)
                            {
                                RegSyntax syntax = new RegSyntax(currentSyntax, "ERRO – Símbolo(s) Inválido(s):");
                                _syntaxList.Add(syntax);
                            }
                            else
                            {
                                RegSyntax syntax = new RegSyntax(currentSyntax, "ERRO – Sentença Inválida.");
                                _syntaxList.Add(syntax);
                            }
                        }
                        currentIndex++;
                    }
                    else
                    {
                        symbol = _regexSyntax[currentIndex + 1];
                        currentIndex++;
                    }






                    //if (column == -1)
                    //{

                    //    if (char.IsControl(symbol) || arithmath.Contains(symbol) || char.IsWhiteSpace(symbol))
                    //    {
                    //        if (invalidSyntax || invalidSymbol)
                    //        {
                    //            if (invalidSyntax)
                    //            {
                    //                RegSyntax syntax = new RegSyntax(currentSyntax, "ERRO – Sentença Inválida");
                    //                _syntaxList.Add(syntax);
                    //            }
                    //            else
                    //            {
                    //                RegSyntax syntax = new RegSyntax(currentSyntax, "ERRO – Símbolo(s) Inválido(s):");
                    //                _syntaxList.Add(syntax);
                    //            }
                    //            currentSyntax = "";
                    //            invalidSymbol = false;
                    //            invalidSyntax = false;
                    //        }
                    //        else
                    //        {
                    //            if (!String.IsNullOrEmpty(currentSyntax))
                    //            {
                    //                if(_ef[state] == 1)
                    //                {
                    //                    RegSyntax syntax = new RegSyntax(currentSyntax, "Sentença Válida.");
                    //                    _syntaxList.Add(syntax);
                    //                }
                    //                else
                    //                {
                    //                    RegSyntax syntax = new RegSyntax(currentSyntax, "ERRO – Sentença Inválida");
                    //                    _syntaxList.Add(syntax);
                    //                }
                    //            }
                    //            currentSyntax = "";
                    //            invalidSymbol = false;
                    //            invalidSyntax = false;
                    //        }
                    //        if (arithmath.Contains(symbol))
                    //        {
                    //            RegSyntax syntax = new RegSyntax(symbol.ToString(), "Operador Aritmético.");
                    //            _syntaxList.Add(syntax);
                    //            currentSyntax = "";
                    //            invalidSymbol = false;
                    //            invalidSyntax= false;
                    //        }

                    //    }
                    //    else
                    //    {
                    //        currentSyntax += symbol.ToString();
                    //        if (currentSyntax.Length <= 1 && !invalidSyntax)
                    //        {
                    //            invalidSymbol = true;
                    //        }
                    //        else
                    //        {

                    //            invalidSyntax = true;
                    //        }
                    //    }

                    //}
                    //else
                    //{
                    //    currentSyntax += symbol.ToString();
                    //}







                    //if (column != -1)
                    //{
                    //    currentSyntax += symbol.ToString();
                    //}
                    //state = _stateMachine[state, column];
                    //symbol = _regexSyntax[currentIndex + 1];
                    //currentIndex++;

                }

                // FIM DO WHILE


            }
            catch (Exception e)
            {
                

            }



        }

        

    }
}


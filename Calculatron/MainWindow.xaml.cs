using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NC = NCalc;

namespace Calculatron
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ExpressionBuilder _builder;

        public MainWindow()
        {
            InitializeComponent();
            _builder = new ExpressionBuilder();
        }

        private void btn_Click_Chiffre(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            lbl_affichage.Content = _builder.AddFigure((string)btn.Content);
        }

        private void btn_Click_Symbole(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if ((string)btn.Content == WebUtility.HtmlDecode("&#8592;"))
                lbl_affichage.Content = _builder.EraseLastCharacter();
            else
                lbl_affichage.Content = _builder.AddSymbole((string)btn.Content);
        }

        private void btn_Click_Egal(object sender, RoutedEventArgs e)
        {
            try
            {
                lbl_affichage.Content = _builder.EvaluateExpression();
            }
            catch (Exception)
            {
                lbl_affichage.Content = "";
                MessageBox.Show("Expression mal formée");
            }
        }

        /// <summary>
        /// Supprime l'affichage en cours
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Click_Clear(object sender, RoutedEventArgs e)
        {
            _builder.Clear();
            lbl_affichage.Content = "";
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                string tempExpr = "";

                if (Regex.IsMatch(e.Key.ToString(), @"NumPad\d"))
                {
                    tempExpr = _builder.AddFigure(e.Key.ToString().Last().ToString());
                }
                else
                {
                    switch (e.Key.ToString())
                    {
                        case "Decimal":
                            tempExpr = _builder.AddFigure(".");
                            break;
                        case "Divide":
                            tempExpr = _builder.AddSymbole("/");
                            break;
                        case "Multiply":
                            tempExpr = _builder.AddSymbole("*");
                            break;
                        case "Add":
                            tempExpr = _builder.AddSymbole("+");
                            break;
                        case "Subtract":
                            tempExpr = _builder.AddSymbole("-");
                            break;
                        case "Return": 
                            try
                            {
                                tempExpr = _builder.EvaluateExpression();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            break;
                        case "D5":
                            tempExpr = _builder.AddSymbole("(");
                            break;
                        case "OemOpenBrackets":
                            tempExpr = _builder.AddSymbole(")");
                            break;
                        case "Back":
                            tempExpr = _builder.EraseLastCharacter();
                            break;
                        default:
                            break;
                    }
                }

                lbl_affichage.Content = tempExpr;
            }
            catch (Exception ex)
            {
                lbl_affichage.Content = "";
                MessageBox.Show(ex.Message);
            }
        }
    }
}

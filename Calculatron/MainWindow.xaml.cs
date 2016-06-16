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
        private bool isResult;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Click_Chiffre(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (isResult)
            {
                lbl_affichage.Content = "";
                isResult = false;
            }

            AddFigure((string)btn.Content);
        }

        /// <summary>
        /// Méthode d'ajout de chiffre en fin de chaine dans l'afficheur
        /// </summary>
        /// <param name="figure">Chiffre à ajouter</param>
        private void AddFigure(string figure)
        {
            if (figure == ".")
            {
                if (!Regex.IsMatch((string)lbl_affichage.Content + ".", @"\d*\.\d*\."))
                    lbl_affichage.Content = (string)lbl_affichage.Content + figure;
            }
            else
            {
                lbl_affichage.Content = (string)lbl_affichage.Content + figure;
            }
        }

        /// <summary>
        /// Méthode d'ajout de symbole en fin de chaine dans l'afficheur
        /// </summary>
        /// <param name="symbole">Symbole à ajouter</param>
        private void AddSymbole(string symbole)
        {
            isResult = false;
            if (!Regex.IsMatch((string)lbl_affichage.Content, @"[\/+*\-]$"))
            {
                lbl_affichage.Content = (string)lbl_affichage.Content + symbole;
            }
        }

        private void btn_Click_Symbole(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if ((string)btn.Content == WebUtility.HtmlDecode("&#8592;"))
                EraseLastCharacter();
            else
                AddSymbole((string)btn.Content);
        }

        private void EraseLastCharacter()
        {
            string print = lbl_affichage.Content.ToString();
            if (print.Length != 0)
                lbl_affichage.Content = print.Remove(print.Length - 1);
        }

        private void btn_Click_Egal(object sender, RoutedEventArgs e)
        {
            EvaluateExpression();
        }

        private void EvaluateExpression()
        {
            NC.Expression expr = new NC.Expression((string)lbl_affichage.Content);
            try
            {
                object res = expr.Evaluate();
                lbl_affichage.Content = res.ToString();
                isResult = true;
            }
            catch (NC.EvaluationException evalEx)
            {
                MessageBox.Show("Erreur d'interprétation dans le calcul.\n\nMessage d'erreur : \n\t-" + evalEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur générale.\n\nMessage d'erreur : \n\t-" + ex.Message);
            }
        }

        /// <summary>
        /// Supprime l'affichage en cours
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Click_Clear(object sender, RoutedEventArgs e)
        {
            lbl_affichage.Content = "";
            isResult = false;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Regex.IsMatch(e.Key.ToString(), @"NumPad\d"))
                {
                    AddFigure(e.Key.ToString().Last().ToString());
                }
                switch (e.Key.ToString())
                {
                    case "Decimal":
                        AddFigure(".");
                        break;
                    case "Divide":
                        AddSymbole("/");
                        break;
                    case "Multiply":
                        AddSymbole("*");
                        break;
                    case "Add":
                        AddSymbole("+");
                        break;
                    case "Subtract":
                        AddSymbole("-");
                        break;
                    case "Return":
                        EvaluateExpression();
                        break;
                    //TODO : Appuyer sur 'Retour arrière' pour supprimer
                    default:
                        break;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}

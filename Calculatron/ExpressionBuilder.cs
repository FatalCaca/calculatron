using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculatron
{
    public class ExpressionBuilder
    {
        private const string DECIMAL_SEPARATOR = ",";
        private const string END_SYMBOL_REGEX = @"[%\/+*\-\^]$";

        private bool _isResult;
        private Regex _powRegex;
        private Regex _squareRegex;
        private string _expression;
        private int _nbOpenedParenthesis;

        /// <summary>
        /// Expression régulière permettant de détecter la présence d'un calcul 
        /// de type x^y afin de le remplacer par Pow(x,y)
        /// pour que NCalc soit capable de l'interpréter
        /// </summary>
        private string POW_REGEX = string.Format(@"(\d*({0}\d+)?)\^(\d*({0}\d+)?)", DECIMAL_SEPARATOR);

        /// <summary>
        /// Expression régulière détectant la présence d'un nombre élevé au carré
        /// </summary>
        private string SQUARE_REGEX = string.Format(@"(\d*({0}\d+)?)\^(²)", DECIMAL_SEPARATOR);

        /// <summary>
        /// Expression régulière permettant de détecter un nombre décimal afin de ne pas
        /// placer deux virgules pour le même nombre
        /// </summary>
        private string DOT_REGEX = string.Format(@"\d*{0}\d*{0}", DECIMAL_SEPARATOR);

        /// <summary>
        /// Expression régulière permettant de ne pas écrire plusieurs 0 d'affilée en début de nombre
        /// </summary>
        private string DOUBLE_ZERO_REGEX = string.Format(@"([^\d^{0}]0$)|(^0$)", DECIMAL_SEPARATOR);

        public ExpressionBuilder()
        {
            Clear();
            _powRegex = new Regex(POW_REGEX);
            _squareRegex = new Regex(SQUARE_REGEX);
        }

        /// <summary>
        /// Méthode d'ajout de chiffre en fin d'expression
        /// </summary>
        /// <param name="figure">Chiffre à ajouter</param>
        public string AddFigure(string figure)
        {
            if (_isResult)
            {
                Clear();
            }

            if (figure == ",")
            {
                if (!Regex.IsMatch(_expression + ".", DOT_REGEX))
                    _expression += figure;
            }
            else
            {
                if (!Regex.IsMatch(_expression, DOUBLE_ZERO_REGEX))
                    _expression += figure;
            }

            return _expression;
        }

        /// <summary>
        /// Méthode d'ajout de symbole en fin d'expression
        /// </summary>
        /// <param name="symbole">Symbole à ajouter</param>
        public string AddSymbole(string symbole)
        {
            _isResult = false;

            if (symbole == "(" || symbole == ")")
            {
                switch (symbole)
                {
                    case "(":
                        _nbOpenedParenthesis++;
                        _expression += symbole;
                        break;
                    case ")":
                        if (_nbOpenedParenthesis > 0 && !Regex.IsMatch(_expression, END_SYMBOL_REGEX))
                        {
                            _nbOpenedParenthesis--;
                            _expression += symbole;
                        }
                        break;
                    default:
                        break;
                }
            }
            else if (!Regex.IsMatch(_expression, END_SYMBOL_REGEX))
            {
                _expression += symbole;
            }

            return _expression;
        }

        /// <summary>
        /// Supprime le dernier caractère d'une expression
        /// </summary>
        public string EraseLastCharacter()
        {
            try
            {
                if (_expression.Length != 0)
                {
                    char last = _expression.Last();

                    if (last == '(')
                        _nbOpenedParenthesis--;
                    else if (last == ')')
                        _nbOpenedParenthesis++;

                    _expression = _expression.Remove(_expression.Length - 1);
                }

            }
            catch (Exception)
            {

            }

            return _expression;
        }

        /// <summary>
        /// Evalue l'expression à l'aide de la librairie NCalc
        /// </summary>
        public string EvaluateExpression()
        {

            if (!Regex.IsMatch(_expression, END_SYMBOL_REGEX))
            {
                try
                {
                    string unevalExpr = SanitizeExpr(_expression);
                    Expression expr = new Expression(unevalExpr);
                    object res = expr.Evaluate();
                    _expression = res.ToString();
                }
                catch (EvaluationException evalEx)
                {
                    throw evalEx;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _isResult = true;
                }
            }

            return _expression;
        }

        /// <summary>
        /// Remet à 0 le compteur de parenthèse ouvertes, et le contenu de l'expression
        /// </summary>
        public void Clear()
        {
            _isResult = false;
            _expression = "";
            _nbOpenedParenthesis = 0;
        }

        /// <summary>
        /// Nettoie l'expression pour que NCalc puisse l'interpréter
        /// </summary>
        /// <param name="labelContent"></param>
        /// <returns></returns>
        private string SanitizeExpr(string labelContent)
        {
            string res = _powRegex.Replace(labelContent, "Pow($1, $3)");
            res = _squareRegex.Replace(labelContent, "Pow($1, 2)");
            res = res.Replace(',', '.');
            return res;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculatron
{
    public class BaseCalculator
    {
        /// Return the result of an addition between 2 numbers
        public float Addition(float valeurA, float valeurB)
        {
            return valeurA + valeurB;
        }

        /// Return the result of a substraction between 2 numbers
        public float Soustraction(float valeurA, float valeurB)
        {
            return valeurA - valeurB;
        }

        /// Returns the result of a division between 2 numbers
        public float Division(float valeurA, float valeurB)
        {
            float result;

            if(valeurB != 0)
            {
                result = valeurA / valeurB;
            }
            else
            {
                result = -1;
            }
            
            return result;
        }

        /// Returns the product of 2 numbers
        public float Multiplication(float valeurA, float valeurB)
        {
            return valeurA * valeurB;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculatron
{
    public class BaseCalculator
    {
        public float Addition(float valeurA, float valeurB)
        {
            return valeurA + valeurB;
        }

        public float Soustraction(float valeurA, float valeurB)
        {
            return valeurA - valeurB;
        }

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

        public float Multiplication(float valeurA, float valeurB)
        {
            return valeurA * valeurB;
        }
    }
}

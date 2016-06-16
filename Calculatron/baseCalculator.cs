using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculatron
{
    public class baseCalculator
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
            float value;
            if(valeurB != 0)
            {
                value = valeurA / valeurB;
            }
            else
            {
                value = -1;
            }
            
            return value;
        }

        public float Multiplication(float valeurA, float valeurB)
        {
            return valeurA * valeurB;
        }
    }
}

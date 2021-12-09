using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAY
{
    class Equation
    {
        public List<(int, List<(string, double)>)> EquationLeftPart { get; private set; }
        public List<double> EquationRightPart { get; private set; }


        public Equation(List<(int,List<(string, double)>)> keyValuePairs, List<double> valuesOfLine)
        {
            EquationRightPart = valuesOfLine;
            EquationLeftPart = keyValuePairs;
        }

        public Equation()
        {
            EquationLeftPart = new();
            EquationRightPart = new();
        }


        public bool CheckEquation()
        {
            if(EquationLeftPart.Count != EquationRightPart.Count)
            {
                return false;
            }
            foreach(var elem in EquationLeftPart)
            {
                if(elem.Item2.Count> EquationRightPart.Count)
                {
                    return false;
                }
            }
            return true;
        }


        public List<(double,double)> CalculateAlphaAndBeta()
        {
            List<(double,double)> result = new();
            List<double> coofC = new();
            EquationLeftPart.ForEach(
                x => {
               coofC.Add( (-1)*x.Item2.Skip(1).First().Item2);
            }
            );
            double alpha = EquationLeftPart[0].Item2[1].Item2 /((-1)* EquationLeftPart[0].Item2[0].Item2);
            double beta = - EquationRightPart[0]/ coofC[0];
            for(int i = 1; i < EquationRightPart.Count; i++)
            {
                result.Add((alpha, beta));
                var temp = alpha;
                alpha = EquationLeftPart[i].Item2.Last().Item2/
                    (coofC[i] - EquationLeftPart[i].Item2.First().Item2 * alpha);
                beta = (EquationLeftPart[i].Item2.First().Item2 * beta - EquationRightPart[i])
                    /
                    (coofC[i] - EquationLeftPart[i].Item2.First().Item2 * temp);
                //alpha = temp;
            }
            result.Add((alpha, beta));
            return result;
        }

        public List<double> CalculateValues()
        {
            var l = CalculateAlphaAndBeta();
            List<double> result = new();
            result.Add(l.Last().Item2);
            for(int i = l.Count-2 ; i>=0; i--)
            {
                result.Add(l[i].Item1 * result.Last() + l[i].Item2);
            }
            result.Reverse();
            return result;
        }



    }
}

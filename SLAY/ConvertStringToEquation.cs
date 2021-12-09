using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SLAY
{
    class ConvertStringToEquation
    {

        private Regex regexForValue;
        private Regex regexForKey;
        private Regex regexForSplit;

        public ConvertStringToEquation()
        {
             regexForValue = new Regex(@"(\-)?\d+(,\d{1,2})?");
             regexForKey = new Regex(@"[A-Za-z]+[0-9]*");
             regexForSplit = new Regex(@"((\-)?\d+(,\d{1,2})?)([a-z]+)(\d+)");
        }

        public Equation ConvertationEquation(string equation)
        {
            var temp = SplitString(equation);
            var rightPart = GetValueOfLine(temp);
            List<(int, List<(string, double)>)> leftPart = new();
            for(int i = 0; i < temp.Length; i++)
            {
                var list = GetMemberInEquation(temp[i]);
                leftPart.Add((i, list));
            }
            return new Equation(leftPart, rightPart);
        }

        private List<(string, double)> GetMemberInEquation(string line)
        {
            List<(string, double)> result = new ();
            MatchCollection stringSplitInLine = regexForSplit.Matches(line);
            foreach (Match member in stringSplitInLine)
            {
                string key = regexForKey.Match(member.Value).Value;
                double value = double.Parse(regexForValue.Match(member.Value).Value);
                if (result.Any(x => x.Item1 == key))
                {
                    var temp = result.Find(x => x.Item1 == member.Value);
                    result.Remove(temp);
                    value += temp.Item2;
                }
                result.Add((key, value));
            }
            result.Sort((x,y) =>  x.Item1.CompareTo(y.Item1) );
            return result;
        }

        private string[] SplitString(string equation)
        {
           return equation.ToLower().Split('\n'); 
        }


        private List<double> GetValueOfLine(string[] lines)
        {
            List<double> result = new();
            lines.ToList().ForEach(x =>
            {
                result.Add(double.Parse(regexForValue.Matches(x).Last().Value));
            });
            return result;
        }




    }
}

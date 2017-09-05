using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{
    class RPNCalculatorEngine : CalculatorEngine
    {
        public string Process(string str)
        {
            List<string> parts = str.Split(' ').ToList<string>();
            string result = "0";
            Stack rpnStack = new Stack();
            for (int i =0; i < parts.Count; i++)
            {
                if (isNumber(parts[i]))
                {
                    rpnStack.Push(parts[i]);
                }
                if (isOperator(parts[i]))
                {
                    string second = rpnStack.Pop().ToString();
                    string first = rpnStack.Pop().ToString();
                    result = calculate(parts[i], first, second);
                    rpnStack.Push(result);
                    
                }
            }
            return result;
        }
    }
}
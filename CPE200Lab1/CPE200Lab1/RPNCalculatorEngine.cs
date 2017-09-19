using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{
    public class RPNCalculatorEngine : CalculatorEngine
    {
        public new string Process(string str)
        {
            Stack<string> rpnStack = new Stack<string>();
            if (str == null || str == "") return "E";

            char checkingPlus = str[0];


            if (checkingPlus == '+') //Starter "+"
            {
                return "E";
            }



            List<string> parts = str.Split(' ').ToList<string>();




            checkingPlus = str[0];
            if (parts.Count < 3 && checkingPlus != '0') return "E";
            if (parts.Count == 1)
            {
                Double cast = Convert.ToUInt16(parts[0]);
                if (isNumber(parts[0])) return parts[0];
                return "E";
            }
            else if (parts.Count <= 2)
            {
                return "E";
            }


            string result;
            string firstOperand, secondOperand;




            foreach (string token in parts)
            {
                if (isNumber(token))
                {
                    rpnStack.Push(token);
                }



                //Divine if
                else if (isOperator(token))
                {
                    //FIXME, what if there == only one left in stack?
                    if (rpnStack.Count < 2)
                    {
                        return "E";
                    }

                    secondOperand = rpnStack.Pop();
                    firstOperand = rpnStack.Pop();

                    result = calculate(token, firstOperand, secondOperand, 8);

                    //Second and First not an Operand
                    if (isOperator(firstOperand))
                    {
                        return "E";
                    }
                    else if (isOperator(secondOperand))
                    {
                        return "E";
                    }


                    if (result == "E")
                    {
                        return result;
                    }
                    rpnStack.Push(result);

                }
                else if (token != "") return "E";
            }


            if (rpnStack.Count != 1) return "E";
            if (rpnStack.Count == 0) return "E";
            //FIXME, what if there == more than one, or zero, items in the stack?
            result = rpnStack.Pop();
            return result;
        }
    }
}
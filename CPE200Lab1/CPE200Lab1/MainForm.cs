using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class MainForm : Form
    {
        private bool hasDot;
        private bool isAllowBack;
        private bool isAfterOperater;
        private bool isAfterEqual;
        private string firstOperand;
        private string secondOperand;
        private string operate;
        CalculatorEngine engine;
        double Mpluse;
        double Mminus;
        double sum=0;
        private double memory;
        private string temp;
        
 
        private void resetAll()
        {
            lblDisplay.Text = "0";
            isAllowBack = true;
            hasDot = false;
            isAfterOperater = false;
            isAfterEqual = false;
            firstOperand = null;
            sum = 0;
            
        }

        

        public MainForm()
        {
            InitializeComponent();
            memory = 0;
            engine = new CalculatorEngine();
            resetAll();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (isAfterOperater)
            {
                lblDisplay.Text = "0";
            }
            if(lblDisplay.Text.Length is 8)
            {
                return;
            }
            isAllowBack = true;
            string digit = ((Button)sender).Text;
            if(lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += digit;
            isAfterOperater = false;
        }

        private void btnUnaryOperator_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                return;
            }
            operate = ((Button)sender).Text;
            firstOperand = lblDisplay.Text;
            string result = engine.unaryCalculate(operate, firstOperand);
            string temp = operate;
            if (result is "E" || result.Length > 8)
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
            }

        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                return;
            }

            string result = engine.calculate(operate, firstOperand, secondOperand);
            string temp = operate;
            operate = ((Button)sender).Text;

            switch (operate)
            {
                case "+":
                case "-":
                case "X":
                case "÷":
                    if (firstOperand == null)
                    {
                        firstOperand = lblDisplay.Text;
                        isAfterOperater = true;
                    }
                    else
                    {
                        secondOperand = lblDisplay.Text;
                        firstOperand = engine.calculate(temp, firstOperand, secondOperand);
                        lblDisplay.Text = firstOperand;
                        isAfterOperater = true;
                    }
                    break;
                case "%":
                    // your code here
                    lblDisplay.Text = (Convert.ToDouble(firstOperand) * Convert.ToDouble(lblDisplay.Text) / 100).ToString();
                    isAfterOperater = true;
                    operate = temp;
                    break;

                case "√":
                    firstOperand = lblDisplay.Text;
                    lblDisplay.Text = (Math.Pow(Convert.ToDouble(firstOperand), 0.5)).ToString();
                    isAfterOperater = true;
                    break;

                case "1/X":
                    firstOperand = lblDisplay.Text;
                    lblDisplay.Text = (1 / Convert.ToDouble(firstOperand)).ToString();
                    isAfterOperater = true;
                    break;

                case "M+":
                    Mpluse = Convert.ToDouble(lblDisplay.Text);
                    sum = sum + Mpluse;
                    isAfterOperater = true;
                    break;

                case "M-":
                    Mminus = Convert.ToDouble(lblDisplay.Text);
                    sum = sum - Mminus;
                    isAfterOperater = true;
                    break;
                case "MS":
                    sum = Convert.ToDouble(lblDisplay.Text);

                    isAfterOperater = true;
                    break;
            }
            isAllowBack = false;
        }

            private void btnEqual_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            string secondOperand = lblDisplay.Text;
            string result = engine.calculate(operate, firstOperand, secondOperand);
            if (result is "E" || result.Length > 8)
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
            }
            isAfterEqual = true;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (!hasDot)
            {
                lblDisplay.Text += ".";
                hasDot = true;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            // already contain negative sign
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if(lblDisplay.Text[0] is '-')
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            } else
            {
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            if (!isAllowBack)
            {
                return;
            }
            if(lblDisplay.Text != "0")
            {
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if(rightMost is '.')
                {
                    hasDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if(lblDisplay.Text is "" || lblDisplay.Text is "-")
                {
                    lblDisplay.Text = "0";
                }
            }
        }

        private void btnMP_Click(object sender, EventArgs e)
        {
            if(lblDisplay.Text is "Error")
            {
                return;
            }
            memory += Convert.ToDouble(lblDisplay.Text);
            isAfterOperater = true;
        }

        private void btnMC_Click(object sender, EventArgs e)
        {
            memory = 0;
            resetAll();
        }

        private void btnMM_Click(object sender, EventArgs e)
        {
            if(lblDisplay.Text is "Error")
            {
                return;
            }
            memory -= Convert.ToDouble(lblDisplay.Text);
            isAfterOperater = true;
        }

        private void btnMR_Click(object sender, EventArgs e)
        {
            if(lblDisplay.Text is "error")
            {
                return;
            }
            lblDisplay.Text = memory.ToString();
        }
    }
}

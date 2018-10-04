using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Speech.Synthesis;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for Simple_Calculator.xaml
    /// </summary>
    public partial class Simple_Calculator : Window
    {
        
        public Simple_Calculator()
        {
            InitializeComponent();
        }
        string input = string.Empty;
        string oprd1  = string.Empty;
        string oprd2  = string.Empty;
        double result = 0;
        string opera;

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SelectVoice("Microsoft David Desktop");
            //synth.Speak(text);
            oprd2 = input;
            double.TryParse(oprd1, out double opd1);
            double.TryParse(oprd2, out double opd2);


            switch (opera)
            {
                case "+":
                    string sum = Convert.ToString(Math.Round(opd1 + opd2, 10));
                    input1.Text = oprd1 +" + "+ oprd2 + " = " + sum;
                    synth.Speak(opd1 + "+" + opd2 + "=" + sum);
                    opera += string.Empty;
                    break;
                case "-":
                    string diff = Convert.ToString(Math.Round(opd1 - opd2, 10));
                    input1.Text = oprd1 + " - " + oprd2 + " = " + diff;
                    synth.Speak(opd1 + "minus" + opd2 + "=" + diff);
                    opera += string.Empty;
                    break;
                case "*":
                    string pro = Convert.ToString(Math.Round(opd1 * opd2, 10));
                    input1.Text = oprd1 + " x " + oprd2 + " = " + pro;
                    synth.Speak(opd1 + "multiplied by" + opd2 + "=" + pro);
                    opera += string.Empty;
                    break;

                case "/":
                    string co = Convert.ToString(Math.Round(opd1 / opd2, 10));
                    input1.Text = oprd1 + " / " + oprd2 + " = " + co;
                    synth.Speak(opd1 + "divided by " + opd2 + "=" + co);
                    opera += string.Empty;
                    break;

                default:
                    synth.Speak("please select a valid operator");
                    break;

            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            this.input1.Text = string.Empty;
            this.input = string.Empty;
            this.oprd1 = string.Empty;
            this.oprd2 = string.Empty;
        }

        private void seven_Click(object sender, RoutedEventArgs e) 
        {
           
            this.input1.Text = string.Empty;
            Button b = (Button)sender;
            input += b.Content;
            this.input1.Text += input;
        }

        private void main_Click(object sender, RoutedEventArgs e)
        {
            Simple_Calculator1.Hide();
        }
   
        private void minus_Click(object sender, RoutedEventArgs e)
        {
             oprd1 = input;
             opera = "-";
             input = string.Empty; 
        }

        private void divide_Click(object sender, RoutedEventArgs e)
        {
            oprd1 = input;
            opera = "/";
            input = string.Empty;
        }

        private void mult_Click(object sender, RoutedEventArgs e)
        {
            oprd1 = input;
            opera = "*";
            input = string.Empty;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            oprd1 = input;
            opera = "+";
            input = string.Empty;
        }
    }
}

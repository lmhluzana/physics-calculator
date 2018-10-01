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
        double resul = 0;
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
                    break;
                case "-":
                    string diff = Convert.ToString(Math.Round(opd1 - opd2, 10));
                    input1.Text = oprd1 + " - " + oprd2 + " = " + diff;
                    synth.Speak(opd1 + "minus" + opd2 + "=" + diff);
                    break;
                case "x":
                    string pro = Convert.ToString(Math.Round(opd1 * opd2, 10));
                    input1.Text = oprd1 + " x " + oprd2 + " = " + pro;
                    synth.Speak(opd1 + "*" + opd2 + "=" + pro);
                    break;

                case "/":
                    string co = Convert.ToString(Math.Round(opd1 / opd2, 10));
                    input1.Text = oprd1 + " / " + oprd2 + " = " + co;
                    synth.Speak(opd1 + "divided by " + opd2 + "=" + co);
                    break;

                default:
                    synth.Speak("please select a valid operator");
                    break;

            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            ;
        }

        private void subtract_Click(object sender, RoutedEventArgs e)
        {
            ;
        }

        private void multiply_Click(object sender, RoutedEventArgs e)
        {
            ;
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            this.input1.Text = "";
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ;
        }

        private void minus_Click(object sender, RoutedEventArgs e)
        {
            Button c = (Button)sender;
            opera += c.Content;
            if (opera == "-")
            {
                oprd1 = input;
                opera = "-";
                input = string.Empty;
            }
            if (opera == "+")
            {
                oprd1 = input;
                opera = "+";
                input = string.Empty;
            }
            if (opera == "/")
            {
                oprd1 = input;
                opera = "/";
                input = string.Empty;
            }
            if (opera == "x")
            {
                oprd1 = input;
                opera = "x";
                input = string.Empty;
            }
        }
    }
}//

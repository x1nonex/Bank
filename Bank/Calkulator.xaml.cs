using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bank
{
    /// <summary>
    /// Логика взаимодействия для Calkulator.xaml
    /// </summary>
    public partial class Calkulator : Window
    {
        public Calkulator()
        {
            InitializeComponent();               
        }       
        private void Slider_monthly_replenishment_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBox_monthly_replenishment.Text = Slider_monthly_replenishment.Value.ToString();
        }
        private void TextBox_monthly_replenishment_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(TextBox_monthly_replenishment.Text, out double value))
            {
                if (Slider_monthly_replenishment != null) // Проверяем, что Slider_term инициализирован
                {                    
                    if (value >= Slider_monthly_replenishment.Minimum && value <= Slider_monthly_replenishment.Maximum)
                    {
                        double truncatedNumber = Math.Truncate(value * 100) / 100;
                        Slider_monthly_replenishment.Value = truncatedNumber;
                    }
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToString(Label_Stable.Content) != "Мин. 3 Месяца")
            {
                Comparison newWindow = new Comparison(null); // Создаем новый экземпляр окна
                newWindow.Show();
                newWindow.Label_Standard_income.Content = Label_Standard.Content;
                newWindow.Label_Optimal_income.Content = "---------";
                newWindow.Label_Stable_income.Content = Label_Stable.Content;
                Match mathStable = Regex.Match(Convert.ToString(Label_Stable.Content), @"\d+");                
                Match mathStandard = Regex.Match(Convert.ToString(Label_Standard.Content), @"\d+");
                double AmountStable = Convert.ToDouble(mathStable.Value) + Convert.ToDouble(TextBox_Summ.Text);                
                double AmountStandard = Convert.ToDouble(mathStandard.Value) + Convert.ToDouble(TextBox_Summ.Text);
                newWindow.Label_Stable_Amount.Content = AmountStable + "Руб.";
                newWindow.Label_Optimal_Amount.Content = "---------";
                newWindow.Label_Standard_Amount.Content = AmountStandard + "Руб.";
                if (Convert.ToString(Label_Optimal.Content) != "Мин. 6 Месяца")
                {
                    newWindow.Label_Optimal_income.Content = Label_Optimal.Content;
                    Match mathOptimal = Regex.Match(Convert.ToString(Label_Optimal.Content), @"\d+");
                    double AmountOptimal = Convert.ToDouble(mathOptimal.Value) + Convert.ToDouble(TextBox_Summ.Text);
                    newWindow.Label_Optimal_Amount.Content = AmountOptimal + "Руб.";
                }
                this.Close();                           
            }
            else
            {
                Label_Stable.Content = "Мин. 3 Месяца";
                Label_Standard.Content = "Мин. 3 Месяца";
                Label_Optimal.Content = "Мин. 6 Месяца";
            }
        }     
        private void Slider_term_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBox_term.Text = Slider_term.Value.ToString();
        }
        private void TextBox_term_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(TextBox_term.Text, out double value))
            {
                if (Slider_term != null) // Проверяем, что Slider_term инициализирован
                {
                    if (value >= Slider_term.Minimum && value <= Slider_term.Maximum)
                    {
                        double truncatedNumber = Math.Truncate(value * 100) / 100;
                        Slider_term.Value = truncatedNumber;
                    }
                }
            }
        }
        private void Slider_Summ_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBox_Summ.Text = Slider_Summ.Value.ToString();
        }
        private void TextBox_Summ_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(TextBox_Summ.Text, out double value))
            {
                if (Slider_Summ != null) // Проверяем, что Slider_Summ инициализирован
                {
                    if (value >= Slider_Summ.Minimum && value <= Slider_Summ.Maximum)
                    {
                        double truncatedNumber = Math.Truncate(value * 100) / 100;
                        Slider_Summ.Value = truncatedNumber;
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TextBox_monthly_replenishment.Text, out double Monthly_replenishment) && double.TryParse(TextBox_Summ.Text, out double Summ) && double.TryParse(TextBox_term.Text, out double Term))
            {
                double Stable = (Summ * 0.08) * (Term / 12);                
                double Standart = 0;
                double Optimal = 0;
                for (int i = 0; i < Term; i++)
                {
                    double a = Monthly_replenishment * i;
                    double c = ((Summ + a) * 0.05);
                    double b = (1 / 12.0);
                    Summ +=  c * b;
                    Optimal = (Summ - Convert.ToDouble(TextBox_Summ.Text));
                }
                Summ = Convert.ToDouble(TextBox_Summ.Text);
                for (int i = 0; i < Term; i++)
                {
                    double a = Monthly_replenishment * i;
                    double c = ((Summ + a) * 0.05);
                    double b = (1.0 / 12.0);
                    Standart += c * b;                    
                }              
                if (Term >= 3)
                {
                    Label_Stable.Content = Math.Round(Stable, 2) + "Руб.";
                    Label_Standard.Content = Math.Round(Standart, 2) + "Руб.";
                    if (Term >= 6)
                    {
                        Label_Optimal.Content = Math.Round(Optimal, 2) + "Руб.";
                    }
                    else Label_Optimal.Content = "Мин. 6 Месяца";
                }
                else
                {
                    Label_Stable.Content = "Мин. 3 Месяца";
                    Label_Standard.Content = "Мин. 3 Месяца";
                    Label_Optimal.Content = "Мин. 6 Месяца";
                }
            }
        }
    }
}

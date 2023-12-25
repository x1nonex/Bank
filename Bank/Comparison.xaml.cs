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


namespace Bank
{
    /// <summary>
    /// Логика взаимодействия для Comparison.xaml
    /// </summary>
    public partial class Comparison : Window
    {
        public Comparison(string Login)
        {
            InitializeComponent();
            if (Login != null)
            {
                label_Login.Content = Login;
            }
            else label_Login.Content = "Гость";
                    
        }

        private void Button_Stable_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization(Convert.ToString(Label_Stable_Amount.Content),
                Convert.ToString(Label_Optimal_Amount.Content),
                Convert.ToString(Label_Standard_Amount.Content),
                Convert.ToString(Label_Stable_income.Content),
                Convert.ToString(Label_Optimal_income.Content),
                Convert.ToString(Label_Standard_income.Content));
            authorization.Show();
            this.Close();
        }

        private void Button_Optimal_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization(Convert.ToString(Label_Stable_Amount.Content),
                Convert.ToString(Label_Optimal_Amount.Content),
                Convert.ToString(Label_Standard_Amount.Content),
                Convert.ToString(Label_Stable_income.Content),
                Convert.ToString(Label_Optimal_income.Content),
                Convert.ToString(Label_Standard_income.Content));
            authorization.Show();
            this.Close();

        }

        private void Button_Standard_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization(Convert.ToString(Label_Stable_Amount.Content),
                Convert.ToString(Label_Optimal_Amount.Content),
                Convert.ToString(Label_Standard_Amount.Content),
                Convert.ToString(Label_Stable_income.Content),
                Convert.ToString(Label_Optimal_income.Content),
                Convert.ToString(Label_Standard_income.Content));
            authorization.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

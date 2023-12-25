using System;
using System.Collections.Generic;
using System.IO;
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
using System.Data.SQLite;

namespace Bank
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {        
        public Authorization(string Stable_Amount,
                string Optimal_Amount,
                string Standard_Amount,
                string Stable_income,
                string Optimal_income,
                string Standard_income)
        {
            InitializeComponent();
            Line_Info = Stable_Amount + "|" + Optimal_Amount + "|" + Standard_Amount + "|" + Stable_income + "|" + Optimal_income + "|" + Standard_income;
        }        
        public static List<string> List_Users = new List<string>();
        public static List<string> List_Password = new List<string>();
        public static bool List_Stable;
        public static bool List_Optimal;
        public static bool List_Standart;
        public static string Line_Info;
        public static string Line_Inf;
        private void Entrance_Click(object sender, RoutedEventArgs e)
        {
            string Login = TextBox_Login.Text;
            string Password = TextBox_Password.Text;                       
            if (Data_Base_Class.DataBaseWork(Login, Password))
            {
                Comparison newWindow = new Comparison(Login);
                newWindow.Show();
                string[] strings = Line_Info.Split('|');
                newWindow.Label_Standard_income.Content = strings[3];
                newWindow.Label_Optimal_income.Content = strings[4];
                newWindow.Label_Stable_income.Content = strings[5];
                newWindow.Label_Stable_Amount.Content = strings[0];
                newWindow.Label_Optimal_Amount.Content = strings[1];
                newWindow.Label_Standard_Amount.Content = strings[2];
                newWindow.label_Login.Content = Login;
                if (List_Optimal) newWindow.CheckBox_Optimal.IsChecked = true;
                else newWindow.CheckBox_Optimal.IsChecked = false;
                if (List_Standart) newWindow.CheckBox_Standard.IsChecked = true;
                else newWindow.CheckBox_Standard.IsChecked = false;
                if (List_Stable) newWindow.CheckBox_Stable.IsChecked = true;
                else newWindow.CheckBox_Stable.IsChecked = false;
                this.Close();
            }
            else
            {
                error_Login.Content = "Ошибка Логина";
                error_Password.Content = "Ошибка Пароля";
            }
        }        
    }
    public class Data_Base_Class
    {
        private static string bdFileName = "Bank_DataBase.db";
        private static SQLiteConnection bd_Connect;
        private static SQLiteCommand bd_Command;
        public static bool DataBaseWork(string Login,string Password)
        {
            try
            {
                bd_Connect = new SQLiteConnection("Data Source=" + bdFileName + ";Version=3;");
                bd_Connect.Open();
                string comandLoginPassword = "SELECT * FROM Users";
                bd_Command = new SQLiteCommand(comandLoginPassword, bd_Connect);                
                using (SQLiteDataReader reader = bd_Command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Convert.ToString(reader["login"]) == Login && Convert.ToString(reader["password"]) == Password)
                        {
                            if (Convert.ToInt32(reader["stable"]) == 1)
                                Authorization.List_Stable = true;
                            else Authorization.List_Stable = false;
                            if (Convert.ToInt32(reader["optimal"]) == 1)
                                Authorization.List_Optimal = true;
                            else Authorization.List_Optimal = false;
                            if (Convert.ToInt32(reader["standart"]) == 1)
                                Authorization.List_Standart = true;
                            else Authorization.List_Standart = false;
                            return true;
                        }
                    }
                    return false;
                }                
            }
            catch (SQLiteException ex)
            {
                return false;       
            }
        }
    }
}

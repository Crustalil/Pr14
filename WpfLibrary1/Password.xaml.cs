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

namespace WpfLibrary1
{
    /// <summary>
    /// Логика взаимодействия для Password.xaml
    /// </summary>
    public partial class Password : Window
    {
        public Password()
        {
            InitializeComponent();
        }

        //Ввод пароля
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (txtPas.Password == "123")
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный пароль. Попробуйте снова.", "Ошибка");
                txtPas.Clear();
            }
        }

        //Закрытие окна авторизации
        private void btnEsc_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Close();
        }

    }
}

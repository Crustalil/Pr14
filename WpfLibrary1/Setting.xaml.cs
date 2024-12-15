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
using Пример_таблицы_WPF;
using System.IO;

namespace WpfLibrary1
{
    /// <summary>
    /// Логика взаимодействия для Setting.xaml
    /// </summary>
    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
        }
        //Строки
        public int Rows { get; private set; }

        //Столбцы
        public int Cols { get; private set; }

        //Кнопка записи настроек
        private void btnSetSetting_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbCountRow.Text, out int rows) && int.TryParse(tbCountColumn.Text, out int cols))
            {
                this.Rows = rows;
                this.Cols = cols;

                File.WriteAllText("config.ini", $"{rows},{cols}");

                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Введите корректные значения.", "Ошибка");
            }
        }


    }
}

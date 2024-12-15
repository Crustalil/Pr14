using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfLibrary1;
using Пример_таблицы_WPF;
using System.IO;



namespace Пр14
{
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
            LoadSettings();
        }

        //Таймер
        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;
            tbTime.Text = d.ToString("HH:mm:ss");
            tbData.Text = d.ToString("dd.MM.yyyy");
        }

        //Инициализаци таймера
        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }
        int[,] matr;
        int value, value1;
        bool f, c;

        //Кнопка информации
        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Дана целочисленная матрица размера M * N. Найти номер первого из ее столбцов, содержащих максимальное количество одинаковых элементов.  \nРазарботчик: Кузнецов М.Н. ИСП-31");
        }

        //Кнопка выхода
        public void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Заполнение
        public void miFill_Click(object sender, RoutedEventArgs e)
        {
            f = Int32.TryParse(tbColumnCount.Text, out value);
            c = Int32.TryParse(tbRowCount.Text, out value1);
            if (f && c == true)
            {

                WorkWithMassiv.InitMas(out matr, value1, value);
                dataGrid.ItemsSource = VisualArray.ToDataTable(matr).DefaultView;
                tbColumnCount.Clear();
                tbRowCount.Clear();

                tbProduct.Clear();
            }
            else MessageBox.Show("Введите корректные значения");
        }

        //Очистка
        public void miClear_CLick(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            WorkWithMassiv.ClearMas(ref matr);
            tbColumnCount.Clear();
            tbRowCount.Clear();

            tbProduct.Clear();
        }

        //Сохранение
        private void Save_CLick(object sender, RoutedEventArgs e)
        {
            WorkWithMassiv.SaveMas(matr);
        }

        //Открытие
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            WorkWithMassiv.OpenMas(ref matr);
            dataGrid.ItemsSource = VisualArray.ToDataTable(matr).DefaultView;
        }

        //Кнопка настройки матрицы
        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            Setting settingsWindow = new Setting();
            if (settingsWindow.ShowDialog() == true)
            {
                int rows = settingsWindow.Rows;
                int cols = settingsWindow.Cols;

                tbRowCount.Text = rows.ToString();
                tbColumnCount.Text = cols.ToString();
            }
        }

        //Метод загрузки размеров матрицы
        private void LoadSettings()
        {
            if (File.Exists("config.ini"))
            {
                var content = File.ReadAllText("config.ini").Split(',');
                if (int.TryParse(content[0], out int rows) && int.TryParse(content[1], out int cols))
                {
                    tbRowCount.Text = rows.ToString();
                    tbColumnCount.Text = cols.ToString();
                }
            }
        }

        //Вывод размера матрицы
        private void dgInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int rowCount = dataGrid.Items.Count;
            int columnCount = (matr != null) ? matr.GetLength(1) : 0;

            StatusTableSize.Text = $"Размер таблицы: {rowCount}x{columnCount}";
        }

        //Очистка входных данных
        private void btnClic_DataClear(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            WorkWithMassiv.ClearMas(ref matr);
            tbColumnCount.Clear();
            tbRowCount.Clear();
        }

        //Очистка результата
        private void btnClic_RezClear(object sender, RoutedEventArgs e)
        {
            tbProduct.Clear();
        }

        //Окно авторизации
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Password pas = new Password();
            pas.Owner = this;
            if (pas.ShowDialog() != true)
            {
                this.Close();
            }

        }

        //Проверка на корректность значений и обновление данных
        public void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid != null)
            {
                int rowIndex = e.Row.GetIndex();
                int columnIndex = e.Column.DisplayIndex;

                TextBox editedTextbox = e.EditingElement as TextBox;
                if (editedTextbox != null)
                {
                    int newValue;
                    if (int.TryParse(editedTextbox.Text, out newValue))
                    {
                        // Обновляем значение в матрице matr
                        matr[rowIndex, columnIndex] = newValue;
                    }
                    else
                    {
                        MessageBox.Show("Введите корректное числовое значение.");
                        // Возвращаем предыдущее значение в ячейку
                        editedTextbox.Text = matr[rowIndex, columnIndex].ToString();
                    }
                }
            }

        }

        //Расчет
        public void miCalc_CLick(object sender, RoutedEventArgs e)
        {
            if (matr != null)
            {
                tbProduct.Text = RaschetInMatr.Search(matr).ToString();
            }
            else MessageBox.Show("Заполните матрицу");
        }
    }
}
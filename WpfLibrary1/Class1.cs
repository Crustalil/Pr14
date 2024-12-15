using Microsoft.Win32;
using System.Data;
using System.IO;
using System.Windows;

namespace Пример_таблицы_WPF
{
    //Класс для связывания массива с элементом DataGrid
    //для визуализации данных 
    public static class VisualArray
    {
        //Метод для двухмерного массива
        public static DataTable ToDataTable<T>(this T[,] matrix)
        {
            var res = new DataTable();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                res.Columns.Add("col" + (i + 1), typeof(T));
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var row = res.NewRow();

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    row[j] = matrix[i, j];
                }

                res.Rows.Add(row);
            }

            return res;
        }
    }



    public class WorkWithMassiv
    {
        /// <summary>
        /// Заполнение матрицы случайными значениями
        /// </summary>
        /// <param name="matr">Матрица</param>
        /// <param name="row">Колличество строк</param>
        /// <param name="column">Колличество  столбцов</param>
        public static void InitMas(out int[,] matr, int row, int column)
        {
            Random Rnd = new Random();
            matr = new Int32[row, column];
            for (int i = 0; i < matr.GetLength(0); i++)
                for (int j = 0; j < matr.GetLength(1); j++) matr[i, j] = Rnd.Next(7);
        }

        /// <summary>
        /// Очистка матрицы
        /// </summary>
        /// <param name="matr">Матрица</param>
        public static void ClearMas(ref int[,] matr)
        {
            matr = null;
        }

        /// <summary>
        /// Сохранения матрицы в текстовый документ
        /// </summary>
        /// <param name="matr">Матрица</param>
        public static void SaveMas(int[,] matr)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы (*.*) |*.*| Текстовые файлы | *.txt*";
            save.FilterIndex = 1;
            save.Title = "Сохранение таблицы";
            if (save.ShowDialog() == true)
            {
                StreamWriter outfile = new StreamWriter(save.FileName, false);
                outfile.WriteLine(matr.GetLength(0));
                outfile.WriteLine(matr.GetLength(1));
                for (int i = 0; i < matr.GetLength(0); i++)
                {
                    for (int j = 0; j < matr.GetLength(1); j++)
                    {
                        outfile.WriteLine(matr[i, j]);
                    }
                }
                outfile.Close();
            }
            else MessageBox.Show("Не удалось открыть окно");
        }

        /// <summary>
        /// Открытие текстовго документа с матрицой
        /// </summary>
        /// <param name="matr">Матрица</param>
        public static void OpenMas(ref int[,] matr)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = ".txt";
            open.Filter = "Все файлы (*.*) |*.*| Текстовые файлы | *.txt*";
            open.FilterIndex = 2;
            open.Title = "Открытие таблицы";
            if (open.ShowDialog() == true)
            {
                StreamReader file = new StreamReader(open.FileName);
                int row = Convert.ToInt32(file.ReadLine());
                int col = Convert.ToInt32(file.ReadLine());
                matr = new int[row, col];
                for (int i = 0; i < matr.GetLength(0); i++)
                {
                    for (int j = 0; j < matr.GetLength(1); j++)
                    {
                        string a = file.ReadLine();
                        bool f1;
                        int value;
                        f1 = int.TryParse(a, out value);
                        if (f1)
                        {
                            matr[i, j] = value;

                        }

                        else MessageBox.Show("Некоректные значения");
                    }
                }
                file.Close();
            }
        }
    }

    public class RaschetInMatr
    {
        /// <summary>
        /// Расчет суммы и произведения K-го столбца матрицы
        /// </summary>
        /// <param name="matr"></param>
        /// <param name="K"></param>
        /// <param name="sum"></param>
        /// <param name="product"></param>
        public static int Search(int[,] matr)
        {


            int maxDuplicateCount = 0;
            int columnNumber = -1;

            for (int j = 0; j < matr.GetLength(1); j++)
            {
                int currentMaxDuplicateCount = 0;
                for (int i = 0; i < matr.GetLength(0) - 1; i++)
                {
                    if (matr[i, j] == matr[i + 1, j])
                    {
                        currentMaxDuplicateCount++;
                    }
                }

                if (currentMaxDuplicateCount > maxDuplicateCount)
                {
                    maxDuplicateCount = currentMaxDuplicateCount;
                    columnNumber = j + 1;
                }
            }

            if (columnNumber != -1)
                return columnNumber;
            else return -1;



        }
    }
}

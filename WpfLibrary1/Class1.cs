using Microsoft.Win32;
using System.Data;
using System.IO;
using System.Windows;

namespace ������_�������_WPF
{
    //����� ��� ���������� ������� � ��������� DataGrid
    //��� ������������ ������ 
    public static class VisualArray
    {
        //����� ��� ����������� �������
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
        /// ���������� ������� ���������� ����������
        /// </summary>
        /// <param name="matr">�������</param>
        /// <param name="row">����������� �����</param>
        /// <param name="column">�����������  ��������</param>
        public static void InitMas(out int[,] matr, int row, int column)
        {
            Random Rnd = new Random();
            matr = new Int32[row, column];
            for (int i = 0; i < matr.GetLength(0); i++)
                for (int j = 0; j < matr.GetLength(1); j++) matr[i, j] = Rnd.Next(7);
        }

        /// <summary>
        /// ������� �������
        /// </summary>
        /// <param name="matr">�������</param>
        public static void ClearMas(ref int[,] matr)
        {
            matr = null;
        }

        /// <summary>
        /// ���������� ������� � ��������� ��������
        /// </summary>
        /// <param name="matr">�������</param>
        public static void SaveMas(int[,] matr)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "��� ����� (*.*) |*.*| ��������� ����� | *.txt*";
            save.FilterIndex = 1;
            save.Title = "���������� �������";
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
            else MessageBox.Show("�� ������� ������� ����");
        }

        /// <summary>
        /// �������� ��������� ��������� � ��������
        /// </summary>
        /// <param name="matr">�������</param>
        public static void OpenMas(ref int[,] matr)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = ".txt";
            open.Filter = "��� ����� (*.*) |*.*| ��������� ����� | *.txt*";
            open.FilterIndex = 2;
            open.Title = "�������� �������";
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

                        else MessageBox.Show("����������� ��������");
                    }
                }
                file.Close();
            }
        }
    }

    public class RaschetInMatr
    {
        /// <summary>
        /// ������ ����� � ������������ K-�� ������� �������
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

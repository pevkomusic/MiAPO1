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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void exit_b_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void about_b_Click(object sender, RoutedEventArgs e)                    //Кнопка "О программе"
        {
            MessageBox.Show("Задание: Дана квадратная вещественная матрица размерности n. Сравнить сумму элементов матрицы на главной и побочной диагоналях.\nРазработчик: Герасимов Дмитрий гр. ИСП-32", "О программе", MessageBoxButton.OK, MessageBoxImage.Information); //Сообщениe
        }

        double[,] matr;                                                                 //Объявляем глобально матрицу
        int n = 0;

        private void create_b_Click(object sender, RoutedEventArgs e)                   //Кнопка "Создать"
        {
            if (razmer_tb.Text != "")                                                   //Проверка на пустоту
            {
                bool a = Int32.TryParse(razmer_tb.Text, out n);                         //Записываем в переменную значение из ТекстБокса
                if (a == true)                                                          //Проверяем введено ли число
                {
                    if (n > 1)                                                          //Условие для создания матрицы от размерности 2
                    {
                        double[,] matr = new double[n, n];
                        dataGrid.ItemsSource = VisualArray.ToDataTable(matr).DefaultView;   //Вносим матрицу в ДатаГрид, используя класс
                    }
                    else MessageBox.Show("Введите число >1!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);    //Сообщение об ошибке
                }
                else MessageBox.Show("Введите положительное целое число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);   //Сообщение об ошибке
            }
            else MessageBox.Show("Размерность матрицы не указана!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);  //Сообщение об ошибке
        }


        private void rnd_b_Click(object sender, RoutedEventArgs e)
        {
            if (razmer_tb.Text != "" && min_tb.Text != "" && max_tb.Text != "")         //Проверка на пустоту
            {
                bool a = Int32.TryParse(razmer_tb.Text, out n);                         //Записываем в переменную значение из ТекстБокса
                bool min1 = Int32.TryParse(min_tb.Text, out int min);                   //Записываем в переменную значение из ТекстБокса
                bool max1 = Int32.TryParse(max_tb.Text, out int max);                   //Записываем в переменную значение из ТекстБокса

                if (a == true && min1 == true && max1 == true)                          //Проверяем введено ли число
                {
                    if (min < max)                                                      //Сравниваем крайние границы рандома
                    {
                        if (n > 1)                                                      //Условие для создания матрицы от размерности 2
                        {
                            matr = new double[n, n];
                            Random rnd = new Random();                                  //Объявляем рандом

                            for (int i = 0; i < n; i++)                                 //Циклы для заполнения матрицы
                            {
                                for (int j = 0; j < n; j++)
                                {
                                    matr[i, j] = Math.Round((min + rnd.NextDouble() * (max - min)), 2);         //Заполняем матрицу случайными числами вещественного типа данных
                                }
                            }
                            dataGrid.ItemsSource = VisualArray.ToDataTable(matr).DefaultView;                   //Записывает сгенерированные значения в ДатаГрид
                        }
                        else MessageBox.Show("Введите число >1!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else MessageBox.Show("Крайний левый предел должен быть меньше крайнего правого!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);    //Сообщение об ошибке
                }
                else MessageBox.Show("Введите положительное целое число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);                               //Сообщение об ошибке
            }
            else MessageBox.Show("Одно или несколько полей не заполнено!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);                           //Сообщение об ошибке
        }

        private void rez_b_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.ItemsSource != null)                       //Проверяем, что ДатаГрид заполнен
            {
                if (matr != null)                                   //Проверяем, что матрица заполнена
                {
                    double sumMainDiagonal = 0;                     
                    double sumPobochDiagonal = 0;

                    for (int i = 0; i < n; i++)
                    {
                        sumMainDiagonal += matr[i, i];              //Находим сумму элементов главной диагонали
                        sumPobochDiagonal += matr[i, n - 1 - i];    //Находим сумму элементов побочной диагонали
                    }

                    if (sumMainDiagonal > sumPobochDiagonal)        //Сравниваем суммы диагоналей
                    {
                        rezult_tb.Text = $"Сумма элементов матрицы на главной диагонали ({Math.Round(sumMainDiagonal, 2)}) БОЛЬШЕ суммы элементов на побочной ({Math.Round(sumPobochDiagonal, 2)}).";
                    }
                    else if (sumMainDiagonal < sumPobochDiagonal)   //Сравниваем суммы диагоналей
                    {
                        rezult_tb.Text = $"Сумма элементов матрицы на главной диагонали ({Math.Round(sumMainDiagonal, 2)}) МЕНЬШЕ суммы элементов на побочной ({Math.Round(sumPobochDiagonal, 2)}).";
                    }
                    else if (sumMainDiagonal == sumPobochDiagonal)  //Сравниваем суммы диагоналей
                    {
                        rezult_tb.Text = $"Суммы элементов главной и побочной диагоналей матрицы РАВНЫ ({Math.Round(sumMainDiagonal, 2)} = {Math.Round(sumPobochDiagonal, 2)}).";
                    }
                }
                else MessageBox.Show("Матрица не заполнена. Нажмите кнопку 'Рандом' сначала.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);       //Сообщение об ошибке
            }
            else MessageBox.Show("Таблица не создана.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);                                              //Сообщение об ошибке
        }

        private void clear_b_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.ItemsSource != null)
            {
                dataGrid.ItemsSource = null;                    //Очищаем содержимое DataGrid
                rezult_tb.Clear();                              //Очищаем TextBox'ы
                razmer_tb.Clear();
                min_tb.Clear();
                max_tb.Clear();
            }
            else MessageBox.Show("Таблицы не существует!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);                                    //Сообщение об ошибке
        }

        private void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();   //Нумеруем строки
        }
    }
}

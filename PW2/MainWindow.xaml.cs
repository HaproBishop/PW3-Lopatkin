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
using System.IO;
using Microsoft.Win32;
using LibMas;
using Lib_8;
using VisualTable;

//Практическая работа №3. Лопаткин Сергей ИСП-31
//Задание №8. Дана матрица M x N. В каждом столбце матрицы найти максимальный элемент
namespace PW2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Open_Menu_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog
            {
                Title = "Открытие таблицы",
                Filter = "Все файлы (*.*) | *.* | Текстовые файлы | *.txt",
                FilterIndex = 2,
                DefaultExt = "*.txt",                
            };
            
            if (openfile.ShowDialog() == true) 
            {                                
                WorkMas.Open_File(openfile.FileName); //Обращение к функции с параметром (название текстового файла, в котором хранятся данные)
                DataGT.ItemsSource = VisualArray.ToDataTable(WorkMas.dmas).DefaultView; //Отображение данных, считанных с файла
                Solution_TextBox.Clear();
                Solute_Button.IsEnabled = true;
                Solute_Menu.IsEnabled = true;
            }
        }

        private void Save_Menu_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog
            {
                Title = "Сохранение таблицы",
                Filter = "Все файлы (*.*) | *.* | Текстовые файлы | *.txt",
                FilterIndex = 2,
                DefaultExt = "*.txt",
            };

            if (savefile.ShowDialog() == true)
            {                
                WorkMas.twomas = true;                
                WorkMas.Save_File(savefile.FileName); //Обращение к функции с параметром (аналогично предыдущему) 
            }
        }

        private void ClearTable_Button_Click(object sender, RoutedEventArgs e)
        {
            DataGT.ItemsSource = WorkMas.ClearTable(); //Обращение к функции "очистки" массива и возвращение null для DataGrid(Очистка таблицы)
            Solution_TextBox.Clear();
            Solute_Button.IsEnabled = false;
            Solute_Menu.IsEnabled = false;
        }

        private void CreateMas_Button_Click(object sender, RoutedEventArgs e)
        {
            Solution_TextBox.Clear();
            bool prv_columns = Int32.TryParse(CountColumns_TextBox.Text, out int columns);
            bool prv_rows = Int32.TryParse(CountRows_TextBox.Text, out int rows);
            if (prv_columns == true && prv_rows == true && rows >= 0 && columns >= 0)
            {
                WorkMas.CreateMas(in rows, in columns);
                DataGT.ItemsSource = VisualArray.ToDataTable(WorkMas.dmas).DefaultView;
                Solute_Button.IsEnabled = true;
                Solute_Menu.IsEnabled = true;
            }
           
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e) //Закрытие программы
        {
            this.Close();
        }

        private void AbProg_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Лопаткин Сергей Михайлович. Практическая работа №3. Задание №8. Дана матрица M x N. В каждом столбце матрицы найти максимальный элемент","О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Fill_Button_Click(object sender, RoutedEventArgs e)
        {
            Solution_TextBox.Clear();
            bool prv_range = Int32.TryParse(Range_TextBox.Text, out int range);
            if (prv_range == true && WorkMas.dmas != null && range > 0) //2-ое условие - проверка на заполнение без скелета
            {
                WorkMas.FillDMas(in range);//Обращение с передачей информации об диапазоне
                DataGT.ItemsSource = VisualArray.ToDataTable(WorkMas.dmas).DefaultView; //Отображение таблицы с заполненными значениями
            }
            else MessageBox.Show("У вас нет скелета таблицы или введен некорректно диапазон значений", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Assist_Menu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("1) В программе нельзя вводить более трехзначных чисел для диапазона и двухзначных для столбцов и строк.\n2)Заполнение происходит от 0 до указанного вами значенияю\n3)Для включения кнопки \"Выполнить\" необходимо создать таблицу.", "Справка", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DataGT_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int iColumn = e.Column.DisplayIndex; //Присваивание значения выбранного столбца
            int iRow = e.Row.GetIndex();
            bool prv_edit = Int32.TryParse(((TextBox)e.EditingElement).Text, out WorkMas.dmas[iRow, iColumn]);//Проверка и вывод в массив         
            if(prv_edit==true) Solution_TextBox.Clear();
        }

        private void Solute_Button_Click(object sender, RoutedEventArgs e)
        {            
            Solution_TextBox.Text = Convert.ToString(Class_Lib_8.FindMaxOfColumn(WorkMas.dmas));//Вывод ответа в строку после обращения к функции
        }//с указанными данными массива(параметр)

        private void CountColumns_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CreateMas_Button.IsDefault = true;
            Fill_Button.IsDefault = false;
        }

        private void Range_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CreateMas_Button.IsDefault = false;
            Fill_Button.IsDefault = true;
        }

        private void CountRows_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CreateMas_Button.IsDefault = true;
            Fill_Button.IsDefault = false;
        }
    }
}

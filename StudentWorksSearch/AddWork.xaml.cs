using StudentWorksSearch.Engines;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace StudentWorksSearch
{
    /// <summary>
    /// Interaction logic for AddWork.xaml
    /// </summary>
    public partial class AddWork : Window
    {
        public AddWork()
        {
            InitializeComponent();

            var engine = new DBEngine();
            cmbboxDis.ItemsSource = engine.GetDis();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LuceneSearch.LuceneEngine le = new LuceneSearch.LuceneEngine();
               var engine = new FileEngine();
                
                if (txtboxName.Text != "" && cmbboxDis.SelectedIndex != -1 && btnFile.Content.ToString() != "Выбрать файл")
                {
                   var IndexMe= engine.AddFile(txtboxName.Text, cmbboxDis.SelectedIndex, txtboxAuth.Text, txtboxTags.Text, txtboxComment.Text);
                    le.BuildIndex(IndexMe);//index this file
                   
                    this.Close();
                }
                else
                    MessageBox.Show("Вы ввели не все данные!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
}

        private void btnFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var engine = new FileEngine();
            engine.LoadFile();
            string str = new FileInfo(Repository.Path).Name;
            btnFile.Content = "Выбран файл: " + str;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Вы не выбрали файл для чтения!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FileFormatException)
            {
                MessageBox.Show("Неверный формат файла!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}


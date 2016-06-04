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
using StudentWorksSearch.Engines;
using StudentWorksSearch.Repo;
using System.IO;

namespace StudentWorksSearch
{
    /// <summary>
    /// Interaction logic for PlagiarismCheck.xaml
    /// </summary>
    public partial class PlagiarismCheck : Window
    {
        public PlagiarismCheck()
        {
            InitializeComponent();
            progressBar.IsIndeterminate = true;

            var engine = new APIEngine();
            engine.CheckReady += result => SearchInvoke(result);
            engine.StartCheck();
        }

        private void SearchInvoke(string res)
        {
            txtPlag.Dispatcher.Invoke(() => txtPlag.Text = res);
            txtHead.Dispatcher.Invoke(() => txtHead.Text = "Проверка завершена");
            progressBar.Dispatcher.Invoke(() => progressBar.IsIndeterminate = false);
        }
    }
}

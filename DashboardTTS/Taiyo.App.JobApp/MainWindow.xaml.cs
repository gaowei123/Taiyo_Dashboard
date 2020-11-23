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

namespace Taiyo.App.JobApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Taiyo.JobSchedule.InventorySchedule schedule = new JobSchedule.InventorySchedule();
        public MainWindow()
        {
            InitializeComponent();


            Taiyo.Tool.LogHelper.Log("Application start");

        }

    

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            schedule.StartJob();
        }

       
    }
}

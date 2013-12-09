using System;
using System.Collections.Generic;
using System.Linq;
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

using System.IO;
using System.Data;

namespace CANViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SerialLog log;
        private SerialLogViewModel logViewModel;

        public MainWindow()
        {
            InitializeComponent();

            log = new SerialLog();
            logViewModel = new SerialLogViewModel(log);

            DataContext = logViewModel;

            log.Start();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            log.Start();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            log.Stop();
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            log.Stop();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace FuelDistributors
{
    public delegate void DistributorsDataCallback(List<DistributorHandler> distributors);
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const float D1Price = 3.60f;

        public List<DistributorHandler> Distributors { get; private set; }
        public BackgroundWorker Worker;

        private DistributorsDataCallback _callback;

        public MainWindow()
        { 
            InitializeComponent();
            InitializeDistributors();
        }

        public MainWindow(DistributorsDataCallback callbackDelegate)
        {
            InitializeComponent();
            InitializeDistributors();
            _callback = callbackDelegate;
        }

        private void InitializeDistributors()
        {
            Distributors = new List<DistributorHandler>
            {
                new DistributorHandler("Dystrybutor 1"),
                new DistributorHandler("Dystrybutor 2"),
                new DistributorHandler("Dystrybutor 3")
            };
            DataContext = Distributors;
            Worker = new BackgroundWorker();
            Worker.WorkerSupportsCancellation = true;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int btnCode = btn.GetHashCode();
            if (btnCode == d1Start.GetHashCode())
            {
                if (!Worker.IsBusy)
                {
                    Worker.DoWork += new DoWorkEventHandler((o, args) => worker_DoWork(o, args, 0));
                    Worker.RunWorkerAsync();
                    d1Start.Visibility = Visibility.Hidden;
                    d1Stop.Visibility = Visibility.Visible;
                }
            }
            else if (btnCode == d1Stop.GetHashCode())
            {
                Worker.CancelAsync();
                d1Start.Visibility = Visibility.Visible;
                d1Stop.Visibility = Visibility.Hidden;
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs doWorkEventArgs, int distIndex)
        {
            while (!Worker.CancellationPending)
            {
                Distributors[distIndex].Volume += DistributorHandler.FuelAtOnce;
                Distributors[distIndex].TotalPrice += DistributorHandler.FuelAtOnce*D1Price;
                _callback(Distributors);
                System.Threading.Thread.Sleep(80);
            }
            doWorkEventArgs.Cancel = true;
        }
    }
}

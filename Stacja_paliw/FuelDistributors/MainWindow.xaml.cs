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
        public List<BackgroundWorker> Workers;

        private readonly DistributorsDataCallback _callback;

        public List<DistributorHandler> Distributors { get; private set; }

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
                new DistributorHandler("E95",             
                    new FuelTank (100.0, 2.1, 5.2), 5.79f),
                new DistributorHandler("E98", 
                    new FuelTank (100.0, 3.0, 6.0), 5.99f),
                new DistributorHandler("ON", 
                    new FuelTank (80.0, 1.5, 5.0), 5.59f),
                new DistributorHandler("LPG", 
                    new FuelTank(80.0, 3.0, 5.0), 2.89f)
            };
            DataContext = Distributors;
            Workers = new List<BackgroundWorker>();
            foreach (var distributor in Distributors)
            {
                Workers.Add(new BackgroundWorker {WorkerSupportsCancellation = true});
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int btnCode = btn.GetHashCode();
            int btnIndex = Int32.Parse(btn.Name.Substring(1, 1));

            if (btnCode == ((Button)FindName("d" + btnIndex + "Start")).GetHashCode())
            {
                if (!Workers[btnIndex-1].IsBusy)
                {
                    Workers[btnIndex - 1].DoWork += new DoWorkEventHandler((o, args) => worker_DoWork(o, args, btnIndex-1));
                    Workers[btnIndex - 1].RunWorkerAsync();
                    btn.Visibility = Visibility.Hidden;
                    ((Button)FindName("d"+btnIndex+"Stop")).Visibility = Visibility.Visible;                
                }
            }
            else if (btnCode == ((Button)FindName("d" + btnIndex + "Stop")).GetHashCode())
            {
                Workers[btnIndex - 1].CancelAsync();
                ((Button)FindName("d" + btnIndex + "Start")).Visibility = Visibility.Visible;
                btn.Visibility = Visibility.Hidden;
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs doWorkEventArgs, int distIndex)
        {
            var distributor = Distributors[distIndex];
            distributor.IsBusy = true;
            while (!Workers[distIndex].CancellationPending)
            {
                distributor.Volume += DistributorHandler.FuelAtOnce;
                distributor.TotalPrice += DistributorHandler.FuelAtOnce*distributor.DetailedPrice;
                distributor.FuelTank.FuelLevel -= DistributorHandler.FuelAtOnce;
                distributor.FuelTank.GenerateParamethers();
                _callback(Distributors);
                System.Threading.Thread.Sleep(80);
            }
            doWorkEventArgs.Cancel = true;
            distributor.IsBusy = false;
        }
    }
}

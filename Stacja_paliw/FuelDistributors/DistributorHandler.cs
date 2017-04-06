using System.ComponentModel;
using System.Runtime.CompilerServices;
using FuelDistributors.Annotations;

namespace FuelDistributors
{
    public class DistributorHandler : INotifyPropertyChanged
    {
        public const float FuelAtOnce = 0.02f;

        private volatile float _volume;
        private volatile float _totalPrice;

        public event PropertyChangedEventHandler PropertyChanged;

        #region ---Properties---

        public float Volume
        {
            get { return _volume; }
            set {
                if (_volume != value)
                {
                    _volume = value;
                    OnPropertyChanged(nameof(Volume));
                }
            }
        }

        public float TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                if (_totalPrice != value)
                {
                    _totalPrice = value;
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        public string DistributorName { get;  }

        public float DetailedPrice { get; }

        public bool IsBusy{ get; set; }

        public FuelTank FuelTank { get;}

        #endregion

        public DistributorHandler(string distributorName, FuelTank tank, float price)
        {
            Volume = 0.0f;
            TotalPrice = 0.0f;
            DistributorName = distributorName;
            FuelTank = tank;
            DetailedPrice = price;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ResetDistributor()
        {
            Volume = 0.0f;
            TotalPrice = 0.0f;
            FuelTank.MakeSafe();
        }
    }
}

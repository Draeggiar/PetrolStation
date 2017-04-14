
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stacja_paliw.Areas.Worker.Models
{
    public class TransactionData
    {
        public double Volume;
        public double TotalPrice;

        public TransactionData(double volume, double totalPrice)
        {
            Volume = volume;
            TotalPrice = totalPrice;
        }
    }
}
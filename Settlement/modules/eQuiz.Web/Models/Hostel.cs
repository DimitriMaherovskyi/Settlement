using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Settlement.Web.Models
{
    public class Hostel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }
        public int MonthPaymentSum { get; set; }

        public Hostel (int id, int number, string address, int monthPaymentSum )
        {
            Id = id;
            Number = number;
            Address = address;
            MonthPaymentSum = monthPaymentSum;
        }
    }
}
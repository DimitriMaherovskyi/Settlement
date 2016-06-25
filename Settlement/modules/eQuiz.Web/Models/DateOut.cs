using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Settlement.Web.Models
{
    public class DateOut
    {
        public int Id { get; set; }
        public DateTime OutDate { get; set; }

        public DateOut(int id, DateTime dateOut)
        {
            Id = id;
            OutDate = dateOut;
        }
    }
}
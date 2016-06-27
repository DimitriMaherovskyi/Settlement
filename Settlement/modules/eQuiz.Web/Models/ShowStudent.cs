using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Settlement.Web.Models
{
    public class ShowStudent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public int? HostelNum { get; set; }
        public int? RoomNum { get; set; }

        public ShowStudent (int id, string name, int rating)
        {
            Id = id;
            Name = name;
            Rating = rating;
        }

        public ShowStudent (int id, string name, int rating, int? hostelNum, int? roomNum) : this(id, name, rating)
        {
            HostelNum = hostelNum;
            RoomNum = roomNum;
        }
    }
}
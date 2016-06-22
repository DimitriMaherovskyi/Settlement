using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Settlement.Web.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int AmountPlaces { get; set; }
        public int RoomFloor { get; set; }

        public Room(int id, int number, int amountPlaces, int roomFloor)
        {
            Id = id;
            number = Number;
            amountPlaces = AmountPlaces;
            RoomFloor = roomFloor;
        }
    }
}
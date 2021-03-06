﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Settlement.Web.Models
{
    public static class AutoSettle
    {
        private static List<SettleRoom> freeRooms = new List<SettleRoom>();

        private static List<SettleStudent> students = new List<SettleStudent>();

        public static List<SettleRoom> FreeRooms
        {
            get
            {
                return freeRooms;
            }

            set
            {
                freeRooms = value;
            }
        }

        public static List<SettleStudent> Students
        {
            get
            {
                return students;
            }

            set
            {
                students = value;
            }
        }

        public static void SettleStudents()
        {
            GetFreeRooms();
            SetRatings();
            OrderStudentsByRating();
            SetRoomGenders();

            for (var i = 0; i < students.Count; i++)
            {
                for (var j = 0; j < freeRooms.Count; j++)
                {
                    if (freeRooms[j].Students.Count == 0)
                    {
                        freeRooms[j].Students.Add(students[i]);
                        freeRooms[j].PlacesLeft -= 1;
                        students[i].RoomId = freeRooms[j].Id;
                        SetRoomGenders();
                        break;
                    }

                    if (freeRooms[j].PlacesLeft > 0 && students[i].Gender == freeRooms[j].Gender)
                    {
                        freeRooms[j].Students.Add(students[i]);
                        freeRooms[j].PlacesLeft -= 1;
                        students[i].RoomId = freeRooms[j].Id;
                        break;
                    }
                }
            }
        }

        public static void OrderStudentsByRating()
        {
            Students = students.OrderByDescending(s => s.Rating).ToList();
        }

        public static void SetRatings()
        {
            foreach (var stud in students)
            {
                SettleStudent.CountRating(stud);
            }
        }

        private static void SetRoomGenders()
        {
            foreach (var room in freeRooms)
            {
                SettleRoom.SetGender(room);
            }
        }

        private static void GetFreeRooms()
        {
            var count = freeRooms.Count;

            for (var i = 0; i < count; i++)
            {
                if (freeRooms[i].PlacesLeft < 1)
                {
                    freeRooms.Remove(freeRooms[i]);
                    count -= 1;
                    i -= 1;
                }
            }
        }
    }
}
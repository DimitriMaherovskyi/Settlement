using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Settlement.Web.Models
{
    public class SettleStudent
    {
        const int maxLivingDistance = 400;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Institute { get; set; }
        public string Gender { get; set; }
        public int Distance { get; set; }
        public int BenefitPoints { get; set; }
        public int Rating { get; set; }

        public SettleStudent (int id, string name, string institute, string gender, int distance, int benefitPoints)
        {
            Id = id;
            Name = name;
            Institute = institute;
            Gender = gender;
            Distance = distance;
            BenefitPoints = benefitPoints;
        }

        public static void CountRating(SettleStudent student)
        {
            if (student.Distance > maxLivingDistance)
            {
                student.Rating = Rate(maxLivingDistance, student.BenefitPoints);
            }
            else
            {
                student.Rating = Rate(student.Distance, student.BenefitPoints);
            }
        }

        static int Rate(int distance, int benefit)
        {
            return distance + benefit;
        }
    }
}
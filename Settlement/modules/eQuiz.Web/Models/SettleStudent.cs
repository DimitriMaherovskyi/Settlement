using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Settlement.Web.Models
{
    public enum Sex
    {
        female, male
    }

    public class SettleStudent : IEquatable<SettleStudent>
    {
        const int maxLivingDistance = 400;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Institute { get; set; }
        public string Gender { get; set; }
        public int Distance { get; set; }
        public string LivingPlace { get; set; }
        public int BenefitPoints { get; set; }
        public int Rating { get; set; }
        public int? RoomId { get; set; }
        public int RequestId { get; set; }
        public SettleStudent (int id, string name, string institute, bool gender, string livingPlace, int distance, int benefitPoints, int requestId)
        {
            Id = id;
            Name = name;
            Institute = institute;
            Gender = SetGender(gender);
            LivingPlace = livingPlace;
            Distance = distance;
            BenefitPoints = benefitPoints;
            RequestId = requestId;
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

        public override int GetHashCode()
        {
            return Id.ToString().GetHashCode() + Name.GetHashCode();
        }

        private string SetGender(bool gender)
        {
            if (Convert.ToInt32(gender) == (int)Sex.female)
            {
                return "Female";
            }

            return "Male";
        }

        private static int Rate(int distance, int benefit)
        {
            return distance + benefit;
        }

        public bool Equals(SettleStudent other)
        {
            if (other.Id == Id && other.Name == Name)
            {
                return true;
            }

            return false;
        }
    }
}
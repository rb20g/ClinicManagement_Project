using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library.Clinic.Models
{
    public class Treatment
    {
        public override string ToString() //Need this to print the actual names and not the fully qualified assembly name
        {
            return $"[{Id}] {Name} ${Price}";
            //return "Patient " + PatientId + " has an appointment with " + PhysicianId + " at " + Start + " to " + End;
        }

        public string Display //Need this to print the actual names and not the fully qualified assembly name
        {
            get
            {
                return $"[{Id}] {Name} ${Price}";
            }
           
            //return "Patient " + PatientId + " has an appointment with " + PhysicianId + " at " + Start + " to " + End;
        }
        public int Id { get; set; }
        private string? name;  //field, data member 
        public string Name
        {
            get
            {
                return name ?? string.Empty;
            }

            set
            {
                name = value;
            }
        }
        public double Price { get; set; }
        public Treatment()
        {
            Name = string.Empty;
            Price = 0;
        }
    }
}

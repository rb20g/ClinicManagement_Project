using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library.Clinic.Models
{
    public class Appointment
    {
        public override string ToString() //Need this to print the actual names and not the fully qualified assembly name
        {
            return $"[{Id}] {PatientId} {PhysicianId} {Start} {End}";
            //return "Patient " + PatientId + " has an appointment with " + PhysicianId + " at " + Start + " to " + End;
        }
        public int Id { get; set; }  //in every models to grab one and only one object of that type (in this case patient)
                                     //Guid puts in a hash function, don't have to check for collisons, easier to troubleshoot with in ID's 
        /*private string? name;  //field, data member 
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
        }*/

        public DateTime Start {  get; set; }
        public DateTime End { get; set; }
        public int PatientId { get; set; }
        public int PhysicianId { get; set; }
   

        

        public Appointment()
        {
            Start = DateTime.MinValue;
            End = DateTime.MinValue;
            PatientId = 0;
            PhysicianId = 0;
        }

    }
}


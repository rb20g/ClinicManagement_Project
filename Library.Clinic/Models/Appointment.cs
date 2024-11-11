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
        public Appointment() { }
        public override string ToString() //Need this to print the actual names and not the fully qualified assembly name
        {
            return $"[{Id}] {Patient} {Physician} {StartTime} {EndTime}";
            //return "Patient " + PatientId + " has an appointment with " + PhysicianId + " at " + Start + " to " + End;
        }
        public int Id { get; set; }  //in every models to grab one and only one object of that type (in this case patient)
                                     //Guid puts in a hash function, don't have to check for collisons, easier to troubleshoot with in ID's 

        public DateTime? StartTime {  get; set; }
        public DateTime? EndTime { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
        public int PhysicianId { get; set; }
        public Physician? Physician { get; set; }
   

        
        /*
        public Appointment()
        {
            Start = DateTime.MinValue;
            StartString = string.Empty;
            End = DateTime.MinValue;
            EndString = string.Empty;   
            PatientId = 0;
            PhysicianId = 0;
        }*/

    }
}


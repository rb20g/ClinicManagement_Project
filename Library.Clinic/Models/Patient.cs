using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
{
    public class Patient    //this is a data class
    {
        public int Id { get; set; }  //in every models to grab one and only one object of that type (in this case patient)
        //Guid puts in a hash function, don't have to check for collisons, easier to troubleshoot with in ID's 
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
        public DateOnly Birthday { get; set; }
        public string Address { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public string MedicalNotes { get; set; }

        public override string ToString() //Need this to print the actual names and not the fully qualified assembly name
        {
            //List<Patient> total = new List<Patient>(Name, Address, Birthday, Race, Gender);
            return Name + " " + Address + " " + Birthday + " " + Race + " " + Gender + " " + MedicalNotes;
        }

        public Patient()
        {
            name = string.Empty;
            Address = string.Empty;
            Birthday = DateOnly.MinValue;
            Race = string.Empty;
            Gender = string.Empty;
            MedicalNotes = string.Empty;
        }
    }
}

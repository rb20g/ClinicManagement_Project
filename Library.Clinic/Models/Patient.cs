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
        public override string ToString() //Need this to print the actual names and not the fully qualified assembly name
        {
            return $"[{Id}] {Name} {Address} {Birthday} {Race} {Gender} {MedicalNotes} {Insurance}";
            //return Name + " " + Address + " " + Birthday + " " + Race + " " + Gender + " " + MedicalNotes;
        }
        public string Display
        {
            get
            {
                return $"[{Id}] {Name} {Address} {Birthday} {Race} {Gender} {MedicalNotes} {Insurance}";
            }
        }

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
        /*
        public String BirthdayString
        {
            get
            {
                return Birthday.ToString();
            }

            set
            {
                DateTime.Parse(Birthday.ToString());
            }
        }*/
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public string MedicalNotes { get; set; }
        public int InsuranceId { get; set; }
        public Insurance? Insurance {  get; set; }




        public Patient()
        {
            Name = string.Empty;
            Address = string.Empty;
            Birthday = DateTime.MinValue;
            Race = string.Empty;
            Gender = string.Empty;
            MedicalNotes = string.Empty;
        }
    }
}

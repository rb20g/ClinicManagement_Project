using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.DTO
{
    public class PhysicianDTO
    {
        public override string ToString() //Need this to print the actual names and not the fully qualified assembly name
        {
            return $"[{Id}] {Name} {LicenseNumber} {GraduationDate} {Specialization}";
            //return Name + " " + Address + " " + Birthday + " " + Race + " " + Gender + " " + MedicalNotes;
        }
        public string Display
        {
            get
            {
                return $"[{Id}] {Name} {LicenseNumber} {GraduationDate} {Specialization}";
            }
        }

        public int Id { get; set; }  //in every models to grab one and only one object of that type (in this case patient)
                                     //Guid puts in a hash function, don't have to check for collisons, easier to troubleshoot with in ID's 
        public string? Name { get; set; }
        public string? LicenseNumber { get; set; }
        public DateTime GraduationDate { get; set; }
        public string? Specialization { get; set; }

        public PhysicianDTO() { }
        public PhysicianDTO(Physician p)
        {
            Id = p.Id;
            Name = p.Name;
            LicenseNumber = p.LicenseNumber;
            GraduationDate = p.GraduationDate;
            Specialization = p.Specialization;
        }

    }
}

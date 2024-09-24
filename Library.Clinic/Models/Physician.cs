﻿using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library.Clinic.Models
{
    public class Physician
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

        public string LicenseNumber { get; set; }
        public DateOnly GraduationDate { get; set; }
        public string Specialization { get; set; }

        public override string ToString() //Need this to print the actual names and not the fully qualified assembly name
        {
            return Name + " " + LicenseNumber + " " + GraduationDate + " " + Specialization;
        }

        public Physician()
        {
            name = string.Empty;
            LicenseNumber = string.Empty;
            GraduationDate = DateOnly.MinValue;
            Specialization = string.Empty;
        }
        
    }
}
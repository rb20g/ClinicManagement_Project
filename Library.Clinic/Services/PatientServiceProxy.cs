using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Services   //behavior role, where the behavior for the data goes
{
    public class PatientServiceProxy
    {
        private static object _lock = new object();
        public static PatientServiceProxy Current
        {
            get
            {
                lock(_lock)
                {
                    if (instance == null)
                    {
                        instance = new PatientServiceProxy();
                    }
                }
                return instance;
            }
        }
        private static PatientServiceProxy? instance;
        private PatientServiceProxy()
        {
            instance = null;
        }
        public int LastKey
        {
            get
            {
                if (Patients.Any())
                {
                    return Patients.Select(x => x.Id).Max();
                }
                return 0;
            }
        }
        //field version: List<Patient> patients;
        public List<Patient> Patients { get; private set; } = new List<Patient>();


        public void AddPatient(Patient patient) //responsible for constructing the list, but the application is responsible for constructing the individual objects 
        {
            if (patient.Id <= 0)
            {
                patient.Id = LastKey + 1;              //if added the patient before, will add it again, but if never added patient before, the ID is always going to be 0, actually assign it a new 0 
            }
            Patients.Add(patient);
        }
        
        
        public int GetPatient(string PatName)
        {
            for(int i = 0; i < Patients.Count; i++)
            {
                if(Patients[i].Name == PatName)
                {
                    return Patients[i].Id;
                }
                
            }
            return 0;
        }
        
        
    }
}
using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Services
{
    public class PhysicianServiceProxy
    {
        private static object _lock = new object();
        public static PhysicianServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new PhysicianServiceProxy();
                    }
                }
                return instance;
            }
        }
        private static PhysicianServiceProxy? instance;
        private PhysicianServiceProxy()
        {
            instance = null;
        }
        public int LastKey
        {
            get
            {
                if (Physicians.Any())
                {
                    return Physicians.Select(x => x.Id).Max();
                }
                return 0;
            }
        }
        
        public List<Physician> Physicians { get; private set; } = new List<Physician>();


        public void AddPhysician(Physician physician) //responsible for constructing the list, but the application is responsible for constructing the individual objects 
        {
            if (physician.Id <= 0)
            {
                physician.Id = LastKey + 1;               
            }
            Physicians.Add(physician);
        }

        public int GetPhysician(string PhyName)
        {
            for (int i = 0; i < Physicians.Count; i++)
            {
                if (Physicians[i].Name == PhyName)
                {
                    return Physicians[i].Id;
                }

            }
            return 0;
        }

        /*
        public void AddAppointment(Physician physician, DateTime appointment)  //Need to make into a model
        {
            if(AppointmentAvailable(physician, appointment))
            {
                for(int i = 0; i < Physicians.Count; i++)
                {
                    if (Physicians[i].Name == physician.Name)
                    {
                        Physicians[i].Appointment.Add(appointment);
                        Console.WriteLine("Appointment successfully created");
                    }
                }
                
            }
        }

        public bool AppointmentAvailable(Physician physician, DateTime appointment)
        {
            TimeSpan EightAM = new TimeSpan(8, 0, 0);
            TimeSpan FivePM = new TimeSpan(17, 0, 0);
            for (int i = 0; i < Physicians.Count; i++)
            {
                if (Physicians[i].Name == physician.Name)
                {
                    if (Physicians[i].Appointment.Contains(appointment))
                    {
                        Console.WriteLine("Physican has an appointment at this time");
                        return false;
                    }
                    else if(appointment.DayOfWeek == DayOfWeek.Saturday || appointment.DayOfWeek == DayOfWeek.Sunday)
                    {
                        Console.WriteLine("Physican is available for appointments Monday thru Friday");
                        return false;
                    }
                    else if(appointment.TimeOfDay < EightAM || appointment.TimeOfDay >= FivePM)
                    {
                        Console.WriteLine("Physican is avaliable for appointments from 8:00 to 17:00");
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
            }
            Console.WriteLine("There is no physician with that name in our database");
            return false;

        }*/
    }
}


using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Services
{
    public class AppointmentServiceProxy
    {
        private static object _lock = new object();
        public static AppointmentServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new AppointmentServiceProxy();
                    }
                }
                return instance;
            }
        }
        private static AppointmentServiceProxy? instance;
        private AppointmentServiceProxy()
        {
            instance = null;
        }
        public int LastKey
        {
            get
            {
                if (Appointments.Any())
                {
                    return Appointments.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        public List<Appointment> Appointments { get; private set; } = new List<Appointment>();


        public void AddAppointment(Appointment appointment)  //Need to make into a model
        {
            if (AppointmentAvailable(appointment))
            {
                if (appointment.Id <= 0)
                {
                    appointment.Id = LastKey + 1;              //if added the patient before, will add it again, but if never added patient before, the ID is always going to be 0, actually assign it a new 0 
                }
                Appointments.Add(appointment);
                Console.WriteLine("Appointment Successfully created");
            }
        }

        public bool AppointmentAvailable(Appointment appointment)
        {
            TimeSpan EightAM = new TimeSpan(8, 0, 0);
            TimeSpan FivePM = new TimeSpan(17, 0, 0);
            if(appointment.PatientId > 0 && appointment.PhysicianId > 0)
            {
                for(int i = 0; i < Appointments.Count; i++)
                {
                    if (Appointments[i].PhysicianId == appointment.PhysicianId && appointment.Start.Date == Appointments[i].Start.Date) 
                    {
                        if (DateTime.Compare(Appointments[i].Start, appointment.Start) <= 0 && DateTime.Compare(Appointments[i].End, appointment.Start) >= 0)
                        {
                            Console.WriteLine("That Physican has an appointment at that time");
                            return false;
                        }
                        else if(DateTime.Compare(Appointments[i].Start, appointment.End) <= 0 && DateTime.Compare(Appointments[i].End, appointment.End) >= 0)
                        {
                            Console.WriteLine("That Physican has an appointment at that time");
                            return false;
                        }
                            
                    }
                    else if(appointment.Start.DayOfWeek == DayOfWeek.Saturday || appointment.Start.DayOfWeek == DayOfWeek.Sunday)
                    {
                        Console.WriteLine("Physican is not available for appointments on the weekend");
                        return false;
                    }
                    else if(appointment.Start.TimeOfDay < EightAM || appointment.Start.TimeOfDay >= FivePM)
                    {
                        Console.WriteLine("Physican is avaliable for appointments from 8:00 to 17:00");
                        return false;
                    }
                }
                return true;
            }
            else 
            {
                Console.WriteLine("There is no Physician/Patient with that name in our database");
                return false;
            }
        }
       
    }
}

using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Services
{
    public class AppointmentServiceProxy
    {
        private static object _lock = new object();
        private int lastKey
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
        public List<Appointment> Appointments { get; private set; }

        private static AppointmentServiceProxy _instance;
        public static AppointmentServiceProxy Current
        {
            get
            {

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new AppointmentServiceProxy();
                    }
                }
                return _instance;
            }
        }

        private AppointmentServiceProxy()
        {
            Appointments = new List<Appointment> {
                new Appointment {Id = 1
                , StartTime = new DateTime(2024, 10, 9)
                , EndTime = new DateTime(2024, 10, 9)
                , PatientId = 1}
            };
        }

        public void AddOrUpdateAppointment(Appointment a)
        {
            var isAdd = false;
            if (a.Id <= 0)
            {
                if(AppointmentAvailable(a))
                {
                    a.Id = lastKey + 1;
                    isAdd = true;
                }
            }
            if (isAdd)
            {
                Appointments.Add(a);
            }

        }

        public bool AppointmentAvailable(Appointment appointment)
        {
            TimeSpan EightAM = new TimeSpan(8, 0, 0);
            TimeSpan FivePM = new TimeSpan(17, 0, 0);

            if (appointment.PatientId > 0 && appointment.PhysicianId > 0 && appointment.StartTime.HasValue && appointment.EndTime.HasValue)
            {
                DateTime appointmentStart = appointment.StartTime.Value;
                DateTime appointmentEnd = appointment.EndTime.Value;

                // Check if appointment is on a weekend
                if (appointmentStart.DayOfWeek == DayOfWeek.Saturday || appointmentStart.DayOfWeek == DayOfWeek.Sunday)
                {
                    return false;
                }

                // Check if appointment is within working hours
                if (appointmentStart.TimeOfDay < EightAM || appointmentEnd.TimeOfDay > FivePM)
                {
                    return false;
                }

                foreach (var existingAppointment in Appointments)
                {
                    // Ensure the physician and date match
                    if (existingAppointment.PhysicianId == appointment.PhysicianId &&
                        existingAppointment.StartTime?.Date == appointmentStart.Date)
                    {
                        DateTime existingStart = existingAppointment.StartTime.Value;
                        DateTime existingEnd = existingAppointment.EndTime.Value;

                        // Check if there is an overlap with another appointment
                        if ((appointmentStart >= existingStart && appointmentStart < existingEnd) ||
                            (appointmentEnd > existingStart && appointmentEnd <= existingEnd))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            return false;
        }

        public void DeleteAppointment(int id)
        {
            var appointmentToRemove = Appointments.FirstOrDefault(p => p.Id == id);
            if (appointmentToRemove != null)
            {
                Appointments.Remove(appointmentToRemove);
            }
        }

    }
}

/*
TimeSpan EightAM = new TimeSpan(8, 0, 0);
TimeSpan FivePM = new TimeSpan(17, 0, 0);
if(appointment.PatientId > 0 && appointment.PhysicianId > 0)
{
    for(int i = 0; i < Appointments.Count; i++)
    {
        if (Appointments[i].PhysicianId == appointment.PhysicianId && appointment.StartTime?.Date == Appointments[i].StartTime?.Date) 
        {
            if (DateTime.Compare(Appointments[i].StartTime, appointment.StartTime) <= 0 && DateTime.Compare(Appointments[i].EndTime, appointment.StartTime) >= 0)
            {
                //Console.WriteLine("That Physican has an appointment at that time");
                return false;
            }
            else if(DateTime.Compare(Appointments[i].StartTime, appointment.EndTime) <= 0 && DateTime.Compare(Appointments[i].EndTime, appointment.EndTime) >= 0)
            {
                //Console.WriteLine("That Physican has an appointment at that time");
                return false;
            }

        }
        else if(appointment.StartTime.DayOfWeek == DayOfWeek.Saturday || appointment.StartTime.DayOfWeek == DayOfWeek.Sunday)
        {
            //Console.WriteLine("Physican is not available for appointments on the weekend");
            return false;
        }
        else if(appointment.StartTime.TimeOfDay < EightAM || appointment.StartTime.TimeOfDay >= FivePM)
        {
            //Console.WriteLine("Physican is avaliable for appointments from 8:00 to 17:00");
            return false;
        }
    }
    return true;
}
else 
{
    //Console.WriteLine("There is no Physician/Patient with that name in our database");
    return false;
}
}*/



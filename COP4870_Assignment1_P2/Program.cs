using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Globalization;
namespace MyApp
//namespace: lives inside that physical assembly file, groups items inside the file that do the same thing
//virtual grouping of things inside a physical file, namespaces are pieces of assembly
//assembly: C# projects will ultimatley compile into an assembly, is a physical file
{
    internal class Program
    {
        //Dependencies tab contains all of our libraries the program is using
        //Solution files work as makefile 
        static void Main(string[] args)
        {
            //List<string> patients = new List<string>();

            bool isContinue = true;
            do
            {
                Console.WriteLine("Select an option.");
                Console.WriteLine("A. Add a Patient");   //WriteLine = cout in C++
                Console.WriteLine("B. Add a Physician");
                Console.WriteLine("C. Make Appointment");
                Console.WriteLine("D. Show List of Patients");
                Console.WriteLine("E. Show List of Physicians");
                Console.WriteLine("F. Show List of Appointments");
                Console.WriteLine("G. Delete Patient");
                Console.WriteLine("Q. Quit");

                string input = Console.ReadLine() ?? string.Empty;

                if (char.TryParse(input, out char choice)) //reference parameter in C++, can only use choice inside this if line
                {

                    switch (choice)
                    {
                        case 'a':
                        case 'A':
                            Console.WriteLine("Patients name: ");
                            var PatientName = Console.ReadLine();  //need to decouple the list from the application
                            Console.WriteLine("Patients address: ");
                            var PatientAddress = Console.ReadLine();
                            Console.WriteLine("Patients birth date as MM/DD/YYYY: ");
                            DateOnly PatientBirthday;
                            DateOnly.TryParse(Console.ReadLine(), out PatientBirthday);
                            //DateOnly.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out PatientBirthday);
                            Console.WriteLine("Patients race: ");
                            var PatientRace = Console.ReadLine();
                            Console.WriteLine("Patients gender: ");
                            var PatientGender = Console.ReadLine();
                            Console.WriteLine("Any patient medical notes (diagnoses/prescriptions): ");
                            var PatientMedicalNotes = Console.ReadLine();
                            var newPatient = new Patient { Name = PatientName ?? string.Empty, Address = PatientAddress ?? string.Empty, Birthday = PatientBirthday, Race = PatientRace ?? string.Empty, Gender = PatientGender ?? string.Empty, MedicalNotes = PatientMedicalNotes ?? string.Empty };
                            PatientServiceProxy.Current.AddOrUpdatePatient(newPatient);
                            break;

                        case 'b':
                        case 'B':
                            Console.WriteLine("Physicians name: ");
                            var PhysicianName = Console.ReadLine();
                            Console.WriteLine("Physicians license number: ");
                            var PhysicianLicenseNum = Console.ReadLine();
                            Console.WriteLine("Physicians graduation date as MM/DD/YYYY: ");
                            DateOnly PhysicianGradDate;
                            DateOnly.TryParse(Console.ReadLine(), out PhysicianGradDate);
                            //DateOnly.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out PhysicianGradDate);
                            Console.WriteLine("Physicians specialization: ");
                            var PhysicianSpecial = Console.ReadLine();
                            var newPhysician = new Physician { Name = PhysicianName ?? string.Empty, LicenseNumber = PhysicianLicenseNum ?? string.Empty, GraduationDate = PhysicianGradDate, Specialization = PhysicianSpecial ?? string.Empty };
                            PhysicianServiceProxy.Current.AddPhysician(newPhysician);
                            break;

                        case 'c':
                        case 'C':
                            Console.WriteLine("Name of Patient making the appointment: ");
                            string PatName = Console.ReadLine() ?? string.Empty;
                            //Patient CurrentPatient = new Patient { Name = PatName ?? string.Empty };
                            Console.WriteLine("Name of Physician you would like to make an appointment with: ");
                            var PhName = Console.ReadLine() ?? string.Empty;
                            //var CurrentPhysician = new Physician { Name= PhName ?? string.Empty };
                            Console.WriteLine("Date and Time of appointment as MM/DD/YYYY HH:MM: ");
                            DateTime AppStart;
                            DateTime.TryParse(Console.ReadLine(), out AppStart);
                            DateTime AppEnd = AppStart.AddHours(1);
                            var newAppointment = new Appointment { Start = AppStart, End = AppEnd, PatientId = PatientServiceProxy.Current.GetPatient(PatName), PhysicianId = PhysicianServiceProxy.Current.GetPhysician(PhName) };
                            AppointmentServiceProxy.Current.AddAppointment(newAppointment);
                            break;

                        case 'd':
                        case 'D':
                            foreach (var patient in PatientServiceProxy.Current.Patients)
                            {
                                Console.WriteLine($"{patient}");
                            }
                            break;

                        case 'e':
                        case 'E':
                            foreach (var physician in PhysicianServiceProxy.Current.Physicians)
                            {
                                Console.WriteLine($"{physician}");
                            }
                            break;

                        case 'f':
                        case 'F':
                            foreach (var appointment in AppointmentServiceProxy.Current.Appointments)
                            {
                                Console.WriteLine($"{appointment}");
                            }
                            break;

                        case 'g':
                        case 'G':
                            PatientServiceProxy.Current.Patients.ForEach(x => Console.WriteLine($"{x.Id}. {x.Name}"));
                            int selectedPatient = int.Parse(Console.ReadLine() ?? "-1");
                            PatientServiceProxy.Current.DeletePatient(selectedPatient);
                            break;

                        case 'q':
                        case 'Q':
                            isContinue = false;
                            break;

                        default:
                            Console.WriteLine("That is an invalid choice!"); 
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(choice + " is not a char");
                }
            } while (isContinue);  //enumerated loop, safest way to iterate through a list, but will break if add or move a patient from patients in this loop (changes enumeration)

            
          
        }
       
    }
}


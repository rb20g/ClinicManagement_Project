﻿using Library.Clinic.DTO;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace App.Clinic.ViewModels
{
    public class AppointmentViewModel
    {
        public Appointment? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }

        public int Id
        {
            get
            {
                if (Model == null)
                {
                    return -1;
                }
                return Model.Id;
            }

            set
            {
                if (Model != null && Model.Id != value)
                {
                    Model.Id = value;
                }
            }
        }


        public string PatientName
        {
            get
            {
                if (Model != null && Model.PatientId > 0)
                {
                    if (Model.Patient == null)
                    {
                        Model.Patient = PatientServiceProxy
                            .Current
                            .Patients
                            .FirstOrDefault(p => p.Id == Model.PatientId);
                    }
                }

                return Model?.Patient?.Name ?? string.Empty;
            }
        }

        public string PatientInsurance
        {
            get
            {
                if (Model != null && Model.PatientId > 0)
                {
                    if (Model.Patient == null)
                    {
                        Model.Patient = PatientServiceProxy
                            .Current
                            .Patients
                            .FirstOrDefault(p => p.Id == Model.PatientId);
                    }
                }

                return Model?.Patient?.Insurance?.Name ?? string.Empty;
            }
        }

        public Patient? SelectedPatient
        {
            get
            {
                return Model?.Patient;
            }

            set
            {
                var selectedPatient = value;
                if (Model != null)
                {
                    Model.Patient = selectedPatient;
                    Model.PatientId = selectedPatient?.Id ?? 0;
                }

            }
        }

        public string PhysicianName
        {
            get
            {
                if (Model != null && Model.PhysicianId > 0)
                {
                    if (Model.Physician == null)
                    {
                        Model.Physician = PhysicianServiceProxy
                            .Current
                            .Physicians
                            .FirstOrDefault(p => p.Id == Model.PhysicianId);
                    }
                }

                return Model?.Physician?.Name ?? string.Empty;
            }
        }

        public PhysicianDTO? SelectedPhysician
        {
            get
            {
                return Model?.Physician;
            }

            set
            {
                var selectedPhysician = value;
                if (Model != null)
                {
                    Model.Physician = selectedPhysician;
                    Model.PhysicianId = selectedPhysician?.Id ?? 0;
                }

            }
        }

        public string TreatmentName
        {
            get
            {
                if (Model != null && Model.TreatmentId > 0)
                {
                    if (Model.Treatment == null)
                    {
                        Model.Treatment = TreatmentServiceProxy
                            .Current
                            .Treatments
                            .FirstOrDefault(p => p.Id == Model.TreatmentId);
                    }
                }

                return Model?.Treatment?.Name ?? string.Empty;
            }
        }

        public Treatment? SelectedTreatment
        {
            get
            {
                return Model?.Treatment;
            }

            set
            {
                var selectedTreatment = value;
                if (Model != null)
                {
                    Model.Treatment = selectedTreatment;
                    Model.TreatmentId = selectedTreatment?.Id ?? 0;
                }

            }
        }
        public string InsuranceName
        {
            get
            {
                if (Model != null && Model.Patient?.InsuranceId > 0)
                {
                    if (Model.Insurance == null)
                    {
                        Model.Insurance = InsuranceServiceProxy
                            .Current
                            .Insurances
                            .FirstOrDefault(p => p.Id == Model.Patient.InsuranceId);
                    }
                }

                return Model?.Insurance?.Name ?? string.Empty;
            }
        }

        public double TreatmentPrice
        {
            get
            {
                if (Model != null && Model.TreatmentId > 0)
                {
                    if (Model.Treatment?.Price == null)
                    {
                        Model.Treatment = TreatmentServiceProxy
                            .Current
                            .Treatments
                            .FirstOrDefault(p => p.Id == Model.TreatmentId);
                    }
                }
                return Model?.Treatment?.Price ?? double.NaN;
            }
        }

        public double InsurancePrice
        {
            get
            {
                if (Model != null && TreatmentPrice > 0)
                {
                    InsuranceServiceProxy.Current.AddOrUpdateTreatmentPrice(Model.Insurance, TreatmentPrice);
                }
                return Model?.Insurance?.DiscountPrice ?? double.NaN;
            }

            
        }

        public Insurance? SelectedInsurance
        {
            get
            {
                return Model?.Insurance;
            }

            set
            {
                var selectedInsurance = value;
                if (Model != null)
                {
                    Model.Insurance = selectedInsurance;
                    Model.Patient.InsuranceId = selectedInsurance?.Id ?? 0;
                }

            }
        }

        public ObservableCollection<Insurance> Insurances
        {
            get
            {
                return new ObservableCollection<Insurance>(InsuranceServiceProxy.Current.Insurances);
            }
        }


        public ObservableCollection<Patient> Patients
        {
            get
            {
                return new ObservableCollection<Patient>(PatientServiceProxy.Current.Patients);
            }
        }

        public ObservableCollection<PhysicianDTO> Physicians
        {
            get
            {
                return new ObservableCollection<PhysicianDTO>(PhysicianServiceProxy.Current.Physicians);
            }
        }

        public ObservableCollection<Treatment> Treatments
        {
            get
            {
                return new ObservableCollection<Treatment>(TreatmentServiceProxy.Current.Treatments);
            }
        }

        public DateTime MinStartDate
        {
            get
            {
                return DateTime.Today;
            }
        }

        public void RefreshTime()
        {
            if (Model != null)
            {
                /*
                if(Model.StartTime.HasValue)
                {
                    Model.StartTime = StartDate.Date + StartTime;
                    Model.EndTime = Model.StartTime.Value.AddHours(1);
                }*/
               
                if (Model.StartTime != null)
                {
                    Model.StartTime = StartDate.Date + StartTime;
                    Model.EndTime = Model.StartTime.Value.AddHours(1);
                    /*
                    Model.StartTime = StartDate;
                    Model.StartTime = Model.StartTime.Value.AddHours(StartTime.Hours);
                    Model.EndTime = Model.StartTime.Value.AddHours(1);*/
                }
            }
        }

        public DateTime StartDate
        {

            get
            {
                return Model?.StartTime?.Date ?? DateTime.Today;
            }

            set
            {
                if (Model != null)
                {
                    Model.StartTime = value;
                    RefreshTime();
                }
            }
        }
        public TimeSpan StartTime { get; set; }

        public DateTime EndDate
        {

            get
            {
                return Model?.EndTime?.Date ?? DateTime.Today;
            }

            set
            {
                if (Model != null)
                {
                    Model.EndTime = value;
                    RefreshTime();
                }
            }
        }
        public TimeSpan EndTime { get; set; }

        public AppointmentViewModel()
        {
            Model = new Appointment();
            SetupCommands();
        }
        public AppointmentViewModel(Appointment? a)
        {
            Model = a;
            SetupCommands();
        }

        public void SetupCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as AppointmentViewModel));
        }
        public void DoDelete()
        {
            if (Id > 0)
            {
                AppointmentServiceProxy.Current.DeleteAppointment(Id);
            }
            Shell.Current.GoToAsync("//Appointments");
        }

        public void DoEdit(AppointmentViewModel? avm)
        {
            if (avm == null)
            {
                return;
            }
            var selectedAppointmentId = avm?.Id ?? 0;
            Shell.Current.GoToAsync($"//AppointmentDetails?appointmentId={selectedAppointmentId}");
        }

        public void ExecuteAdd()
        {
            if (Model != null)
            {
                AppointmentServiceProxy.Current.AddOrUpdateAppointment(Model);
            }
            Shell.Current.GoToAsync("//Appointments");

        }
    }
}
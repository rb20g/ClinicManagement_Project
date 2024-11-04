﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using App.Clinic.Views;
using Library.Clinic.Models;
using Library.Clinic.Services;

namespace App.Clinic.ViewModels
{
    public class PatientViewModel
    {
        public Patient? Model { get; set; }
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

        public string Name
        {
            get => Model?.Name ?? string.Empty;

            set
            {
                if (Model != null)
                {
                    Model.Name = value;
                }
            }
        }
        
        public string Address
        {
            get => Model?.Address ?? string.Empty;

            set
            {
                if (Model != null)
                {
                    Model.Address = value;
                }
            }
        }

        public DateTime MinStartDate
        {
            get
            {
                return new DateTime(1920, 1, 1);
            }
        }

        public DateTime Birthday
        {
            get
            {
                return Model?.Birthday.Date ?? DateTime.Today;

            }
            set
            {
                if (Model != null)
                {
                    Model.Birthday = value;
                }
            }
        }

        public string Race
        {
            get => Model?.Race ?? string.Empty;

            set
            {
                if (Model != null)
                {
                    Model.Race = value;
                }
            }
        }

        public string Gender
        {
            get => Model?.Gender ?? string.Empty;

            set
            {
                if (Model != null)
                {
                    Model.Gender = value;
                }
            }
        }

        public string MedicalNotes
        {
            get => Model?.MedicalNotes ?? string.Empty;

            set
            {
                if (Model != null)
                {
                    Model.MedicalNotes = value;
                }
            }
        }

        public void SetupCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as PatientViewModel));
        }

        public void DoDelete()
        {
            if (Id > 0)
                PatientServiceProxy.Current.DeletePatient(Id);
            Shell.Current.GoToAsync("//Patients");
        }

        public void DoEdit(PatientViewModel? pvm)
        {
            if (pvm == null)
            {
                return;
            }
            var selectedPatientId = pvm?.Id ?? 0;
            Shell.Current.GoToAsync($"//PatientDetails?patientId={selectedPatientId}");
        }

        public PatientViewModel()
        {
            Model = new Patient();
            //now if Model is null, we know something went wrong
            SetupCommands();
        }

        public PatientViewModel(Patient? _model) //conversion constructer for Patient Model to PatientViewModel
        {
            Model = _model;
            SetupCommands();
        }

        public void ExecuteAdd()
        {
            if (Model != null)
            {
                PatientServiceProxy
                    .Current
                    .AddOrUpdatePatient(Model);
            }

            Shell.Current.GoToAsync("//Patients");
        }
    }
}

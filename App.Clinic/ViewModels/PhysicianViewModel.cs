using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using App.Clinic.Views;
using Library.Clinic.DTO;
using Library.Clinic.Models;
using Library.Clinic.Services;

namespace App.Clinic.ViewModels
{
    public class PhysicianViewModel
    {
        public PhysicianDTO? Model { get; set; }
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

        public string LicenseNumber
        {
            get => Model?.LicenseNumber ?? string.Empty;

            set
            {
                if (Model != null)
                {
                    Model.LicenseNumber = value;
                }
            }
        }

        public DateTime MinStartDate
        {
            get
            {
                return new DateTime(1980, 1, 1);
            }
        }

        public DateTime GraduationDate
        {
            get
            {
                return Model?.GraduationDate.Date ?? DateTime.Today;

            }
            set
            {
                if (Model != null)
                {
                    Model.GraduationDate = value;
                }
            }
        }

        public string Specialization
        {
            get => Model?.Specialization ?? string.Empty;

            set
            {
                if (Model != null)
                {
                    Model.Specialization = value;
                }
            }
        }

        public void SetupCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as PhysicianViewModel));
        }

        public void DoDelete()
        {
            if (Id > 0)
                PhysicianServiceProxy.Current.DeletePhysician(Id);
            Shell.Current.GoToAsync("//Physicians");
        }

        public void DoEdit(PhysicianViewModel? pvm)
        {
            if (pvm == null)
            {
                return;
            }
            var selectedPhysicianId = pvm?.Id ?? 0;
            Shell.Current.GoToAsync($"//PhysicianDetails?physicianId={selectedPhysicianId}");
        }

        public PhysicianViewModel()
        {
            Model = new PhysicianDTO();
            //now if Model is null, we know something went wrong
            SetupCommands();
        }

        public PhysicianViewModel(PhysicianDTO? _model) //conversion constructer for Patient Model to PatientViewModel
        {
            Model = _model;
            SetupCommands();
        }

        public void ExecuteAdd()
        {
            if (Model != null)
            {
                PhysicianServiceProxy
                    .Current
                    .AddOrUpdatePhysician(Model);
            }

            Shell.Current.GoToAsync("//Physicians");
        }
    }
}

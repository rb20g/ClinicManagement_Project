using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Library.Clinic.Models;
using Library.Clinic.Services;

namespace App.Clinic.ViewModels
{
    public class TreatmentViewModel
    {
        public Treatment? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }

        public int TreatmentId
        {
            get
            {
                if (Model == null)
                {
                    return -1; 
                }

                return Model.TreatmentId;
            }

            set
            {
                if(Model != null && Model.TreatmentId != value)
                {
                    Model.TreatmentId = value;
                }
            }
        }

        public string Name
        {
            get => Model?.Name ?? string.Empty;
            set
            {
                if(Model != null)
                {
                    Model.Name = value;
                }
            }
        }

        public double Price
        {
            get => Model?.Price ?? 0;

            set
            {
                if (Model != null)
                {
                    Model.Price = value;
                }
            }
        }

        public void SetupCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as TreatmentViewModel));
        }

        private void DoDelete()
        {
            if (TreatmentId > 0)
            {
                TreatmentServiceProxy.Current.DeleteTreatment(TreatmentId);
                Shell.Current.GoToAsync("//Patients");
            }
        }

        private void DoEdit(TreatmentViewModel? tvm)
        {
            if(tvm == null)
            {
                return;
            }
            var selectedTreatmentId = tvm?.TreatmentId ?? 0;
            Shell.Current.GoToAsync($"//TreatmentDetails?treatmentId={selectedTreatmentId}");
        }

        public TreatmentViewModel()
        {
            Model = new Treatment();
            SetupCommands();
        }

        public TreatmentViewModel(Treatment? _model)
        {
            Model = _model;
            SetupCommands();
        }

        public void ExecuteAdd()
        {
            if (Model != null)
            {
                TreatmentServiceProxy
                    .Current
                    .AddOrUpdateTreatments(Model);
            }

            Shell.Current.GoToAsync("//Treatments");
        }
    }
}

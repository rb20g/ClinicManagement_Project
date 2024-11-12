﻿using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace App.Clinic.ViewModels
{
    public class TreatmentManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private TreatmentServiceProxy _TSvc = TreatmentServiceProxy.Current;

        public ObservableCollection<TreatmentViewModel> Treatments
        {
            get
            {
                return new ObservableCollection<TreatmentViewModel>(
                    _TSvc.Treatments.Select(a => new TreatmentViewModel(a)));
            }

        }
        public TreatmentViewModel? SelectedTreatment { get; set; }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Treatments));
        }
    }
}

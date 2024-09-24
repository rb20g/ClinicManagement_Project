using System.ComponentModel;
using Library.Clinic.Services;
using Library.Clinic.Models;
using App.Clinic.ViewModels;
namespace App.Clinic.Views;

public partial class AppointmentManagement : ContentPage, INotifyPropertyChanged
{
    public AppointmentManagement()
    {
        InitializeComponent();
        BindingContext = new AppointmentManagementViewModel();
    }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//AppointmentDetails?appointmentId=0");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        var selectedAppointmentId = (BindingContext as AppointmentManagementViewModel)?
            .SelectedAppointment?.Id ?? 0;
        Shell.Current.GoToAsync($"//AppointmentDetails?appointmentId={selectedAppointmentId}");
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as AppointmentManagementViewModel)?.Delete();
    }

    private void AppointmentManagement_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as AppointmentManagementViewModel)?.Refresh();
    }
}
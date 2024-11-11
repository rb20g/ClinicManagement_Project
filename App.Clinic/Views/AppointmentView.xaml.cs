using Library.Clinic.Models;
using Library.Clinic.Services;
using App.Clinic.ViewModels;

namespace App.Clinic.Views;

[QueryProperty(nameof(AppointmentId), "appointmentId")]
public partial class AppointmentView : ContentPage
{
	public AppointmentView()
	{
        InitializeComponent();
        BindingContext = new AppointmentViewModel();
    }

	public int AppointmentId { get; set; }

	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Appointments");
	}
	
	private void AddClicked(object sender, EventArgs e)
	{
		(BindingContext as AppointmentViewModel)?.ExecuteAdd();
		/*
		var appointmentToAdd = BindingContext as Appointment;
		if (appointmentToAdd != null)
		{
			AppointmentServiceProxy
				.Current
				.AddOrUpdateAppointment(appointmentToAdd);
		}
		Shell.Current.GoToAsync("//Appointments");*/
	}

    private void TimePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        (BindingContext as AppointmentViewModel)?.RefreshTime();
    }

    private void AppointmentView_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		if(AppointmentId > 0)
		{
			var model = AppointmentServiceProxy.Current
				.Appointments.FirstOrDefault(p => p.Id == AppointmentId);
			if (model != null)
			{
				BindingContext = new AppointmentViewModel(model);
			}
			else
			{
				BindingContext = new AppointmentViewModel(); 
			}
		}
		else
		{
			BindingContext = new AppointmentViewModel();
		}
	}
}
using Library.Clinic.Models;
using Library.Clinic.Services;
using App.Clinic.ViewModels;

namespace App.Clinic.Views;

[QueryProperty(nameof(PatientId), "patientId")]
public partial class PatientView : ContentPage
{
	public PatientView()
	{
        InitializeComponent();
        //BindingContext = new PatientManagementViewModel();
    }

	public int PatientId { get; set; }

	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Patients");
	}

	private void AddClicked(object sender, EventArgs e)
	{
		var patientToAdd = BindingContext as Patient;
		if (patientToAdd != null)
		{
			PatientServiceProxy
				.Current
				.AddOrUpdatePatient(patientToAdd);
		}
		Shell.Current.GoToAsync("//Patients");
	}

	private void PatientView_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		if(PatientId > 0)
		{
			BindingContext = PatientServiceProxy.Current
				.Patients.FirstOrDefault(p => p.Id == PatientId);
		}
		else
		{
			BindingContext = new Patient();
		}
	}
}
using App.Clinic.ViewModels;
using System.ComponentModel;

namespace App.Clinic.Views;

public partial class TreatmentManagementView : ContentPage, INotifyPropertyChanged
{
	public TreatmentManagementView()
	{
        InitializeComponent();
		BindingContext = new TreatmentManagementViewModel();
	}

	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}

	private void AddClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//TreatmentDetails");
	}

    private void EditClicked(object sender, EventArgs e)
    {
        var selectedTreatmentId = (BindingContext as TreatmentManagementViewModel)?
            .SelectedTreatment?.Id ?? 0;
        Shell.Current.GoToAsync($"//TreatmentDetails?treatmentId={selectedTreatmentId}");
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as TreatmentManagementViewModel)?.Delete();
    }

    private void RefreshClicked(object sender, EventArgs e)
    {
        (BindingContext as TreatmentManagementViewModel)?.Refresh();
    }

    private void TreatmentManagement_NavigatedTo(object sender, NavigationEventArgs e)
	{
		(BindingContext as TreatmentManagementViewModel)?.Refresh();
	}

 
}
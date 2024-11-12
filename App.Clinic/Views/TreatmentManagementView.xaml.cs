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

	private void RefreshClicked(object sender, EventArgs e)
	{
		(BindingContext as TreatmentManagementViewModel)?.Refresh();
	}
}
using App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;

namespace App.Clinic.Views;

public partial class TreatmentView : ContentPage
{
	public TreatmentView()
	{
		InitializeComponent();
		BindingContext = new TreatmentViewModel();
	}

    public int TreatmentId { get; set; }

    private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Treatments");
	}

	private void AddClicked(object sender, EventArgs e)
	{
		(BindingContext as TreatmentViewModel)?.ExecuteAdd();
	}
 
    private void TreatmentView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (TreatmentId > 0)
        {
            var model = TreatmentServiceProxy.Current
                .Treatments.FirstOrDefault(p => p.TreatmentId == TreatmentId);

            if (model != null)
            {
                BindingContext = new TreatmentViewModel(model);
            }
            else
            {
                BindingContext = new TreatmentViewModel();
            }
        }

        else
        {
            BindingContext = new TreatmentViewModel();
        }
    }


}
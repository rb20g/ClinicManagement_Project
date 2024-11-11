using Library.Clinic.Models;
using Library.Clinic.Services;
using App.Clinic.ViewModels;

namespace App.Clinic.Views;

[QueryProperty(nameof(PhysicianId), "physicianId")]
public partial class PhysicianView : ContentPage
{
    public PhysicianView()
    {
        InitializeComponent();
    }

    public int PhysicianId { get; set; }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Physicians");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        (BindingContext as PhysicianViewModel)?.ExecuteAdd();
    }

    private void PhysicianView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (PhysicianId > 0)
        {
            var model = PhysicianServiceProxy.Current
                .Physicians.FirstOrDefault(p => p.Id == PhysicianId);
        
            if (model != null)
            {
                BindingContext = new PhysicianViewModel(model);
            }
            else
            {
                BindingContext = new PhysicianViewModel();
            }
        }

        else
        {
            BindingContext = new PhysicianViewModel();
        }
    }
}
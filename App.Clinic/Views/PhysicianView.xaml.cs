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
        BindingContext = new PhysicianManagementViewModel();
    }

    public int PhysicianId { get; set; }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Physicians");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        var physicianToAdd = BindingContext as Physician;
        if (physicianToAdd != null)
        {
            PhysicianServiceProxy
                .Current
                .AddOrUpdatePhysician(physicianToAdd);
        }
        Shell.Current.GoToAsync("//Physicians");
    }

    private void PhysicianView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (PhysicianId > 0)
        {
            BindingContext = PhysicianServiceProxy.Current
                .Physicians.FirstOrDefault(p => p.Id == PhysicianId);
        }
        else
        {
            BindingContext = new Physician();
        }
    }
}
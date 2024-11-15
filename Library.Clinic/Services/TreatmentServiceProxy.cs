using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Services
{
    public class TreatmentServiceProxy
    {
        private static object _lock = new object();
        private int LastKey
        {
            get
            {
                if (Treatments.Any())
                {
                    return Treatments.Select(x => x.Id).Max();
                }
                return 0;
            }
        }
        public List<Treatment> Treatments { get; private set; }
        private static TreatmentServiceProxy? instance;
        public static TreatmentServiceProxy Current  //do this to deal with static 
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new TreatmentServiceProxy();
                    }
                }
                return instance;
            }
        }

        
        private TreatmentServiceProxy()
        {

            Treatments = new List<Treatment>
            {
                new Treatment{Id = 1, Name = "Cold and flu symptoms", Price = 50.00}
                , new Treatment{Id = 2, Name = "COVID-19 vaccination", Price = 190.00}
            };
        }
 
        /*
        private List<Treatment> treatments;
        //singleton
        public List<Treatment> Treatments
        {
            get
            {
                return treatments;
                //make sure hitting break point
            }
            private set
            {
                if (treatments != value)
                {
                    treatments = value;
                }
            }
        }*/

        public void AddOrUpdateTreatments(Treatment treatment) //responsible for constructing the list, but the application is responsible for constructing the individual objects 
        {
            bool isAdd = false;
            if (treatment.Id <= 0)
            {
                treatment.Id = LastKey + 1;              //if added the patient before, will add it again, but if never added patient before, the ID is always going to be 0, actually assign it a new 0 
                isAdd = true;
                //this explicitly tells when something should be added or not, we go into the function with an assumption that this is an updated
                //if we are proven wrong (that is if the Id is something 0 or less than 0) we are going to assign an Id, and at the point of 
                //assigning a Id, we know that this is an Add, and we need to put a new thing into the list
            }
            if (isAdd)
            {
                Treatments.Add(treatment);
            }
        }

        public void DeleteTreatment(int id)
        {
            var treatmentToRemove = Treatments.FirstOrDefault(p => p.Id == id);  //this grabs the first patient with this idea (and there should only be 1 since they're unique) or default
            if (treatmentToRemove != null)
            {
                Treatments.Remove(treatmentToRemove);
            }

        }

        public int GetTreatment(string TreatmentName)
        {
            for (int i = 0; i < Treatments.Count; i++)
            {
                if (Treatments[i].Name == TreatmentName)
                {
                    return Treatments[i].Id;
                }

            }
            return 0;
        }

    }
}
  

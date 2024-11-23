using Library.Clinic.DTO;
using Library.Clinic.Models;
using Newtonsoft.Json;
using PP.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Services
{
    public class PhysicianServiceProxy
    {
        private static object _lock = new object();
        public static PhysicianServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new PhysicianServiceProxy();
                    }
                }
                return instance;
            }
        }
        private static PhysicianServiceProxy? instance;
        private PhysicianServiceProxy()
        {
            instance = null;

            var physiciansData = new WebRequestHandler().Get("/Physician").Result;

            Physicians = JsonConvert.DeserializeObject<List<PhysicianDTO>>(physiciansData) ?? new List<PhysicianDTO>();
        }
        public int LastKey
        {
            get
            {
                if (Physicians.Any())
                {
                    return Physicians.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        private List<PhysicianDTO> physicians;
        public List<PhysicianDTO> Physicians
        {
            get
            {
                return physicians;
            }
            private set
            {
                if (physicians != value)
                {
                    physicians = value;
                }
            }
        }


        public void AddOrUpdatePhysician(PhysicianDTO physician) //responsible for constructing the list, but the application is responsible for constructing the individual objects 
        {
            bool isAdd = false;
            if (physician.Id <= 0)
            {
                physician.Id = LastKey + 1;
                isAdd = true;
            }
            if (isAdd)
            {
                Physicians.Add(physician);
            }

        }

        public async void DeletePhysician(int id)
        {
            var physicianToRemove = Physicians.FirstOrDefault(p => p.Id == id);
            if (physicianToRemove != null)
            {
                Physicians.Remove(physicianToRemove);

                await new WebRequestHandler().Delete($"/Physician/{id}");
            }
        }

        public int GetPhysician(string PhyName)
        {
            for (int i = 0; i < Physicians.Count; i++)
            {
                if (Physicians[i].Name == PhyName)
                {
                    return Physicians[i].Id;
                }

            }
            return 0;
        }

    }
}


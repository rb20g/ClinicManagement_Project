using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Services
{
    public class InsuranceServiceProxy
    {
        private static object _lock = new object();
        private int lastKey
        {
            get
            {
                if (Insurances.Any())
                {
                    return Insurances.Select(x => x.Id).Max();
                }
                return 0;
            }
        }
        public List<Insurance> Insurances { get; set; }

        private static InsuranceServiceProxy _instance;
        public static InsuranceServiceProxy Current
        {
            get
            {

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new InsuranceServiceProxy();
                    }
                }
                return _instance;
            }
        }

        public InsuranceServiceProxy()
        {
            Insurances = new List<Insurance> {
                new Insurance {Id = 1
                , Name = "Blue Cross Blue Shield"
                , DiscountRate = 0.75 },
                new Insurance {Id = 2
                , Name = "UnitedHealthcare"
                , DiscountRate = 0.75 },
                new Insurance {Id = 3
                , Name = "Aetna"
                , DiscountRate = 0.5 },
                new Insurance {Id = 4
                , Name = "Elevance Health"
                , DiscountRate = 0.5 },
                new Insurance {Id = 5
                , Name = "Centene"
                , DiscountRate = 0.25 },
                new Insurance {Id = 6
                , Name = "Kaiser Permanente"
                , DiscountRate = 0.25 }
            };
        }

        public async Task<List<Insurance>> GetInsurancePlans()
        {
            return Insurances;
        }
        public void AddOrUpdateTreatmentPrice(Insurance i, double t)
        {
            if (i.DiscountRate != 0)
            {
                double Discount = t * i.DiscountRate;
                i.DiscountPrice = t - Discount;
            }
            else
            {
                i.DiscountPrice = t;
            }
        }
    }
}

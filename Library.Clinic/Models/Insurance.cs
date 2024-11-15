using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
{
    public class Insurance
    {
        public int Id { get; set; }
        private string? name;  //field, data member 
        public string Name
        {
            get
            {
                return name ?? string.Empty;
            }

            set
            {
                name = value;
            }
        }
        public double DiscountRate;
        public double DiscountPrice;
        public Insurance()
        {
            Name = string.Empty;
            DiscountRate = 0;
            DiscountPrice = 0;
        }
    }
}

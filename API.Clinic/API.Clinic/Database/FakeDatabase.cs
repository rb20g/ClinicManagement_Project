using Library.Clinic.Models;
using System.Runtime.CompilerServices;

namespace API.Clinic.Database
{
    static public class FakeDatabase
    {
        public static int LastKey
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

        private static List<Physician> physicians = new List<Physician>
                {
                    new Physician{Id = 1, Name = "Robert Smith", LicenseNumber = "24380", GraduationDate = new DateTime(2024, 10, 9), Specialization = "Ear, Nose, and Throat" }
                    , new Physician{Id = 2, Name = "Johnny Bravo", LicenseNumber = "7006001", GraduationDate = new DateTime(2020, 12, 9), Specialization = "Optometrist" }
                };
        public static List<Physician> Physicians
        {
            get
            {
                return physicians;
            }
        }

        public static Physician? AddOrUpdatePhysician(Physician? physician)
        {
            if (physician == null)
            {
                return null;
            }

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

            return physician;
        }
    }
}

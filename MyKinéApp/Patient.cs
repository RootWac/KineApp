using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKinéApp
{
    class Patient
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string CIN { get; private set; }
        public string Gender { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime Birthday { get; private set; }
        public string Address { get; private set; }
        public double Height { get; private set; }
        public double Weight { get; private set; }

        public int ID;
        string Antecedent;
        public string Diagnostic;
        public string BalanceSheet;
        public string Treatment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Lastname"></param>
        /// <param name="CIN"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="Birthday"></param>
        /// <param name="Adress"></param>
        /// <param name="Taille"></param>
        /// <param name="Weight"></param>
        public Patient(string Name, string LastName, string CIN, string PhoneNumber, string Address, DateTime Birthday, double Height, double Weight, string Gender)
        {
            this.Name = Name;
            this.LastName = LastName;
            this.CIN = CIN;
            this.PhoneNumber = PhoneNumber;
            this.Birthday = Birthday;
            this.Address = Address;
            this.Height = Height;
            this.Weight = Weight;
            this.Gender = Gender;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update_ClinicalFollowUp()
        {
            DataBase.UpdatePatient_ClinicalFollowUp(this);
        }
    }
}

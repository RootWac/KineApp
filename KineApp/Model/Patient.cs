using KineApp.Controller;
using System;
using System.Collections.Generic;
using System.Text;

namespace KineApp.Model
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string CIN { get; set; }
        public string Phone { get; set; }
        public char Gender { get; set; } = 'F';
        public DateTime DateOfBirth { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Note { get; set; } = "Ancune note ajoute au dossier";

        public int Age
        {
            get { return (int)((DateTime.Now - DateOfBirth).TotalDays / 365); }
        }

        public string CompleteName
        {
            get { return LastName + " " + FirstName; }
        }

        public Dictionary<int, Record> AllRecords = new Dictionary<int, Record>();
        public Record CurrentRecord;
        internal List<PatientFiles> AdditionalFiles = new List<PatientFiles>();

        public Patient()
        {

        }


        public Patient(int Id, string FirstName, string LastName, string Address, string CIN, string Phone, DateTime DateOfBirth, char Gender, int Height, int Weight)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Address = Address;
            this.CIN = CIN;
            this.DateOfBirth = DateOfBirth;
            this.Phone = Phone;
            this.Height = Height;
            this.Weight = Weight;
            this.Gender = Gender;
        }

        public Patient(string FirstName, string LastName, string Address, string CIN, string Phone, DateTime DateOfBirth, char Gender, int Height, int Weight)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Address = Address;
            this.CIN = CIN;
            this.DateOfBirth = DateOfBirth;
            this.Phone = Phone;
            this.Height = Height;
            this.Weight = Weight;
            this.Gender = Gender;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        internal void CloseRecord()
        {
            DateTime CloseDate = DateTime.Now;
            if(CurrentRecord.Finish(CloseDate))
            {
                //AllRecords.Add(CurrentRecord.Id, CurrentRecord);
                CurrentRecord = null; 
            }
        }
    }
}

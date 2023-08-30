using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using KineApp.Controller;

namespace KineApp.Model
{
    public enum DiscountEnum
    {
        None,
        Percentage,
        Argent
    }

    public class Transaction {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Rate { get; set; }
        public double Discount { get; private set; }
        public DiscountEnum DiscountType { get; private set; } = DiscountEnum.None;
        public double Amount { get
            {
                switch(DiscountType)
                {
                    case DiscountEnum.Argent:
                        return Rate - Discount;

                    case DiscountEnum.Percentage:
                        return Rate - (Rate * Discount / 100);
                }
                    
                return Rate;
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        public bool isPaied { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="discount"></param>
        /// <param name="discountValue"></param>
        public void Pay(DiscountEnum discount = DiscountEnum.None, double discountValue = 0, bool fromDB = false, string comment = "")
        {
            // if pay method called after manual click
            if (!fromDB)
                isPaied = Data.PayrollUpdate(Id, discount, discountValue, comment, Amount);
            else // value readen from the database directely
                isPaied = true;


            if (discountValue > 0)
            {
                DiscountType = discount;
                Discount = discountValue;
            }
        }
    }
}

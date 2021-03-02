using System;
using System.Collections.Generic;
using System.Text;

namespace Unitest
{
    public class Pub:IPub
    {
        private ICheckInFee _checkInFee;
        private decimal _inCome = 0;
        public Pub(ICheckInFee checkInFee) 
        {
            this._checkInFee = checkInFee;
        }

        public int CheckIn(List<Customer> customers) 
        {
            var result = 0;
            foreach (var customer in customers) 
            {
                var isFemale = !customer.IsMale;
                if (isFemale)
                {
                    continue;
                }
                else 
                {
                    this._inCome += this._checkInFee.GetFree(customer);
                    result++;
                }
            }
            return result;
        }
        public decimal GetInCome => this._inCome;

        public void AddCustomer(bool isMale,int seq) 
        {
            _checkInFee.AddOne(new Customer() {IsMale =isMale,Seq= seq});
    }

    }
}

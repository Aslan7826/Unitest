using System;
using System.Collections.Generic;
using System.Text;

namespace Unitest
{
    public interface ICheckInFee
    {
        decimal GetFree(Customer customer);

        void AddOne(Customer customer);

        void Del(Customer customer);


    }
}

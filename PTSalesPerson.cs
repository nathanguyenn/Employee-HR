/*
 * GROUP 2
 * ASSIGNMENT 9
 * Csd 228 - c#
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    [System.Serializable]
    sealed class PTSalesPerson : SalesPerson
    {
        public PTSalesPerson(string first, string last, DateTime age, float currPay, 
                             string ssn, int numbOfSales)
          : base(first, last, age, currPay, ssn, numbOfSales)
        {
        }
        // Assume other members here...
    }
}

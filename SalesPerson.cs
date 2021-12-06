/*
 * GROUP 2
 * ASSIGNMENT 9
 * Csd 228 - c#
 */
using System;
using System.Collections;

namespace Employees
{
    // Salespeople need to know their number of sales.
    [System.Serializable]
    class SalesPerson : Employee
    {
        #region constructors 
        public SalesPerson() { }

        // As a general rule, all subclasses should explicitly call an appropriate
        // base class constructor.
        public SalesPerson(string first, string last, DateTime age, float currPay, 
                           string ssn, int numbOfSales)
          : base(first, last, age, currPay, ssn)
        {
            // This belongs with us!
            salesNumber = numbOfSales;
        }
        #endregion

        protected int salesNumber; 
        public int SalesNumber { get { return salesNumber; } }
        // Spare prop1 name, default value, and max value
        protected static string prop1Name = "Sales Number:";

        #region methods
        // Add Employee spare prop1 name and default value
        public static string SpareAddProp1Name() { return prop1Name; }
        public static object SpareAddProp1DefaultValue() { return 0; }
        //override spare prop 1 
        public override void GetSpareProp1(ref string name, ref object value)
        {
            name = prop1Name;
            value = SalesNumber;
        }

        // Convert passed value for spare prop1 to a valid value
        // Return null if value cannot be converted
        public static object SpareAddProp1Convert(object obj)
        {
            if (obj is int) return obj;
            else if (obj is string)
            {
                string s = (string)obj;
                int value;

                if (int.TryParse(s, out value)) return value;
            }

            // Not a valid value
            return null;
        }

        // Check if passed object is a valid value for spare prop1
        // Return error string if not valid, else return String.Empty (if valid)
        public static string SpareAddProp1Valid(object obj)
        {
            if (obj is string)
            {
                string s = (string)obj;
                int value;

                if (int.TryParse(s, out value) && value >= 0 )
                    return String.Empty;
            }

            // Error message for invalid values
            return String.Format("Range is 0 and above");
        }
        #region old methods
        // A salesperson's bonus is influenced by the number of sales.
        public override sealed void GiveBonus(float amount)
        {
            int salesBonus = 0;
            if (SalesNumber >= 0 && SalesNumber <= 100)
                salesBonus = 10;
            else
            {
                if (SalesNumber >= 101 && SalesNumber <= 200)
                    salesBonus = 15;
                else
                    salesBonus = 20;
            }
            base.GiveBonus(amount * salesBonus);
        }

        // A SalesPerson gets an extra 300 on promotion
        public override sealed void GivePromotion()
        {
            base.GivePromotion();
            GiveBonus(300);
        }

        public override void DisplayStats()
        {
            base.DisplayStats();
            Console.WriteLine("Number of sales: {0:N0}", SalesNumber);
        }
        #endregion

        #endregion
    }
}

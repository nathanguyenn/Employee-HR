/*
 * GROUP 2
 * ASSIGNMENT 9
 * Csd 228 - c#
 */
using System;

namespace Employees
{
    public enum ExpenseCategory { Conference, Lodging, Meals, Misc, Travel }

    [Serializable]
    public class Expense
    {
        #region Data members / properties
        public DateTime Date { get; set; } = DateTime.Today;
        public ExpenseCategory Category { get; set; }
        public double Amount { get; set; }
        // add description for description field 
        public string Description { get; set; }
        #endregion

        #region Constructors
        public Expense() { }

        public Expense(DateTime expDate, ExpenseCategory category, double amount, string des)
        {
            Description = des;
            Date = expDate;
            Category = category;
            Amount = amount;
        }
        #endregion
    }
}

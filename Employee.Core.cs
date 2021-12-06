/*
 * GROUP 2
 * ASSIGNMENT 8
 * Csd 228 - c#
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace Employees
{
    [Serializable]
    public abstract partial class Employee 
    {
        public static int NamespaceLength = 10;
        // Field data.
        private string first;
        private string last;
        //private string empName;
        private int empID;
        private float currPay;
        private DateTime empDOB;
        private string empSSN;
        public virtual string Role { get { return GetType().ToString().Substring(10); } }
        public List<Expense> Expenses { get; set; } = new List<Expense>();
        public virtual string BenefitString
        {
            get
            {
                return Benefits.ToString();
            }
        }
        // Used to create unique IDs.
        static int NextId = 1;

        #region Constructors
        public Employee() {
            empID = NextId++;
        }

        public Employee(string first,string last, DateTime birthday, float pay, string ssn) :this()
        {

            // Catch Employee ID overflow
            if (NextId == Int32.MaxValue)
                throw new System.OverflowException("Employee ID has reached max value");

            this.first = first;
            this.last = last;
            empDOB = birthday;
            currPay = pay;
            empSSN = ssn;
        }
        #endregion

        #region Properties 
        public string FirstName { get { return first; } } 
        public string LastName { get { return last; } }
        public string Name { get { return first + " " + last; } }
        public int ID { get { return empID; } }
        public float Pay { get { return currPay; } }
        public int Age { get { return (DateTime.Now.Year - empDOB.Year); } }
        public DateTime DateOfBirth { get { return empDOB; } }
        public string SocialSecurityNumber { get { return empSSN; } }
        #endregion

        #region Serialization customization for NextId
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            // Called when the deserialization process is complete.
            if (empID >= NextId)
            {
                // Catch Employee ID overflow
                if (empID == Int32.MaxValue)
                    throw new System.OverflowException("Employee ID has reached max value");

                NextId = empID + 1;
            }
        }
        #endregion
    }
}

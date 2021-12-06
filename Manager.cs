/*
 * GROUP 2
 * ASSIGNMENT 9
 * Csd 228 - c#
 */
using System;
using System.Collections;
using System.Collections.Generic;

namespace Employees
{
    // Managers need to know their number of stock options and reports
    [System.Serializable]
    public class Manager : Employee, IEnumerable<Employee>
    {
        #region constructors 
        public Manager() { }

        public Manager(string first,string last, DateTime age, float currPay, string ssn, 
                       int numbOfOpts)
          : base(first,last, age, currPay, ssn)
        {
            // This property is defined by the Manager class.
            stockOptions = numbOfOpts;
        }
		#endregion

		#region Constants, data members and properties
        // Add a private member for reports
		public const int MaxReports = 5;
		private List<Employee> _reports = new List<Employee>();

        // Stock options unique to Managers
        protected int stockOptions = 10000;
        public int StockOptions { get { return stockOptions; } }

        // Spare prop1 name, default value, and max value
        protected static string prop1Name = "Stock Options:";
        
        protected static int prop1MaxValue = 10000;
        #endregion

        #region Exceptions
        // Exception raised when adding more than MaxReports to a Manager
        [System.Serializable]
        public class AddReportException : ApplicationException
        {
            // Standard exception constructors
            public AddReportException() {}
            public AddReportException(string message) 
                : base(message) {}
            public AddReportException(string message, Exception inner) 
                : base(message, inner) {}
            protected AddReportException(System.Runtime.Serialization.SerializationInfo info, 
                                  System.Runtime.Serialization.StreamingContext context) 
                : base(info, context) {}
        }
        #endregion

        #region Class Methods
        public override void GetSpareProp1(ref string name, ref object value)
        {
            name = prop1Name;
            value = StockOptions.ToString();
        }
        public override void GetSpareProp2(ref string name, ref object value)
        {
            name = "Reports";
            string names = "";
            foreach (Employee emp in _reports)
            {
                if (emp.Equals( _reports[_reports.Count - 1]))
                {
                    names += emp.Name;
                }
                else
                {
                    names += emp.Name + ", ";
                }
            }
            value = names;
        }
        // Add Employee spare prop1 name and default value
        public static string SpareAddProp1Name()
        {
            return prop1Name;
        }
        public static object SpareAddProp1DefaultValue() { return 500; }

        #region old standard methods
        // Enumerate reports for Manager
        public IEnumerator<Employee> GetEnumerator() { return _reports.GetEnumerator(); }
		IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }

        // Enumerate reports, sorted by Employee Name, Age, and Pay
		public IEnumerable<Employee> ReportsByName() { return GetReports(Employee.SortByName); }
		public IEnumerable<Employee> ReportsByAge() { return GetReports(Employee.SortByAge); }
		public IEnumerable<Employee> ReportsByPay() { return GetReports(Employee.SortByPay); }

        // Enumerator to return reports in passed sort order 
        // (null indicating no sort)
        private IEnumerable<Employee> GetReports(IComparer<Employee> sortOrder = null)
        {
            // Sort reports if sort order non-null
            if (sortOrder != null) _reports.Sort(sortOrder);

            // Enumerate reports in specified order
            return this;
		}

		// Override GiveBonus to change stock options for Manager
		public override void GiveBonus(float amount)
        {
            base.GiveBonus(amount);
            Random r = new Random();
            stockOptions += r.Next(500);
        }

        // A Manager gets an extra 600 on promotion
        public override void GivePromotion()
        {
            base.GivePromotion();
            GiveBonus(600);
        }

        // Methods for adding/removing reports
        public virtual void AddReport(Employee newReport)
        {
            // Do nothing if adding a null newReport
            if (newReport == null) return;

            // Local to hold error string, if found
            string errorString = null;

            // Check number of reports
            if (_reports.Count >= MaxReports)
                errorString = string.Format("Manager already has {0} reports.", MaxReports);
            else if (_reports.IndexOf(newReport) >= 0)
                errorString = "Employee already reports to manager";
            else if (this == newReport)
                errorString = "Manager can not report to himself/herself";

            // Create an exception if we found an error
            if (errorString != null)
            {
                Exception ex = new AddReportException(errorString);

                // Add Manager custom data dictionary
                ex.Data.Add("Manager", this.Name);

                // Also add report that failed to be added, and throw exception
                ex.Data.Add("New Report", newReport.Name);
                throw ex;

            }

            // Only add report if not already a report and not same as this
            else 
            {
                // Put Employee in empty position
                _reports.Add(newReport);
            }
        }

        public virtual void RemoveReport(Employee emp)
        {
            // Remove report
            _reports.Remove(emp);
        }

        // Display Manager with stock options and list of reports
        public override void DisplayStats()
        {
            base.DisplayStats();
            Console.WriteLine("Stock Options: {0:N0}", StockOptions);

            // Print out reports on a single line
            Console.Write("Reports: ");
			foreach (Employee emp in this)
				Console.Write("{0} ", emp.Name);
			Console.WriteLine();
        }
        #endregion

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

                if (int.TryParse(s, out value) && value >= 0 && value <= prop1MaxValue)
                    return String.Empty;
            }

            // Error message for invalid values
            return String.Format("Range is 0 to {0:N0}", prop1MaxValue);
        }
        #endregion
    }
 }
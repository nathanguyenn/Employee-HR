/*
 * GROUP 2
 * ASSIGNMENT 9
 * Csd 228 - c#
 */
using System;
using System.Collections.Generic;

namespace Employees
{
    // Executives have titles
    public enum ExecTitle { CEO, CTO, CFO, VP }

    [System.Serializable]
    public class Executive : Manager
    {
		#region constructors 
		// Executives start with Gold benefits and 10,000 stock options
		public Executive() : base()
        {
            empBenefits = new GoldBenefitPackage();
            stockOptions = 10000;          
        }

		public Executive(string first, string last, DateTime age, float currPay, 
                         string ssn, int numbOfOpts = 10000, ExecTitle title = ExecTitle.VP)
          : base(first,last, age, currPay, ssn, numbOfOpts)
        {
			// Title defined by the Executive class.
			Title = title;
            empBenefits = new GoldBenefitPackage();            
		}
        #endregion

        
        public ExecTitle Title { get; set; } = ExecTitle.VP;
        // override to add extra role 
        public override string Role => base.Role + ", " + Title;


        // add spareprop2 neccesaries things 
        // so it display in the add employee page 
        public static string SpareAddProp2Name()
        {
            return "Title";
        }
        public static object SpareAddProp2DefaultValue()
        {
            return new List<string>()
            { ExecTitle.CEO.ToString(), ExecTitle.CTO.ToString(), ExecTitle.CFO.ToString(), ExecTitle.VP.ToString() };
        }

        public static object SpareAddProp2Convert(object obj)
        {

            if (obj is int)
            {
                int s = (int)obj;
                if (s.Equals((int)ExecTitle.VP))
                {
                    return ExecTitle.VP;
                }
                else if (s.Equals((int)ExecTitle.CTO))
                {
                    return ExecTitle.CTO;
                }
                else if (s.Equals((int)ExecTitle.CFO))
                {
                    return ExecTitle.CFO;
                }
                else if (s.Equals((int)ExecTitle.CEO))
                {
                    return ExecTitle.CEO;
                }
                else
                    return null;
            }
            // Not a valid value
            return null;
        }

        #region old methods
        public override void DisplayStats()
		{
			base.DisplayStats();
			Console.WriteLine("Executive Title: {0}", Title);
		}

        // Executives get an extra 1000 bonus and 10,000 stock options on promotion
        // Move GivePromotion
        public override void GivePromotion()
        {
            base.GivePromotion();
            GiveBonus(1000);
            stockOptions += 10000;
        }
        // Methods for adding reports
        public override void AddReport(Employee newReport)
        {
            // Check for proper report to Executive
            if (newReport is Manager || newReport is SalesPerson)
            {
                base.AddReport(newReport);
            }
            else if (newReport != null)
            {
                // Raise exception for report that is not a Manager or Salesperson
                Exception ex = new AddReportException("Executive report not a Manager or Salesperson");

                // Add Manager custom data dictionary
                ex.Data.Add("Manager", this.Name);

                // Add report that failed to be added, and throw exception
                ex.Data.Add("New Report", newReport.Name);
                throw ex;
            }            
        }
        #endregion
    }
}
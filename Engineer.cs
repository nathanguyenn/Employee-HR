/*
 * GROUP 2
 * ASSIGNMENT 9
 * Csd 228 - c#
 */
using System;
using System.Collections.Generic;

namespace Employees
{
	// Engineers have degrees
    public enum DegreeName { BS, MS, PhD }

    [System.Serializable]
    public class Engineer : Employee
    {
        public DegreeName HighestDegree { get; set; } = DegreeName.BS;

        #region constructors 
        public Engineer() { }

		public Engineer(string first,string last, DateTime age, float currPay, string ssn, 
                        DegreeName degree)
          : base(first, last, age, currPay, ssn)
        {
            // This property is defined by the Engineer class.
            HighestDegree = degree;
		}
        #endregion

        protected static string prop1Name = "Highest Degree";

        public override void DisplayStats()
		{
			base.DisplayStats();
			Console.WriteLine("Highest Degree: {0}", HighestDegree);
		}
        //override spare prop 1
        public override void GetSpareProp1(ref string name, ref object value)
        {
            name = prop1Name;
            value = HighestDegree;
        }
        public static string SpareAddProp1Name() { return prop1Name; }
        public static object SpareAddProp1DefaultValue() { return new List<string>()
        { DegreeName.BS.ToString(), DegreeName.MS.ToString(), DegreeName.PhD.ToString() };
        }


        // Return null if value cannot be converted
        public static object SpareAddProp1Convert(object obj)
        {         
            if (obj is int)
            {
                int s = (int)obj;
                if (s.Equals((int)DegreeName.BS))
                {
                    return DegreeName.BS;
                }
                else if (s.Equals((int)DegreeName.MS))
                {
                    return DegreeName.MS;
                }
                else if (s.Equals((int)DegreeName.PhD))
                {;
                    return DegreeName.PhD;
                }
            }
            // Not a valid value
            return null;
        }

    }
}

/*
 * GROUP 2
 * ASSIGNMENT 9
 * Csd 228 - c#
 */
using System;
using System.Collections.Generic;

namespace Employees
{
	// SupportPerson works a shift
    public enum ShiftName { One, Two, Three }

    [System.Serializable]
    public class SupportPerson : Employee
    {
        public ShiftName Shift { get; set; } = ShiftName.One;
        protected static string prop1Name = "Shift";
        #region constructors 
        public SupportPerson() { }
        //override spare prop 1
        public override void GetSpareProp1(ref string name, ref object value)
        {
            name = prop1Name;
            value = Shift;
        }
        public SupportPerson(string first, string last, DateTime age, float currPay, 
                             string ssn, ShiftName shift)
          : base(first, last, age, currPay, ssn)
        {
            // This property is defined by the SupportPerson class.
            Shift = shift;
		}
		#endregion

		public override void DisplayStats()
		{
			base.DisplayStats();
			Console.WriteLine("Shift: {0}", Shift);
		}

        // Add Employee spare prop1 name and default value
        public static string SpareAddProp1Name()
        {
            return prop1Name;
        }
        public static object SpareAddProp1DefaultValue() { return new List<string>()
        { ShiftName.One.ToString(), ShiftName.Two.ToString(), ShiftName.Three.ToString() }; }

        // Convert passed value for spare prop1 to a valid value
        // Return null if value cannot be converted
        public static object SpareAddProp1Convert(object obj)
        {
            if (obj is int)
            {
                int s = (int)obj;
                if (s.Equals((int)ShiftName.One))
                {
                    return ShiftName.One;
                }
                else if (s.Equals((int)ShiftName.Two))
                {
                    return ShiftName.Two;
                }
                else if (s.Equals((int)ShiftName.Three))
                {
                    ;
                    return ShiftName.Three;
                }
            }
            // Not a valid value
            return null;
        }
    }
}

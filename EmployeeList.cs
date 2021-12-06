/*
 * GROUP 2
 * ASSIGNMENT 9
 * Csd 228 - c#
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Employees
{
    [Serializable]
    public class EmployeeList : List<Employee>
    {
        private const string fileName = "Employees.dat";
        private static BinaryFormatter binFormat = new BinaryFormatter();
        public EmployeeList()
        {        }

        // return EmployeeList object that has a list of employee 
        // that are loaded from file 'Employees.dat' 
        // if the file doesnt exist, we create and save the file
        // then load the file which contains said list of employees then return that 
        public static  EmployeeList GetEmployee()
        {
            EmployeeList holder = LoadEmployeesFromBinaryFile(fileName);
            if (holder == null || holder.Count < 1)
            {
                SaveEmployeesAsBinary(fileName, InitialEmployee());
                holder = LoadEmployeesFromBinaryFile(fileName);
            }
            return holder;
        }
        private static EmployeeList InitialEmployee()
        {
            Executive dan = new Executive("Dan", "Brown", DateTime.Parse("3/20/1963"), 200000, "121-12-1211", 50000, ExecTitle.CEO);
            Executive connie = new Executive("Connie", "Chung", DateTime.Parse("2/5/1972"), 150000, "229-67-7898", 40000, ExecTitle.CFO);
            Manager chucky = new Manager("Chucky", "Jones", DateTime.Parse("4/23/1967"), 100000, "333-23-2322", 9000);
            Manager mary = new Manager("Mary", "Williams", DateTime.Parse("5/9/1963"), 200000, "121-12-1211", 9500);
            Engineer bob = new Engineer("Bob", "Roe", DateTime.Parse("6/30/1986"), 120000, "334-24-2422", DegreeName.MS);
            SalesPerson fran = new SalesPerson("Fran", "Smith", DateTime.Parse("7/5/1975"), 80000, "932-32-3232", 31);
            PTSalesPerson sam = new PTSalesPerson("Sam", "Abbott", DateTime.Parse("8/11/1984"), 20000, "525-76-5030", 20);
            PTSalesPerson sally = new PTSalesPerson("Sally", "McRae", DateTime.Parse("9/12/1979"), 30000, "913-43-4343", 10);
            SupportPerson mike = new SupportPerson("Mike", "Roberts", DateTime.Parse("10/31/1975"), 15000, "229-67-7898", ShiftName.One);
            SupportPerson steve = new SupportPerson("Steve", "Kinny", DateTime.Parse("11/21/1982"), 80000, "913-43-4343", ShiftName.Two);
            Manager Jane = new Manager("Jane", "Doe", DateTime.Parse("9/23/1991"), 10000, "111-11-2342", 9000);
            Engineer bob1 = new Engineer("Bob", "Roe", DateTime.Parse("6/30/1986"), 120000, "334-24-2422", DegreeName.MS);
            Engineer bob2 = new Engineer("Bob", "Roe", DateTime.Parse("6/30/1986"), 120000, "334-24-2422", DegreeName.MS);
            Engineer bob3 = new Engineer("Bob", "Roe", DateTime.Parse("6/30/1986"), 120000, "334-24-2422", DegreeName.MS);
            Engineer bob4 = new Engineer("Bob", "Roe", DateTime.Parse("6/30/1986"), 120000, "334-24-2422", DegreeName.MS);
            Engineer bob5 = new Engineer("Bob", "Roe", DateTime.Parse("6/30/1986"), 120000, "334-24-2422", DegreeName.MS);
            Engineer bob6 = new Engineer("Bob", "Roe", DateTime.Parse("6/30/1986"), 120000, "334-24-2422", DegreeName.MS);
            Engineer bob7 = new Engineer("Bob", "Roe", DateTime.Parse("6/30/1986"), 120000, "334-24-2422", DegreeName.MS);
            Engineer bob8 = new Engineer("Bob", "Roe", DateTime.Parse("6/30/1986"), 120000, "334-24-2422", DegreeName.MS);
            Engineer bob9 = new Engineer("Bob", "Roe", DateTime.Parse("6/30/1986"), 120000, "334-24-2422", DegreeName.MS);
            Engineer bob10 = new Engineer("Bob", "Roe", DateTime.Parse("6/30/1986"), 120000, "334-24-2422", DegreeName.MS);

            // Add Expenses
            Jane.Expenses.Add(new Expense(DateTime.Parse("1/25/2017"), ExpenseCategory.Travel, 300.55, "traveling"));
            Jane.Expenses.Add(new Expense(DateTime.Parse("1/27/2017"), ExpenseCategory.Meals, 27.61,"eating"));
            Jane.Expenses.Add(new Expense(DateTime.Parse("1/29/2017"), ExpenseCategory.Lodging, 423.15, "hotel"));
            dan.GiveBonus(1000);
            bob.GiveBonus(500);
            sally.GiveBonus(400);
            dan.GivePromotion();
            chucky.GivePromotion();
            fran.GivePromotion();

            
            // Add reports - just report error and continue on exception
            try
            {
                dan.AddReport(chucky);
                dan.AddReport(mary);
                connie.AddReport(fran);
                connie.AddReport(sally);
                mary.AddReport(sam);
                mary.AddReport(mike);
                chucky.AddReport(bob);
                chucky.AddReport(steve);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error when adding report: {e.Message}");
            }
            return new EmployeeList { dan, connie, chucky, mary, bob, fran, sam, sally, mike, steve, Jane, bob1, bob2, bob3, bob4, bob5, bob6, bob7, bob8, bob9, bob10};
        }
        // save list of employee 
        public static void SaveEmployeesAsBinary(string fileName, List<Employee> emps)
        {
            if( emps == null)
            {
                return;
            }
            // Use both try/catch and using 
            try
            {
                using (Stream fStream = new FileStream(fileName,
                  FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, emps);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving employoees: {0}", ex.Message);
            }
        }

        // Load list of Employees
        public static EmployeeList LoadEmployeesFromBinaryFile(string fileName)
        {
            // Use both try/catch and using
            try
            {
                using (Stream fStream = File.OpenRead(fileName))
                {
                    
                    EmployeeList holder = (EmployeeList) binFormat.Deserialize(fStream);
                    return holder;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading employees: {0}", ex.Message);
            }
            return null;
        }
    }
}

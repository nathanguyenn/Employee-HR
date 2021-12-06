using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Employees
{
    /// <summary>
    /// Interaction logic for PiePage.xaml
    /// </summary>
    public partial class PiePage : Page
    {
        private EmployeeList empList;
        public PiePage(EmployeeList employee)
        {
            InitializeComponent();
            empList = employee;     // data source 
            showChart();
        }

        // method draws the pie chart
        private void showChart()
        {
            SortedDictionary<string, int> resultDict = new SortedDictionary<string, int>();
            for (int i = 0; i < empList.Count; i++)
            {
                List<Expense> Expenses = empList[i].Expenses;

                // forms the dictionary of data
                for (int j = 0; j < Expenses.Count; j++)
                {
                    if (!resultDict.ContainsKey(Expenses[j].Category.ToString()))                                                    
                        resultDict.Add(Expenses[j].Category.ToString(), (int)Expenses[j].Amount);                                  
                    else
                        resultDict[Expenses[j].Category.ToString()] = resultDict[Expenses[j].Category.ToString()] + (int)Expenses[j].Amount;
                }
            }

            PieChart1.DataContext = resultDict; 
        }
    }
}

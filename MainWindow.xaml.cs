/*
 * GROUP 2
 * ASSIGNMENT 9
 * Csd 228 - c#
 */
using System;
using System.Collections.Generic;
using System.Windows.Navigation;

namespace Employees
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        #region Data members
        private EmployeeList empList = EmployeeList.GetEmployee();//new EmployeeList();
        #endregion

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();

            // Catch closing event to save changes
            this.Closing += MainWindow_Closing;

            // Create Details page and navigate to page
            this.NavigationService.Navigate(new CompHome(empList));
        }

        // save the employeelist on closing 
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EmployeeList.SaveEmployeesAsBinary("Employees.dat", CompHome.empList);
        }
        #endregion
    }
}

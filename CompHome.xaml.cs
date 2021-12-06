/*
 *  GROUP 2
 *  ASSIGNMENT 8
 *  CSD228 - C#
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Employees
{
    // The home page of the app
    public partial class CompHome : Page
    {
        #region Data members
        public static EmployeeList empList;
        //public static EmployeeList EmpList { get { return empList; } }
        #endregion

        #region Constructors
        public CompHome()
        {
            InitializeComponent();
        }

        public CompHome(EmployeeList emps) : this()
        {
            empList = emps;

            // Select the All radio button
            this.EmployeeTypeRadioButtons.SelectedIndex = 0;

            // Set event handler for radio button changes
            this.EmployeeTypeRadioButtons.SelectionChanged += new SelectionChangedEventHandler(EmployeeTypeRadioButtons_SelectionChanged);

            // Fill the Employees data grid
            dgEmps.ItemsSource = empList;
        }
        #endregion


        #region event handlers and method
        // Handle details button in XAML
        // Enable/disable buttons depending on selection state
        private void Button_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = dgEmps.SelectedIndex >= 0;
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            Details_DoubleClick(sender, e);
        }

        private void Details_DoubleClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CompDetails(this.dgEmps.SelectedItem));
        }

        private void Expenses_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CompExpenses(this.dgEmps.SelectedItem));
        }

        // Handle Add employee button click
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CompAddEmployee(this, empList));
        }

        // Handle Add employee button click
        private void Analytics_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Analytics (this, empList));            
        }

        // Handle changes to Employee type radio buttons
        void EmployeeTypeRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshEmployeeList();
        }
        public void RefreshEmployeeList()
        {
            List<Employee> empList1;

            // Apply the selection - All or Manager
            switch (this.EmployeeTypeRadioButtons.SelectedIndex)
            {
                case 0:
                    empList1 = empList;
                    break;
                case 1:
                    empList1 = (List<Employee>)empList.FindAll(obj => obj is Manager);
                    break;
                case 2:
                    empList1 = (List<Employee>)empList.FindAll(obj => obj is SalesPerson);
                    break;
                case 3:
                    empList1 = (List<Employee>)empList.FindAll(delegate (Employee emp)
                    {
                        if (emp is Manager || emp is SalesPerson)
                            return false;
                        else
                            return true;
                    });
                    break;
                // All 
                default:
                    empList1 = empList;
                    break;
            }

            dgEmps.ItemsSource = new ObservableCollection<Employee>(empList1);

            dgEmps.Items.Refresh();
        }
        #endregion
    }


}

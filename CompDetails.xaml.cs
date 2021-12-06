/*
 * GROUP 2
 * ASSIGNMENT 8
 * Csd 228 - c#
 */

using System;
using System.Windows;
using System.Windows.Controls;

namespace Employees
{
    /// <summary>
    /// Interaction logic for CompDetails.xaml
    /// </summary>
    public partial class CompDetails : Page
    {
        // Default constructor
        public CompDetails()
        {
            InitializeComponent();
        }

        // Constructor with Employee parameter
        public CompDetails(Object data) : this()
        {
            this.DataContext = data;

            // Need Employee to proceed
            if (data is Employee emp)
            {
                // Variables to hold spare prop name and value
                string name = "";
                object value = "";

                
                emp.GetSpareProp1(ref name, ref value);

                // if name and value is null meaning 
                // this employee doesnt have any extra property 
                // then we dont display the spare property
                if (name != null || value != null)
                {
                    this.SpareProp1Name.Content = name;
                    this.SpareProp1Value.Content = value.ToString();
                    // Make visible after setting values
                    this.SpareProp1Name.Visibility = Visibility.Visible;
                    this.SpareProp1Value.Visibility = Visibility.Visible;
                }

                
                emp.GetSpareProp2(ref name, ref value);

                // if name and value is null meaning 
                // this employee doesnt have any extra property 
                // then we dont display the spare property
                if (name != null || value != null)
                {
                    this.SpareProp2Name.Content = name;
                    this.SpareProp2Value.Content = value.ToString();
                    // Make visible after setting values
                    this.SpareProp2Name.Visibility = Visibility.Visible;
                    this.SpareProp2Value.Visibility = Visibility.Visible;
                }
            }
        }
    }
}

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
    public partial class Analytics : Page
    {
        private int XX = 730; // chart height
        private int YY = 200; // chart width
        private int step;     // X step
        private int xPos;     // X position

        private EmployeeList emp;           // declares data source       
        SortedDictionary<string, double> empDict;      
        List<Rectangle> listRect;                  
        
        public Analytics()
        {
            InitializeComponent();
            BareGrid();                     // выводит сетку координат    
            //PieChart1.Visibility = Visibility.Hidden;
        }
        
        public Analytics(CompHome home, EmployeeList employees) : this()
        {
            emp = employees;                    // получает источник данных           

            // Select the first chart radio button
            this.ChartRadioButtons.SelectedIndex = 0;

            // Set event handler for radio button changes
            this.ChartRadioButtons.SelectionChanged += new SelectionChangedEventHandler(ChartRadioButtons_SelectionChanged);

            ChartBuilder(emp);                  // first call of the chart builder
        }

        // calls methods for data processing and drawing charts
        private void ChartBuilder(EmployeeList emp)
        {
            // changes headlines of charts
            label1.Content = ((ListBoxItem)ChartRadioButtons.SelectedItem).Content;

            switch (this.ChartRadioButtons.SelectedIndex)
            {
                case 4:
                    this.NavigationService.Navigate(new PiePage(emp));
                    return;
            }

            empDict = DictSelect(emp);          // calls the module forming the dictionary
            listRect = RectangleList(empDict);  // calls the module forming bars                
            BarChart(listRect);                 // calls the module drawing bars 
            OverText(empDict);                  // calls the module drawing signatures under bars 
        }

        // handle changes to chart type radio buttons
        private void ChartRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canvas3.Children.Clear();           
            ChartBuilder(emp);                  // calls graph builder when changing radio buttons
        }

        // method picks the other chart data
        private SortedDictionary<string, double> DictSelect(EmployeeList empList)
        {
            SortedDictionary<string, double> resultDict = new SortedDictionary<string, double>();

            // draws the Expenses chart
            if (this.ChartRadioButtons.SelectedIndex == 2)
            {
                for (int i = 0; i < empList.Count; i++)
                {
                    List<Expense> Expenses = empList[i].Expenses;
                    for (int j = 0; j < Expenses.Count; j++)
                    {
                        // if in the list of roles the next role is different from the previous one
                        // the first data value is added to the dictionary
                        if (!resultDict.ContainsKey(Expenses[j].Category.ToString()))                            
                            resultDict.Add(Expenses[j].Category.ToString(), Expenses[j].Amount);                 
                        else
                            resultDict[Expenses[j].Category.ToString()] = resultDict[Expenses[j].Category.ToString()] + Expenses[j].Amount;
                    }
                }
            }
            else
            {
                // picks the other chart data
                List<string> listRoles = new List<string>();
                for (int i = 0; i < empList.Count; i++)
                {
                    listRoles.Add(empList[i].Role);
                }
                
                List<double> ListData = new List<double>();
                switch (this.ChartRadioButtons.SelectedIndex)
                {
                    case 0:
                        for (int i = 0; i < empList.Count; i++)
                        {
                            ListData.Add(1);
                        }
                        break;
                    case 1:
                        for (int i = 0; i < empList.Count; i++)
                        {
                            ListData.Add(empList[i].Pay);
                        }
                        break;
                    case 3:
                        for (int i = 0; i < empList.Count; i++)
                        {
                            ListData.Add(empList[i].GetBenefitCost());
                        }
                        break;                    
                }

                for (int i = 0; i < listRoles.Count; i++)
                {
                    if (!resultDict.ContainsKey(listRoles[i].ToString()))
                        resultDict.Add(listRoles[i].ToString(), (double)ListData[i]);
                    else
                        resultDict[listRoles[i].ToString()] = resultDict[listRoles[i].ToString()] + (double)ListData[i];
                }
            }
            return resultDict;
        }

        // the method draws the coordinate axes
        private void BareGrid()
        {
            Line vertLeft = new Line();
            vertLeft.X1 = 60;
            vertLeft.Y1 = YY;
            vertLeft.X2 = 60;
            vertLeft.Y2 = 0;
            vertLeft.Stroke = Brushes.Black;
            canvas2.Children.Add(vertLeft);
            Line vertRight = new Line();
            vertRight.X1 = XX;
            vertRight.Y1 = YY;
            vertRight.X2 = XX;
            vertRight.Y2 = 0;
            vertRight.Stroke = Brushes.Black;
            canvas2.Children.Add(vertRight);
            Line horL = new Line();
            horL.X1 = 60;
            horL.X2 = XX;
            horL.Y1 = YY;
            horL.Y2 = YY;
            horL.Stroke = Brushes.Black;
            canvas2.Children.Add(horL);

            // displays horizontal grid lines
            for (byte i = 0; i <= 160; i += 40)
            {
                Line a = new Line();
                a.X1 = 60;
                a.X2 = XX;
                a.Y1 = i;
                a.Y2 = i;
                a.Stroke = Brushes.Black;
                a.StrokeThickness = 1;
                canvas2.Children.Add(a);
            }
        }

        // the method forms a collection of bars based on the dictionary
        private List<Rectangle> RectangleList(SortedDictionary<string, double> dict)
        {
            List<Rectangle> empChart = new List<Rectangle>();
            foreach (KeyValuePair<string, double> KeyValue in dict)
            {
                empChart.Add(new Rectangle { Width = 10, Height = KeyValue.Value, Fill = Brushes.Blue });
            }
            return empChart;                                                        
        }

        // the method displays a vertical scale and draws bars
        private void BarChart(List<Rectangle> rect)
        {
            int N = rect.Count;  
            step = StepCalc(N);
            double k = 20;          // conversion factor to pixeles
            List<Rectangle> empChart = rect;            

            // finds the maximum and minimum values ​​of the height of the bars in the data array
            double maxHeight = empChart[0].Height;
            double minHeight = empChart[0].Height;
            for (var i = 0; i < N; i++)
            {
                if (rect[i].Height > maxHeight)
                {
                    maxHeight = rect[i].Height;
                }

                if (rect[i].Height < minHeight)
                {
                    minHeight = rect[i].Height;
                }
            }

            int[] scale = new int[6];                   // declares an array of Y scale values
            int min;                                    
            int hop;                                    // scale step

            if (maxHeight <= 10)                         // если высота столбиков меньше 10
            {
                min = 0;                                // set the minimum lower scale value
                hop = 2;                                // and minimum scale step
            }
            else                                        // otherwise, their nearest values ​​are multiples of 10
            {
                minHeight = (int)minHeight;             
                maxHeight = (int)maxHeight;
                min = (int)Math.Ceiling(minHeight / 10) * 10 - 10;
                hop = (int)(Math.Ceiling((maxHeight - minHeight + 1) / 10.0) * 10 / 5);
                if (maxHeight >= 0.01 && (maxHeight - minHeight) / maxHeight < 0.1)   // if the data is almost the same
                {
                    hop = (int)Math.Ceiling(minHeight * 0.2 / 10.0) * 10;
                    min = (int)Math.Ceiling((maxHeight - 3 * hop) / 10.0) * 10;
                }
            }

            // calculates scale divisions by Y
            for (int i = 0; i < 6; i++)
            {
                scale[i] = min + hop * i;
            }

            // outputs scale numbers
            Y5.Text = scale[5].ToString();
            Y4.Text = scale[4].ToString();
            Y3.Text = scale[3].ToString();
            Y2.Text = scale[2].ToString();
            Y1.Text = scale[1].ToString();
            //Y0.Text = scale[0].ToString();

            // finds the ratio of the value of data to the column pixels
            k = k / (scale[5] / 10);

            int x;
            for (var i = 0; i < N; i++)
            {
                empChart[i].Width = 2 * step;                           // bar width
                x = (3 * i + 1) * step + 60;                            // calculates the X position of the bar
                empChart[i].Height = (int)(empChart[i].Height * k);     // bar height
                Canvas.SetLeft(empChart[i], x);                         // X position of the bar
                Canvas.SetTop(empChart[i], YY - empChart[i].Height);    // Y position of the bar
                canvas3.Children.Add(empChart[i]);                      // the output of the bar
            }
        }

        // method creates signatures under bars
        List<string> empText = new List<string>();
        private List<string> Text(SortedDictionary<string, double> dict)
        {
            empText.Clear();
            foreach (KeyValuePair<string, double> KeyValue in dict)
            {
                empText.Add(KeyValue.Key);
            }
            return empText;                                             
        }

        // method prepares signatures under bars
        private void OverText(SortedDictionary<string, double> dict)
        {
            List<object> empKey = new List<object>();
            List<object> empValue = new List<object>();
            foreach (KeyValuePair<string, double> KeyValue in dict)
            {
                empKey.Add(KeyValue.Key);
                empValue.Add(KeyValue.Value);
            }
            UnderText(empKey, 203);
            UnderText(empValue, 215);
        }

        // method puts signatures under bars
        private void UnderText(List<object> list, int y)
        {
            int N = list.Count;                                     
            step = StepCalc(N);                                     // the X step            
            for (var i = 0; i < N; i++)
            {
                xPos = (3 * i + 1) * step + 55;
                TextBlock textBlock = new TextBlock();
                textBlock.Width = 70;
                textBlock.Text = list[i].ToString();
                textBlock.FontSize = 10;
                textBlock.TextAlignment = TextAlignment.Center;
                Canvas.SetLeft(textBlock, xPos);
                Canvas.SetTop(textBlock, y);
                canvas3.Children.Add(textBlock);
            }
        }

        // the method calculates the X step
        private int StepCalc(int n)
        {
            int step = XX / (4 * (n - 1));
            if (step > 35) step = 35;
            return step;
        }
    }
}

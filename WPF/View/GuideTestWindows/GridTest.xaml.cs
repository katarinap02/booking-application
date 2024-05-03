using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.View.GuideTestWindows
{
    public partial class GridTest : Window
    {
        public GridTest()
        {
            InitializeComponent();

            // Create some test data
            var testData = new List<Person>
            {
                new Person { ID = 1, FirstName = "John", LastName = "Doe", Phone = "123-456-7890", Number = 10, Address = "123 Main St" },
                new Person { ID = 2, FirstName = "Jane", LastName = "Smith", Phone = "987-654-3210", Number = 20, Address = "456 Elm St" },
                new Person { ID = 3, FirstName = "Alice", LastName = "Johnson", Phone = "555-555-5555", Number = 30, Address = "789 Oak St" }
            };

            // Bind the test data to the DataGrid
            testic.ItemsSource = testData;
        }
    }

    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }
    }
}

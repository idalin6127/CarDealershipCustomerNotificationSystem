using Delegate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace Delegate
{

    public partial class MainForm : Form
    {
        public List<Customer> customers = new List<Customer>();

        private string filePath = "customerData.txt";
        private List<string> customersNotified = new List<string>();
        private CustomerForm customerForm;
        public event Action<Customer, bool> CustomerSubscriptionChanged;

        public MainForm()
        {
            InitializeComponent();

            // Load existing customer data from file
            if (File.Exists(filePath))
            {
                LoadCustomerData();
            }
        }

        // Load existing customer data from file
        private void LoadCustomerData()
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] customerData = line.Split(',');
                if (customerData.Length == 5)
                {
                    string firstName = customerData[0];
                    string lastName = customerData[1];
                    string phoneNumber = customerData[2];
                    string address = customerData[3];
                    string email = customerData[4];

                    // Create Customer object using constructor and pass parameter values
                    Customer customer = new Customer(firstName, lastName, phoneNumber, address, email);

                    customers.Add(customer);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Open the new form
            customerForm = new CustomerForm(this, customers, filePath);

            if (customerForm.ShowDialog() == DialogResult.OK)
            {
                // User clicked "Subscribe" button, get customer data and store
                customers.Add(customerForm.NewCustomer);
                SaveCustomerData();
            }

        }

        // Write customer data to file
        private void SaveCustomerData()
        {
            using (StreamWriter file = new StreamWriter(filePath))
            {
                foreach (var customer in customers)
                {
                    if (customer != null)
                    {
                        string customerData = $"{customer.FirstName},{customer.LastName},{customer.PhoneNumber},{customer.Address},{customer.Email}";
                        file.WriteLine(customerData);
                    }

                }
            }
        }

        private void OnNewCustomerAdded(Customer newCustomer)
        {
            customers.Add(newCustomer);
            SaveCustomerData();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // Read vehicle information
            List<Car> cars = LoadCarsData();

            // Create notification message
            string notificationMessage = "Car Information:\n\n";
            foreach (var car in cars)
            {
                notificationMessage += $"Car Model: {car.Model}\nColor: {car.Color}\nYear: {car.Year}\nMileage: {car.Mileage} miles\nPrice: ${car.Price}\n\n";
            }

            // Send notification to all customers
            foreach (var customer in customers)
            {
                // Here you can use your preferred notification method, like displaying in a textbox or popping up a dialog
                string customerNotification = $"Dear {customer.FirstName},\n\n{notificationMessage},\n\n{customer.FirstName},\n\n{customer.LastName} ,\n\n{customer.PhoneNumber},\n\n{customer.Address},\n\n{customer.Email}";
                MessageBox.Show(customerNotification, "Notification for Customer");

            }

            // Update notification list
            string notifiedCustomers = string.Join(", ", customersNotified);

            // Trigger event to notify CustomerForm to update textBoxNotificationList
            if (customerForm != null)
            {
                customerForm.UpdateNotificationList(notifiedCustomers);
            }


        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            // Code to execute when the form loads
        }



        private List<Car> LoadCarsData()
        {
            List<Car> cars = new List<Car>();
            string carsFilePath = "Dealership_Cars_Info.txt";

            if (File.Exists(carsFilePath))
            {
                string[] lines = File.ReadAllLines(carsFilePath);

                foreach (string line in lines)
                {
                    string[] carData = line.Split(',');
                    if (carData.Length == 5)
                    {
                        string model = carData[0];
                        string color = carData[1];
                        int year = int.Parse(carData[2]);
                        int mileage = int.Parse(carData[3]);
                        decimal price = decimal.Parse(carData[4]);

                        Car car = new Car(model, color, year, mileage, price);
                        cars.Add(car);
                    }
                }
            }

            return cars;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

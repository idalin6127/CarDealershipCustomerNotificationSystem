using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static Delegate.MainForm;

namespace Delegate
{


    public partial class CustomerForm : Form
    {
        private MainForm form1;

        private List<Customer> customers;
        private string filePath = "customerData.txt";
        public Customer NewCustomer { get; private set; }
        public event Action<string> UpdateNotificationListEvent;

        public void UpdateNotificationList(string notifiedCustomers)
        {
            // 更新 textBoxNotificationList
            textBoxNotificationList.Text = notifiedCustomers;
        }
        public CustomerForm(MainForm form1, List<Customer> customers, string filePath)
        {
            InitializeComponent();
            this.form1 = form1;
            this.customers = customers;
            this.filePath = filePath;
        }

        //public CustomerForm(MainForm mainForm, List<Customer> customers, Action<Customer> onNewCustomerAdded)
        //{
        //    this.customers = customers;
        //}


        //////////////////////

        ////private void buttonSubscribe_Click(object sender, EventArgs e)
        ////{
        ////    // 获取用户输入的数据并创建一个新客户
        ////    NewCustomer = new Customer
        ////    {
        ////        FirstName = textBoxFirstName.Text,
        ////        LastName = textBoxLastName.Text,
        ////        PhoneNumber = textBoxPhoneNumber.Text,
        ////        Address = textBoxAddress.Text,
        ////        Email = textBoxEmail.Text
        ////    };

        ////    // 关闭窗体并返回 DialogResult.OK
        ////    DialogResult = DialogResult.OK;
        ////    Close();
        ////}

        // 点击Subscribe按钮时的事件处理程序
        private void buttonSubscribe_Click(object sender, EventArgs e)
        {
     
            string firstName = textBoxFirstName.Text;
            string lastName = textBoxLastName.Text;
            string phoneNumber = textBoxPhoneNumber.Text;
            string address = textBoxAddress.Text;
            string email = textBoxEmail.Text;
      

            // 检查邮箱是否已经存在
            if (customers.Any(customer => customer.Email == email))
            {
                MessageBox.Show("Email has aleady exit!");
                //return;
            }
            else
            { 

            Customer newCustomer = new Customer(firstName, lastName, phoneNumber, address, email);
                customers.Add(newCustomer);

                // 更新客户数据文件
                SaveCustomerData();


                //// 清空文本框
                //textBoxFirstName.Clear();
                //textBoxLastName.Clear();
                //textBoxPhoneNumber.Clear();
                //textBoxAddress.Clear();
                //textBoxEmail.Clear();

                // 设置 DialogResult 为 OK
                DialogResult = DialogResult.OK;

                Close();

                // 触发事件通知 MainForm
                //CustomerSubscriptionChanged?.Invoke(NewCustomer, true);
            }
        }

        //private void buttonUnsubscribe_Click(object sender, EventArgs e)
        //{
        //    DialogResult = DialogResult.Cancel;
        //    Close();
        //}

        // 点击Unsubscribe按钮时的事件处理程序
        private void buttonUnsubscribe_Click(object sender, EventArgs e)
        {
            string emailToUnsubscribe = textBoxEmail2.Text;

            // 检查邮箱是否存在
            Customer customerToUnsubscribe = customers.FirstOrDefault(customer => customer.Email == emailToUnsubscribe);

            if (customerToUnsubscribe != null)
            {
                // 执行取消订阅操作
                customers.Remove(customerToUnsubscribe);
                SaveCustomerData();

                MessageBox.Show("Unsubscribe successful!");

                // 设置 DialogResult 为 OK
                DialogResult = DialogResult.OK;
                Close();

                // 触发事件通知 MainForm
                //CustomerSubscriptionChanged?.Invoke(customerToUnsubscribe, false);
            

        }
            else
            {
                MessageBox.Show("Email not found in subscriptions.");
            }

            // 清空文本框
            textBoxEmail2.Clear();
        }

        // 将客户数据写入文件
        private void SaveCustomerData()
        {
            using (StreamWriter file = new StreamWriter(filePath))
            {
                foreach (var customer in customers)
                {
                    if(customer != null)
                    {
                        string customerData = $"{customer.FirstName},{customer.LastName},{customer.PhoneNumber},{customer.Address},{customer.Email}";
                        file.WriteLine(customerData);
                    }

                }
            }
        }


        //private void buttonCancel_Click(object sender, EventArgs e)
        //{
        //    textBoxFirstName.Clear();
        //    textBoxLastName.Clear();
        //    textBoxPhoneNumber.Clear();
        //    textBoxAddress.Clear();
        //    textBoxEmail.Clear();
        //}


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public Customer(string firstName, string lastName, string phoneNumber, string address, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Address = address;
            Email = email;
        }
    }
}




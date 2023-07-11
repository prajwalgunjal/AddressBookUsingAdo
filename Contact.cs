using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookUsingAdo
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Sstate { get; set; }
        public string PinCode { get; set; } 
        public string Country { get; set; }
        public string PhoneNumber2 { get; set; }

        public Contact (int id, string email) {
            Id = id;
            Email = email;
        }   
        public Contact (int id, string firstName, string lastName, string phoneNumber,string phoneNumber2, string email, string city, string sstate, string pinCode, string country)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            PhoneNumber2 = phoneNumber2;
            Email = email;
            City = city;
            Sstate = sstate;
            PinCode = pinCode;
            Country = country;

        } 
        public Contact( string firstName, string lastName, string phoneNumber,string phoneNumber2, string email, string city, string sstate, string pinCode, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            PhoneNumber2 = phoneNumber2;
            Email = email;
            City = city;
            Sstate = sstate;
            PinCode = pinCode;
            Country = country;

        }
        public Contact()
        {

        }
       
    }
}

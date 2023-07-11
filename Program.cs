using System.Diagnostics.Metrics;

namespace AddressBookUsingAdo
{
    public class Program
    {
        static void Main(string[] args)
        {
            AddressBookDBOperations addressBookDBOperations = new AddressBookDBOperations();

            while (true)
            {
                Console.WriteLine("Which Operation you want to perfrom");
                Console.WriteLine("press 1: Add Contact ");
                Console.WriteLine("press 2: Display Contact");
                Console.WriteLine("press 3: Display Contact By Id");
                Console.WriteLine("press 4: Update Contact By First name");
                Console.WriteLine("press 5: Delete Contact By Id");
                
                Console.Write("Enter Your Choice ");
                int Choice = int.Parse(Console.ReadLine());
                Console.WriteLine();
                switch (Choice)
                {
                    case 1:
                        { 
                            Console.WriteLine("Enter ID:- ");
                            int id = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Firstname: - ");
                            string firstName = Console.ReadLine();
                            Console.WriteLine("Enter LastName");
                            string Lastname = Console.ReadLine();
                            Console.WriteLine("Enter PhoneNumber1");
                            string phno = Console.ReadLine();
                            Console.WriteLine("Enter Alternate Phone Number:- ");
                            string phno2 = Console.ReadLine();
                            Console.WriteLine("Enter Email:- ");
                            string email = Console.ReadLine();
                            Console.WriteLine("Enter City:- ");
                            string city = Console.ReadLine();
                            Console.WriteLine("Enter Pincode:- ");
                            string pin = Console.ReadLine();
                            Console.WriteLine("Enter State:- ");
                            string state = Console.ReadLine();
                            Console.WriteLine("Enter Country:- ");
                            string country = Console.ReadLine();

                            Contact contact = new(id,firstName, Lastname, phno, phno2, email, city, state, pin, country)
                            {
                                Id = id,
                                FirstName = firstName,
                                LastName = Lastname,
                                PhoneNumber = phno,
                                PhoneNumber2 = phno2,
                                Email = email,
                                City = city,
                                Sstate = state,
                                PinCode = pin,
                                Country = country
                            };
                            addressBookDBOperations.AddContact(contact);
                            break;
                        }
                    case 2:
                        {
                            addressBookDBOperations.GetAllContact();
                            Console.WriteLine();
                            break;
                        }

                    case 3:
                        {
                            Console.Write("Enter Id:- ");
                            int id = int.Parse(Console.ReadLine()); 
                            addressBookDBOperations.GetContactByID(id);
                            Console.WriteLine();
                            break;
                        }
                        case 4:
                        {
                            Console.WriteLine("Enter ID:- ");
                            int id = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Updated Email id:- ");
                            string Email = Console.ReadLine();
                            Contact contact = new Contact(id, Email);
                            addressBookDBOperations.UpdateContact(contact);
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("Enter the Id which You want to delete");
                            int id = int.Parse(Console.ReadLine()); 
                            addressBookDBOperations.Delete(id);
                            break;
                        }
                }
            }
        }
    }
}
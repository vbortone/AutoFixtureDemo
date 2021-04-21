using System.Collections.Generic;

namespace AutoFixtureDemo.Core
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public IList<Address> Addresses = new List<Address>();

        public void Register()
        {
            // TODO: Implement PlaceOrder
        }

        public void UpdateProfile(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
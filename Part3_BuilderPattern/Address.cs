using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    public class Address
    {

        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string PostalCode { get; }
        public Address(string street, string city, string state, string country, string postalCode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
        }
    }
}

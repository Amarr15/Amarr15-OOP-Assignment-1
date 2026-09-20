using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Assignment
{
    public class Customer
    {

        public int Id { get; }
        public string Name { get; } = null!;
        public string Email { get; } = null!;
        public string City { get; } = null!;
        public bool IsVip { get; }
        public Customer(int id, string name, string email, string city, bool isVip)
        {
            if(id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            if(string.IsNullOrWhiteSpace(name)) throw new ArgumentException(nameof(name));
            if(string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));
            if(string.IsNullOrWhiteSpace(city)) throw new ArgumentNullException(nameof(city));
            Id = id;
            Name = name;
            Email = email;
            City = city;
            IsVip = isVip;
        }

    }
}

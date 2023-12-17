using AttachmentTask.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttachmentTask.Domain.Entites
{
    public class Address : ValueObject
    {
        public string? Street { get; private set; }
        public string? City { get; private set; }
        public string? Country { get; private set; }

        public Address() { }

        public Address(string street, string city,string country)
        {
            Street = street;
            City = city;
            Country = country;

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return Country;
        }
    }
}

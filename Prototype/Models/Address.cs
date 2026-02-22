namespace Prototype.Models;

/// <summary>
/// Address class - As composite property of Employee
/// </summary>
public class Address : IPrototype<Address>
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public Address()
    {
    }

    public Address(string street, string city, string country)
    {
        Street = street;
        City = city;
        Country = country;
    }

    public Address Clone()
    {
        return (Address)this.MemberwiseClone();
    }

    public Address DeepClone()
    {
        return new Address(Street, City, Country);
    }

    public override string ToString()
    {
        return $"{Street}, {City}, {Country}";
    }
}
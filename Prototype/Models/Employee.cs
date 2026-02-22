namespace Prototype.Models
{
    /// <summary>
    /// Employee class - Implements Prototype Pattern
    /// </summary>
    public class Employee : IPrototype<Employee>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Department { get; init; } = string.Empty;
        public List<string> Skills { get; init; }
        public Address Address { get; init; } = new Address();

        public Employee()
        {
            Skills = [];
        }

        public Employee(int id, string name, string department)
        {
            Id = id;
            Name = name;
            Department = department;
            Skills = [];
            Address = new Address();
        }

        /// <summary>
        /// Shallow clone - Using MemberwiseClone
        /// </summary>
        public Employee Clone()
        {
            return (Employee)this.MemberwiseClone();
        }

        /// <summary>
        /// Deep clone - Manually create new object and copy all properties
        /// </summary>
        public Employee DeepClone()
        {
            var clone = new Employee
            {
                Id = this.Id,
                Name = this.Name,
                Department = this.Department,
                Skills = [..this.Skills],
                Address = Address.DeepClone()
            };

            return clone;
        }

        public override string ToString()
        {
            return $"Employee[ID:{Id}, Name:{Name}, Dept:{Department}, " +
                   $"Skills:{string.Join(", ", Skills)}, Address:{Address}]";
        }
    }
}
namespace Iterator.Pattern
{
    /// <summary>
    /// Employee aggregate implementation
    /// Represents a collection of employees that can be iterated
    /// </summary>
    public class EmployeeCollection : IAggregate<Employee>
    {
        private readonly List<Employee> _employees;

        public EmployeeCollection()
        {
            _employees = new List<Employee>();
        }

        public EmployeeCollection(IEnumerable<Employee> employees)
        {
            _employees = new List<Employee>(employees);
        }

        public IIterator<Employee> CreateIterator()
        {
            return new ForwardIterator<Employee>(this);
        }

        public IIterator<Employee> CreateReverseIterator()
        {
            return new ReverseIterator<Employee>(this);
        }

        public IIterator<Employee> CreateSalaryIterator()
        {
            // Sort by salary and return forward iterator
            var sortedEmployees = _employees.OrderBy(e => e.Salary).ToList();
            return new ForwardIterator<Employee>(new EmployeeCollection(sortedEmployees));
        }

        public int Count => _employees.Count;

        public bool IsEmpty() => _employees.Count == 0;

        public Employee GetElement(int index)
        {
            if (index < 0 || index >= _employees.Count)
            {
                throw new IndexOutOfRangeException($"Index {index} is out of range [0, {_employees.Count - 1}]");
            }
            return _employees[index];
        }

        public void Add(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));
                
            _employees.Add(employee);
            Console.WriteLine($"[EmployeeCollection] Added employee: {employee.Name} ({employee.Department})");
        }

        public bool Remove(Employee employee)
        {
            if (employee == null)
                return false;
                
            var removed = _employees.Remove(employee);
            if (removed)
            {
                Console.WriteLine($"[EmployeeCollection] Removed employee: {employee.Name}");
            }
            return removed;
        }

        public void Clear()
        {
            var count = _employees.Count;
            _employees.Clear();
            Console.WriteLine($"[EmployeeCollection] Cleared {count} employees");
        }

        public List<Employee> GetAllEmployees()
        {
            return new List<Employee>(_employees);
        }

        public void SortByName()
        {
            _employees.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine("[EmployeeCollection] Sorted employees by name");
        }

        public void SortByDepartment()
        {
            _employees.Sort((a, b) => string.Compare(a.Department, b.Department, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine("[EmployeeCollection] Sorted employees by department");
        }

        public void SortBySalary()
        {
            _employees.Sort((a, b) => a.Salary.CompareTo(b.Salary));
            Console.WriteLine("[EmployeeCollection] Sorted employees by salary");
        }

        public List<Employee> FindEmployeesByDepartment(string department)
        {
            return _employees.Where(e => e.Department.Equals(department, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Employee> FindEmployeesBySalaryRange(decimal minSalary, decimal maxSalary)
        {
            return _employees.Where(e => e.Salary >= minSalary && e.Salary <= maxSalary).ToList();
        }

        public decimal GetAverageSalary()
        {
            return _employees.Any() ? _employees.Average(e => e.Salary) : 0;
        }

        public decimal GetTotalSalary()
        {
            return _employees.Sum(e => e.Salary);
        }

        public override string ToString()
        {
            return $"EmployeeCollection[Count: {Count}]";
        }

        public void DisplayCollection()
        {
            Console.WriteLine($"\n=== Employee Collection ({Count} employees) ===");
            if (IsEmpty())
            {
                Console.WriteLine("  No employees in collection");
            }
            else
            {
                var iterator = CreateIterator();
                while (iterator.MoveNext())
                {
                    var employee = iterator.Current;
                    Console.WriteLine($"  [{iterator.Position + 1}] {employee.Name} - {employee.Department} (${employee.Salary:F2})");
                }
            }
            Console.WriteLine(new string('=', 45));
        }
    }

    /// <summary>
    /// Employee entity
    /// </summary>
    public class Employee
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public int EmployeeId { get; set; }

        public Employee(int employeeId, string name, string department, string position, decimal salary, DateTime hireDate)
        {
            EmployeeId = employeeId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Department = department ?? throw new ArgumentNullException(nameof(department));
            Position = position ?? throw new ArgumentNullException(nameof(position));
            Salary = salary;
            HireDate = hireDate;
        }

        public int GetYearsOfService()
        {
            return DateTime.Now.Year - HireDate.Year;
        }

        public override string ToString()
        {
            return $"{Name} ({Position}) - {Department}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Employee other)
            {
                return EmployeeId == other.EmployeeId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return EmployeeId.GetHashCode();
        }
    }
}
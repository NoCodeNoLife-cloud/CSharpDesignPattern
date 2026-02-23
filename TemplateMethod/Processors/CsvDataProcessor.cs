namespace TemplateMethod.Processors
{
    /// <summary>
    /// CSV data processor implementation
    /// Processes CSV file data using template method pattern
    /// </summary>
    public class CsvDataProcessor : Framework.DataProcessor
    {
        private readonly string _filePath;
        private readonly List<CsvRecord> _records = new List<CsvRecord>();

        public CsvDataProcessor(string filePath) : base("CSV Processor")
        {
            _filePath = filePath;
        }

        protected override object LoadData()
        {
            Console.WriteLine($"[CSV Processor] Loading data from file: {_filePath}");
            
            // Simulate CSV file reading
            var csvData = new List<string[]>
            {
                new[] { "John", "Developer", "30", "50000" },
                new[] { "Jane", "Designer", "25", "45000" },
                new[] { "Bob", "Manager", "35", "60000" },
                new[] { "Alice", "Analyst", "28", "48000" }
            };
            
            Console.WriteLine($"[CSV Processor] Loaded {csvData.Count} records from CSV");
            return csvData;
        }

        protected override bool ValidateData(object data)
        {
            Console.WriteLine("[CSV Processor] Validating CSV data structure");
            
            if (data is not List<string[]> csvData)
                return false;
            
            // Validate each record has correct number of fields
            foreach (var record in csvData)
            {
                if (record.Length != 4)
                {
                    Console.WriteLine($"[CSV Processor] Invalid record format: {string.Join(",", record)}");
                    return false;
                }
            }
            
            Console.WriteLine($"[CSV Processor] CSV data validation passed ({csvData.Count} records)");
            return true;
        }

        protected override object TransformData(object rawData)
        {
            Console.WriteLine("[CSV Processor] Transforming CSV data to structured format");
            
            var csvData = (List<string[]>)rawData;
            _records.Clear();
            
            foreach (var row in csvData)
            {
                var record = new CsvRecord
                {
                    Name = row[0],
                    Position = row[1],
                    Age = int.Parse(row[2]),
                    Salary = decimal.Parse(row[3])
                };
                _records.Add(record);
            }
            
            Console.WriteLine($"[CSV Processor] Transformed {_records.Count} records");
            return _records;
        }

        protected override object ProcessTransformedData(object transformedData)
        {
            Console.WriteLine("[CSV Processor] Processing employee salary calculations");
            
            var records = (List<CsvRecord>)transformedData;
            
            // Calculate statistics
            var avgSalary = records.Average(r => r.Salary);
            var maxSalary = records.Max(r => r.Salary);
            var minSalary = records.Min(r => r.Salary);
            
            Console.WriteLine($"[CSV Processor] Salary Analysis:");
            Console.WriteLine($"  Average: ${avgSalary:F2}");
            Console.WriteLine($"  Maximum: ${maxSalary:F2}");
            Console.WriteLine($"  Minimum: ${minSalary:F2}");
            
            // Add bonus calculation
            foreach (var record in records)
            {
                record.Bonus = record.Salary * 0.1m; // 10% bonus
                record.TotalCompensation = record.Salary + record.Bonus;
            }
            
            return records;
        }

        protected override void SaveResults(object processedData)
        {
            Console.WriteLine("[CSV Processor] Saving processed results");
            
            var records = (List<CsvRecord>)processedData;
            
            // Simulate saving to database or file
            Console.WriteLine($"[CSV Processor] Results saved: {records.Count} employee records processed");
            foreach (var record in records)
            {
                Console.WriteLine($"  {record.Name}: ${record.TotalCompensation:F2} (Base: ${record.Salary:F2} + Bonus: ${record.Bonus:F2})");
            }
        }

        protected override void GenerateReport()
        {
            Console.WriteLine("[CSV Processor] Generating employee compensation report");
            
            var avgAge = _records.Average(r => r.Age);
            var positions = _records.GroupBy(r => r.Position).Select(g => g.Key).ToList();
            
            Console.WriteLine($"[CSV Processor] Report Summary:");
            Console.WriteLine($"  Total Employees: {_records.Count}");
            Console.WriteLine($"  Average Age: {avgAge:F1} years");
            Console.WriteLine($"  Positions: {string.Join(", ", positions)}");
            Console.WriteLine($"  Total Compensation: ${_records.Sum(r => r.TotalCompensation):F2}");
        }

        protected override void HandleError(Exception ex)
        {
            Console.WriteLine($"[CSV Processor] CSV processing error: {ex.Message}");
            // Log error or send notification
        }

        protected override void HandleValidationError()
        {
            Console.WriteLine("[CSV Processor] CSV data validation failed - check file format");
        }

        // CSV Record data structure
        private class CsvRecord
        {
            public string Name { get; set; } = string.Empty;
            public string Position { get; set; } = string.Empty;
            public int Age { get; set; }
            public decimal Salary { get; set; }
            public decimal Bonus { get; set; }
            public decimal TotalCompensation { get; set; }
        }
    }
}
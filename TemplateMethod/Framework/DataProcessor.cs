namespace TemplateMethod.Framework
{
    /// <summary>
    /// Data processing template abstract class
    /// Defines the skeleton of data processing algorithm
    /// </summary>
    public abstract class DataProcessor
    {
        protected readonly string _processorName;
        protected DateTime _startTime;
        protected DateTime _endTime;

        protected DataProcessor(string processorName)
        {
            _processorName = processorName;
        }

        /// <summary>
        /// Template method - defines the algorithm skeleton
        /// </summary>
        public void ProcessData()
        {
            Console.WriteLine($"\n=== Starting {_processorName} Data Processing ===");
            _startTime = DateTime.Now;

            try
            {
                // Step 1: Initialize processing
                Initialize();
                
                // Step 2: Load data
                var rawData = LoadData();
                
                // Step 3: Validate data
                if (!ValidateData(rawData))
                {
                    Console.WriteLine($"[{_processorName}] Data validation failed");
                    HandleValidationError();
                    return;
                }
                
                // Step 4: Transform data
                var transformedData = TransformData(rawData);
                
                // Step 5: Process data (hook method - can be overridden)
                var processedData = ProcessTransformedData(transformedData);
                
                // Step 6: Save results
                SaveResults(processedData);
                
                // Step 7: Generate report (hook method - can be overridden)
                GenerateReport();
                
                _endTime = DateTime.Now;
                Console.WriteLine($"[{_processorName}] Processing completed successfully in {(_endTime - _startTime).TotalMilliseconds:F0} ms");
            }
            catch (Exception ex)
            {
                _endTime = DateTime.Now;
                Console.WriteLine($"[{_processorName}] Processing failed: {ex.Message}");
                HandleError(ex);
            }
            finally
            {
                Cleanup();
                Console.WriteLine($"=== {_processorName} Data Processing Finished ===\n");
            }
        }

        /// <summary>
        /// Step 1: Initialize processing environment
        /// </summary>
        protected virtual void Initialize()
        {
            Console.WriteLine($"[{_processorName}] Initializing processing environment");
        }

        /// <summary>
        /// Step 2: Load raw data (abstract method - must be implemented)
        /// </summary>
        protected abstract object LoadData();

        /// <summary>
        /// Step 3: Validate loaded data
        /// </summary>
        protected virtual bool ValidateData(object data)
        {
            Console.WriteLine($"[{_processorName}] Validating data");
            return data != null;
        }

        /// <summary>
        /// Step 4: Transform raw data to required format
        /// </summary>
        protected virtual object TransformData(object rawData)
        {
            Console.WriteLine($"[{_processorName}] Transforming data");
            return rawData;
        }

        /// <summary>
        /// Step 5: Process transformed data (hook method - can be overridden)
        /// </summary>
        protected virtual object ProcessTransformedData(object transformedData)
        {
            Console.WriteLine($"[{_processorName}] Processing transformed data");
            return transformedData;
        }

        /// <summary>
        /// Step 6: Save processing results (abstract method - must be implemented)
        /// </summary>
        protected abstract void SaveResults(object processedData);

        /// <summary>
        /// Step 7: Generate processing report (hook method - can be overridden)
        /// </summary>
        protected virtual void GenerateReport()
        {
            Console.WriteLine($"[{_processorName}] Generating processing report");
        }

        /// <summary>
        /// Error handling hook method
        /// </summary>
        protected virtual void HandleError(Exception ex)
        {
            Console.WriteLine($"[{_processorName}] Error handled: {ex.Message}");
        }

        /// <summary>
        /// Validation error handling hook method
        /// </summary>
        protected virtual void HandleValidationError()
        {
            Console.WriteLine($"[{_processorName}] Validation error handled");
        }

        /// <summary>
        /// Cleanup resources
        /// </summary>
        protected virtual void Cleanup()
        {
            Console.WriteLine($"[{_processorName}] Cleaning up resources");
        }

        /// <summary>
        /// Gets processing duration
        /// </summary>
        public TimeSpan GetProcessingDuration()
        {
            return _endTime - _startTime;
        }

        /// <summary>
        /// Gets processor name
        /// </summary>
        public string GetProcessorName()
        {
            return _processorName;
        }
    }
}
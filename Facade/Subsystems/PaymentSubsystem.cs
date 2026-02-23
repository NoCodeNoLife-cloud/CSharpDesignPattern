namespace Facade.Subsystems
{
    /// <summary>
    /// Payment processing subsystem
    /// Handles payment transactions, validation, and financial operations
    /// </summary>
    public class PaymentSubsystem
    {
        private readonly Dictionary<string, PaymentMethod> _paymentMethods = new Dictionary<string, PaymentMethod>();
        private readonly List<Transaction> _transactions = new List<Transaction>();
        private int _transactionCounter = 5000;

        public class PaymentMethod
        {
            public string MethodId { get; set; } = string.Empty;
            public string CustomerName { get; set; } = string.Empty;
            public PaymentType Type { get; set; }
            public string AccountInfo { get; set; } = string.Empty;
            public bool IsActive { get; set; }
            public DateTime CreatedDate { get; set; }
        }

        public class Transaction
        {
            public int TransactionId { get; set; }
            public string PaymentMethodId { get; set; } = string.Empty;
            public int OrderId { get; set; }
            public decimal Amount { get; set; }
            public TransactionStatus Status { get; set; }
            public DateTime Timestamp { get; set; }
            public string ReferenceNumber { get; set; } = string.Empty;
        }

        public enum PaymentType
        {
            CreditCard,
            DebitCard,
            PayPal,
            BankTransfer,
            DigitalWallet
        }

        public enum TransactionStatus
        {
            Pending,
            Processing,
            Completed,
            Failed,
            Refunded
        }

        public PaymentSubsystem()
        {
            // Initialize with sample payment methods
            InitializePaymentMethods();
        }

        private void InitializePaymentMethods()
        {
            RegisterPaymentMethod("PM001", "John Smith", PaymentType.CreditCard, "****-****-****-1234");
            RegisterPaymentMethod("PM002", "Jane Doe", PaymentType.PayPal, "jane.doe@email.com");
            RegisterPaymentMethod("PM003", "Bob Johnson", PaymentType.DebitCard, "****-****-****-5678");
        }

        /// <summary>
        /// Registers a new payment method
        /// </summary>
        public string RegisterPaymentMethod(string customerName, PaymentType type, string accountInfo)
        {
            var methodId = $"PM{new Random().Next(100, 999)}";
            return RegisterPaymentMethod(methodId, customerName, type, accountInfo);
        }

        public string RegisterPaymentMethod(string methodId, string customerName, PaymentType type, string accountInfo)
        {
            var paymentMethod = new PaymentMethod
            {
                MethodId = methodId,
                CustomerName = customerName,
                Type = type,
                AccountInfo = accountInfo,
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            _paymentMethods[methodId] = paymentMethod;
            Console.WriteLine($"[Payment] Registered {type} for {customerName} ({methodId})");
            return methodId;
        }

        /// <summary>
        /// Processes a payment transaction
        /// </summary>
        public int ProcessPayment(string paymentMethodId, int orderId, decimal amount)
        {
            if (!_paymentMethods.ContainsKey(paymentMethodId))
            {
                Console.WriteLine($"[Payment] Invalid payment method: {paymentMethodId}");
                return -1;
            }

            var transaction = new Transaction
            {
                TransactionId = _transactionCounter++,
                PaymentMethodId = paymentMethodId,
                OrderId = orderId,
                Amount = amount,
                Status = TransactionStatus.Processing,
                Timestamp = DateTime.Now,
                ReferenceNumber = GenerateReferenceNumber()
            };

            _transactions.Add(transaction);

            // Simulate payment processing
            var success = SimulatePaymentProcessing(amount);
            transaction.Status = success ? TransactionStatus.Completed : TransactionStatus.Failed;

            var statusText = success ? "completed" : "failed";
            Console.WriteLine($"[Payment] Transaction #{transaction.TransactionId} {statusText} - ${amount:F2} for order #{orderId}");

            return success ? transaction.TransactionId : -1;
        }

        /// <summary>
        /// Validates payment method
        /// </summary>
        public bool ValidatePaymentMethod(string paymentMethodId)
        {
            if (_paymentMethods.TryGetValue(paymentMethodId, out var method))
            {
                bool valid = method.IsActive;
                Console.WriteLine($"[Payment] Payment method {paymentMethodId} validation: {valid}");
                return valid;
            }
            
            Console.WriteLine($"[Payment] Payment method {paymentMethodId} not found");
            return false;
        }

        /// <summary>
        /// Gets transaction details
        /// </summary>
        public Transaction? GetTransactionDetails(int transactionId)
        {
            return _transactions.FirstOrDefault(t => t.TransactionId == transactionId);
        }

        /// <summary>
        /// Processes refund
        /// </summary>
        public bool ProcessRefund(int transactionId, decimal amount)
        {
            var transaction = _transactions.FirstOrDefault(t => t.TransactionId == transactionId);
            if (transaction == null || transaction.Status != TransactionStatus.Completed) return false;

            if (amount > transaction.Amount) return false;

            transaction.Status = TransactionStatus.Refunded;
            Console.WriteLine($"[Payment] Refund of ${amount:F2} processed for transaction #{transactionId}");
            return true;
        }

        /// <summary>
        /// Gets payment methods for customer
        /// </summary>
        public List<PaymentMethod> GetCustomerPaymentMethods(string customerName)
        {
            return _paymentMethods.Values
                .Where(pm => pm.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase) && pm.IsActive)
                .ToList();
        }

        /// <summary>
        /// Gets transaction history
        /// </summary>
        public List<Transaction> GetTransactionHistory(int orderId)
        {
            return _transactions
                .Where(t => t.OrderId == orderId)
                .OrderByDescending(t => t.Timestamp)
                .ToList();
        }

        /// <summary>
        /// Gets daily transaction summary
        /// </summary>
        public Dictionary<TransactionStatus, int> GetDailySummary()
        {
            var today = DateTime.Today;
            return _transactions
                .Where(t => t.Timestamp.Date == today)
                .GroupBy(t => t.Status)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        /// <summary>
        /// Gets total revenue for period
        /// </summary>
        public decimal GetRevenue(DateTime startDate, DateTime endDate)
        {
            return _transactions
                .Where(t => t.Timestamp >= startDate && t.Timestamp <= endDate && t.Status == TransactionStatus.Completed)
                .Sum(t => t.Amount);
        }

        private bool SimulatePaymentProcessing(decimal amount)
        {
            // Simulate 5% failure rate
            return new Random().NextDouble() > 0.05;
        }

        private static string GenerateReferenceNumber()
        {
            return $"REF{DateTime.Now:yyyyMMddHHmmss}{new Random().Next(1000, 9999)}";
        }
    }
}
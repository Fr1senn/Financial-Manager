namespace financial_manager.Models
{
    public class User
    {
        public int Id { get; init; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public int MonthlyBudget { get; set; }
        public string RawPassword { get; set; } = string.Empty;
        public byte[]? PasswordSalt { get; set; }

        public List<Category>? Categories { get; set; }
        public List<Transaction>? Transactions { get; set; }

        private short _budgetUpdateDay { get; set; }

        public short BudgetUpdateDay
        {
            get => this._budgetUpdateDay;
            set
            {
                if (value < 1 || value > 31)
                {
                    throw new ArgumentOutOfRangeException("Budget update day should be in range from 1 to 31");
                }
                _budgetUpdateDay = value;
            }
        }
    }
}

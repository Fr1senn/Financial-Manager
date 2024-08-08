namespace financial_manager.Models
{
    public class User
    {
        public int Id { get; private set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; private set; }
        public int MonthlyBudget { get; set; }

        public List<Category> Categories { get; set; }
        public List<Transaction> Transactions { get; set; }

        private short _budgetUpdateDay { get; set; }
        private string HashedPassword { get; set; } = string.Empty;

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

        public User(string fullName, string email, int monthlyBudget, short budgetUpdateDay, List<Category> categories, List<Transaction> transactions, string password)
        {
            FullName = fullName;
            Email = email;
            RegistrationDate = DateTime.Now;
            MonthlyBudget = monthlyBudget;
            BudgetUpdateDay = budgetUpdateDay;
            Categories = categories;
            Transactions = transactions;
            HashedPassword = password;
        }
    }
}

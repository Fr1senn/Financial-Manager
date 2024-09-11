namespace financial_manager.Entities.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime RegistrationDate {  get; set; }
        public int MonthlyBudget { get; set; }
        public short BudgetUpdateDay { get; set; }
        public IEnumerable<CategoryDTO>? Categories { get; set; }
        public IEnumerable<TransactionDTO>? Transactions {  get; set; }
        public IEnumerable<TokenDTO>? Tokens { get; set; }
    }
}

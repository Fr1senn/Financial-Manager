namespace financial_manager.Entities;

public class MonthlyTransactionData
{
    public int? Month { get; set; }
    public decimal Income { get; set; }
    public decimal Expense { get; set; }
}
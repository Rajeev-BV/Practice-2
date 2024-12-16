namespace Practice_2
{
    public interface IBankRepository
    {
        Task<int> GetBalanceAmount(string accountNumber);
        Task UpdateBalance(string accountNumber, int amount);
    }
}
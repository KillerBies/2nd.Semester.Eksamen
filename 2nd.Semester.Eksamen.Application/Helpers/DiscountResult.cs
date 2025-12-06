using _2nd.Semester.Eksamen.Domain.Entities.Discounts;

namespace _2nd.Semester.Eksamen.Application.Helpers
{
    public class DiscountResult
    {
        public decimal TotalSavings { get; private set; } = 0;
        public Discount? Discount { get; private set; }
        private readonly object _lock = new();

        public void TryUpdate(decimal savings, Discount discount)
        {
            lock (_lock)
            {
                if (savings > TotalSavings)
                {
                    TotalSavings = savings;
                    Discount = discount;
                }
            }
        }
    }
}

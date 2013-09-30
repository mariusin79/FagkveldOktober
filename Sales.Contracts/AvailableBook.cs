using SharedContracts;

namespace Sales.Contracts
{
    public class AvailableBook
    {
        public BookKey Id { get; set; }
        public Money Price { get; set; } 
    }
}
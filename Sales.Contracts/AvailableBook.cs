using SharedContracts;

namespace Sales.Contracts
{
    public class AvailableBook
    {
        public BookKey Book { get; set; }
        public Money Price { get; set; } 
    }
}
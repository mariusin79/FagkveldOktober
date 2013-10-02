using SharedContracts;

namespace Sales.PlaceAnOrder
{
    public class SoldBook
    {
        public virtual int Id { get; set; }

        public virtual BookKey SoldBookKey { get; set; }
    }
}
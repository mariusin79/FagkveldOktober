namespace Sales.Automapping
{
    public interface IMapper
    {
        T Map<T>(object src);
    }
}
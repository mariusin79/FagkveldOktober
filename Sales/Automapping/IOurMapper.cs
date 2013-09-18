namespace Sales.Automapping
{
    public interface IOurMapper
    {
        T Map<T>(object src);
    }
}
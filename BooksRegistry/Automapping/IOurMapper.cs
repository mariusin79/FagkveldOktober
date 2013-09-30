namespace BooksRegistry.Automapping
{
    public interface IOurMapper
    {
        T Map<T>(object src);
    }
}
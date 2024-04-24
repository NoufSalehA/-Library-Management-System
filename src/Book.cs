public class Book : LibraryItems//inheritance
{
    public Book(string title, DateTime? theDate = null) : base(title, theDate)
    {//inherit /access the parent constructor using ':base'
    }
}

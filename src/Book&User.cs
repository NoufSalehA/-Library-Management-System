public class Book : LibraryItems//inheritance
{
    public Book(string title, DateTime? theDate = null) : base(title, theDate)
    {//inherit /access the parent constructor using ':base'
    }
}
public class User : LibraryItems
{
    public User(string name, DateTime? theDate = null) : base(name, theDate)
    {

    }
}
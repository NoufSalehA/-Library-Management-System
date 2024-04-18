public class LibraryItems
{//only name is required
    public Guid Id
    {
        get;
        set;
    }
    public string Name
    {
        get;
        set;
    }
    public DateTime CreatedDate
    {
        get;
        set;
    }
    public LibraryItems(string names, DateTime? thecreatedate = null)//constructor//trying null instead of default
    {
        Id = Guid.NewGuid();
        this.Name = names;
        this.CreatedDate = thecreatedate ?? DateTime.Now;//if it is null then it will use the date of today- without this. is also correct
    }
}
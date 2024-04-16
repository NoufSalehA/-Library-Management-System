public class LibraryItems
{//only name is required
    public Guid Id
    {
        get;
        set;
    }
    public string Names
    {
        get;
        set;
    }
    public DateTime TheCreateDate
    {
        get;
        set;
    }
    public LibraryItems(string names, DateTime thecreatedate = default)//constructor
    {
        this.Names = names;
        this.TheCreateDate = thecreatedate = default ? DateTime.Now : thecreatedate;
    }

}
public class Book:LibraryItems//inheritance
{

public Book(string title,DateTime theDate):base(title,theDate){//inherit /access the parent constructor using ':base'

}



}
public class User:LibraryItems
{
    public User(string name,DateTime theDate):base(name,theDate){

    }

}
public class Library{//all the work




}


internal class Program
{
    private static void Main()
    {

    }
}

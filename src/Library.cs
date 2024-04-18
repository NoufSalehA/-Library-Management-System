public class Library
{//all the work
    public List<Book> _books;
    public List<User> _users;
    private INotificationService notificationService;//receive what inside constructor down
    public Library(INotificationService notificationService)//constructor -create objs for users,books //define notification inside constructor. dependency injection done

    {
        _books = new List<Book>();
        _users = new List<User>();
        this.notificationService = notificationService;
    }
    public void AddBook(Book book)//method for adding books
    {
        _books.Add(book);// the message here will added/pass to the original success message
        notificationService.SendNotificationOnSuccess($"*{book.Name}* Has been Added");
    }
    public void DeleteBook(Guid id)//method for deleting books by its id
    {
        Book? bookToDelete = _books.Find(book => book.Id == id);//find book by id
        if (bookToDelete != null)
        {
            _books.Remove(bookToDelete);
            notificationService.SendNotificationOnSuccess($"*{bookToDelete.Name} *--- Deleted ---");
        }
        else
        {
            notificationService.SendNotificationOnFailure($"--- Incorrect ID---");
        }
    }
    public List<Book> FindBookByItsTitle(string title)
    {
        return _books.FindAll(book => book.Name.Contains(title));
    }
    public List<Book> GetAllBooks(int page, int pageSize)
    {
        //pagination: number of page-limit(page size)
        return _books.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }
    public void AddUser(User user)//method to add user
    {
        _users.Add(user);
        notificationService.SendNotificationOnSuccess($"*{user.Name}* Has been Added");
    }
    public void DeleteUser(Guid id)
    {
        User? userToDelete = _users.Find(user => user.Id == id);//find user by id
        if (userToDelete != null)
        {
            _users.Remove(userToDelete);
            notificationService.SendNotificationOnFailure($"User{userToDelete.Name} is removed from list");
        }
        else
        {
            Console.WriteLine($"--!--Incorrect ID--!--");
        }
    }
    public List<User> FindUserByName(string name)
    {
        return _users.FindAll(user => user.Name.Contains(name));
    }
    public List<User> GetAllUsers(int page, int pageSize)
    {
        //pagination: number of page-limit(page size)
        return _users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }
}
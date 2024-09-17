public class Admin
{
    public Guid Id { get; set; }
    public string _username { get; private set; }
    public string _email { get; private set; }

    public Admin(string username, string email)
    {
        Id = Guid.NewGuid();
        _username = username;
        _email = email;
    }
}
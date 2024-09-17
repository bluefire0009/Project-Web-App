public class User
{
    public Guid Id { get; set; }
    public string _first_name { get; private set; }
    public string _last_name { get; private set; }
    public string _email { get; private set; }
    public string _password { get; private set; }
    public int _recurring_days { get; private set; }

    public User(string first_name, string last_name, string email, string password, int recurring_days)
    {
        Id = Guid.NewGuid();
        _first_name = first_name;
        _last_name = last_name;
        _email = email;
        _password = password;
        _recurring_days = recurring_days;
    }
}
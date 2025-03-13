using System;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public User(string name, string email, string passwordHash = "-1")
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
    }
}

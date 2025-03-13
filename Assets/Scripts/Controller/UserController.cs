using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

public class UserController
{
    private List<User> users = new List<User>();

    public User RegisterUser(string name, string email, string password)
    {
        if (!IsValidEmail(email))
            throw new ArgumentException("Invalid email format.");

        if (password.Length < 6)
            throw new ArgumentException("Password must be at least 6 characters long.");

        if (users.Exists(u => u.Email == email))
            throw new ArgumentException("Email is already in use.");

        string hashedPassword = HashPassword(password);
        User newUser = new User(name, email, hashedPassword);
        users.Add(newUser);
        return newUser;
    }

    public bool Login(string email, string password, out User loggedInUser)
    {
        loggedInUser = users.Find(u => u.Email == email);

        if (loggedInUser != null && loggedInUser.PasswordHash == HashPassword(password))
        {
            return true;
        }

        loggedInUser = null;
        return false;
    }


    private bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
    public bool EditProfile(Guid userId, string newName, string newEmail, string newPassword)
    {
        User user = users.Find(u => u.Id == userId);
        if (user == null)
            return false;

        if (!string.IsNullOrEmpty(newEmail) && !IsValidEmail(newEmail))
            throw new ArgumentException("Invalid email format.");

        if (!string.IsNullOrEmpty(newPassword) && newPassword.Length < 6)
            throw new ArgumentException("Password must be at least 6 characters long.");

        user.Name = string.IsNullOrEmpty(newName) ? user.Name : newName;
        user.Email = string.IsNullOrEmpty(newEmail) ? user.Email : newEmail;
        user.PasswordHash = string.IsNullOrEmpty(newPassword) ? user.PasswordHash : HashPassword(newPassword);

        return true;
    }


}

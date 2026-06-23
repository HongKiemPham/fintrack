using FinTrack.Domain.Common;

namespace FinTrack.Domain.Entities;

public class User : AuditableEntity
{
    private User() { }

    public User(string email, string passwordHash, string fullName, string role)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash is required.");

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required.");

        if (string.IsNullOrWhiteSpace(role))
            throw new ArgumentException("Role is required.");

        Email = email;
        PasswordHash = passwordHash;
        FullName = fullName;
        Role = role;
        IsLocked = false;
    }

    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public string FullName { get; private set; } = default!;
    public string Role { get; private set; } = default!;
    public bool IsLocked { get; private set; }

    public void Lock()
    {
        IsLocked = true;
        MarkAsUpdated();
    }

    public void Unlock()
    {
        IsLocked = false;
        MarkAsUpdated();
    }

    public void ChangeRole(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            throw new ArgumentException("Role is required.");

        Role = role;
        MarkAsUpdated();
    }
}
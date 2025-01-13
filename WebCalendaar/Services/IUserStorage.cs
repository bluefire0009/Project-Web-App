using WebCalendaar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public interface IUserStorage
{
    Task<bool> Create(User user);
    Task<User?> Read(int user_id);
    Task<bool> Update(int user_id, User user);
    Task<bool> Delete(int user_id);
    Task<User?> ReadByEmail(string email);
}

public class UserDBStorage : IUserStorage
{
    public DatabaseContext db;
    public UserDBStorage(DatabaseContext db)
    {
        this.db = db;
    }
    public async Task<bool> Create(User user)
    {
        if (await db.User.AnyAsync(x => x.UserId == user.UserId)) return false;
        await db.User.AddAsync(user);
        if (await db.SaveChangesAsync() > 0) return true;
        return false;
    }

    public async Task<bool> Delete(int user_id)
    {
        // returns true if user was deleted
        User? user = await db.User.FirstOrDefaultAsync(x => x.UserId == user_id);
        if (user == null) return false;
        db.User.Remove(user);
        if (await db.SaveChangesAsync() > 0) return true;
        return false;

    }

    public async Task<User?> Read(int user_id)
    {
        return await db.User.FirstOrDefaultAsync(x => x.UserId == user_id);
    }

    public async Task<User?> ReadByEmail(string email)
    {
        return await db.User.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<bool> Update(int user_id, User updatedUser)
    {
        // find user in db
        User? foundUser = await this.Read(user_id);
        // return flase if user doesnt exist
        if (foundUser == null) return false;

        // update all the fields of the user
        foundUser.FirstName = updatedUser.FirstName;
        foundUser.LastName = updatedUser.LastName;
        foundUser.Email = updatedUser.Email;
        foundUser.Password = updatedUser.Password;
        foundUser.RecuringDays = updatedUser.RecuringDays;

        if (await db.SaveChangesAsync() > 0) return true;
        return false;

    }
}
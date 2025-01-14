using Microsoft.EntityFrameworkCore;
using WebCalendaar.Models;
using WebCalendaar.Utils;

public class AdminDBStorage : IAdminStorage
{
    private DatabaseContext db;

    public AdminDBStorage(DatabaseContext db)
    {
        this.db = db;
    }

    public async Task<bool> Create(Admin admin)
    {
        Admin? adminInDatabase = await db.Admin.FirstOrDefaultAsync(a => a.AdminId == admin.AdminId);
        if (adminInDatabase != null)
            return false;

        string Password = EncryptionHelper.EncryptPassword(admin.Password);
        admin.Password = Password;
        await db.Admin.AddAsync(admin);

        int nrChanges = await db.SaveChangesAsync();
        if (nrChanges > 0)
            return true;
        return false;
    }

    public async Task<bool> Update(Admin admin)
    {
        Admin? adminInDatabase = await db.Admin.FirstOrDefaultAsync(a => a.AdminId == admin.AdminId);
        if (adminInDatabase == null)
            return false;

        adminInDatabase.AdminId = admin.AdminId;
        adminInDatabase.Email = admin.Email;
        adminInDatabase.UserName = admin.UserName;
        adminInDatabase.Password = admin.Password;

        int nrChanges = await db.SaveChangesAsync();
        if (nrChanges > 0)
            return true;
        return false;
    }

    public async Task<bool> Delete(int adminId)
    {
        Admin? adminInDatabase = await db.Admin.FirstOrDefaultAsync(a => a.AdminId == adminId);
        if (adminInDatabase == null)
            return false;

        db.Admin.Remove(adminInDatabase);

        int nrChanges = await db.SaveChangesAsync();
        if (nrChanges > 0)
            return true;
        return false;
    }

    public async Task<Admin?> Find(int adminId)
    {
        Admin? adminInDatabase = await db.Admin.FirstOrDefaultAsync(a => a.AdminId == adminId);
        if (adminInDatabase == null)
            return null;
        return adminInDatabase;
    }

    public async Task<List<Admin>> FindMany(int[] adminIds)
    {
        List<Admin> adminsInDatabase = await db.Admin.Where(A => adminIds.Contains(A.AdminId)).ToListAsync();
        return adminsInDatabase;
    }
}
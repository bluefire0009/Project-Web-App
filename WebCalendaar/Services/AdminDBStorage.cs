using Microsoft.EntityFrameworkCore;
using WebCalendaar.Models;

public class AdminDBStorage : IAdminStorage
{
    private DatabaseContext db;

    public AdminDBStorage(DatabaseContext db)
    {
        this.db = db;
    }

    public async Task<bool> Create(Admin admin)
    {
        Admin? adminInDatabase = db.Admin.FirstOrDefault(a => a.AdminId == admin.AdminId);
        if (adminInDatabase != null)
            return false;

        admin.AdminId = Guid.NewGuid();
        await db.Admin.AddAsync(admin);

        int nrChanges = await db.SaveChangesAsync();
        if (nrChanges > 0)
            return true;
        return false;
    }

    public async Task<bool> Delete(Guid adminId)
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

    public async Task<Admin?> Find(Guid adminId)
    {
        Admin? adminInDatabase = await db.Admin.FirstOrDefaultAsync(a => a.AdminId == adminId);
        if (adminInDatabase == null)
            return null;
        return adminInDatabase;
    }

    public async Task<List<Admin>> FindMany(Guid[] adminIds)
    {
        List<Admin> adminsInDatabase = await db.Admin.Where(A => adminIds.Contains(A.AdminId)).ToListAsync();
        return adminsInDatabase;
    }
}
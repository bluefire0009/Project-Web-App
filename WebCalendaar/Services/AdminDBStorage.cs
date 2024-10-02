using Microsoft.EntityFrameworkCore;
using WebCalendaar.Models;

public class AdminDBStorage : IAdminStorage
{
    private DatabaseContext db;

    public AdminDBStorage(DatabaseContext db)
    {
        this.db = db;
    }

    public async Task Create(Admin admin)
    {
        Admin? adminInDatabase = db.Admin.FirstOrDefault(a => a.AdminId == admin.AdminId);

        admin.AdminId = Guid.NewGuid();
        await db.Admin.AddAsync(admin);
        await db.SaveChangesAsync();
    }

    public Task Delete(Guid adminId)
    {
        throw new NotImplementedException();
    }

    public Task<Admin?> Find(Guid adminId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Admin>> FindMany(Guid[] adminIds)
    {
        throw new NotImplementedException();
    }
}
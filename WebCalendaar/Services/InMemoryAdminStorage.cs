using WebCalendaar.Models;

public class InMemoryAdminStorage : IAdminStorage
{
    public static List<Admin> admins = new();

    public async Task<bool> Create(Admin admin)
    {
        admin.AdminId = Guid.NewGuid();
        await Task.Delay(0);
        admins.Add(admin);
        return true;
    }

    public async Task<bool> Delete(Guid adminId)
    {
        await Task.Delay(0);
        admins.Remove(admins.Find(a => a.AdminId == adminId));
        return true;
    }

    public async Task<Admin?> Find(Guid adminId)
    {
        await Task.Delay(0);
        return admins.Find(a => a.AdminId == adminId);
    }

    public async Task<List<Admin>> FindMany(Guid[] adminIds)
    {
        List<Admin> found = new();
        await Task.Delay(0);

        admins.ForEach(a => 
        {
            adminIds.ToList().ForEach(id =>
            {
                if (a.AdminId == id)
                    found.Add(a);
            });
        });
        return found;
    }
}
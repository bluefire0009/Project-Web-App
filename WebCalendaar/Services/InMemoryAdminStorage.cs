using WebCalendaar.Models;

public class InMemoryAdminStorage : IAdminStorage
{
    List<Admin> admins;
    public InMemoryAdminStorage()
    {
        admins = new List<Admin>();
    }

    public async Task Create(Admin admin)
    {
        await Task.Delay(0);
        admins.Add(admin);
    }

    public async Task Delete(Guid adminId)
    {
        await Task.Delay(0);
        admins.Remove(admins.Find(a => a.AdminId == adminId));
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
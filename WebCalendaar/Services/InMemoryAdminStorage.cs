using WebCalendaar.Models;

public class InMemoryAdminStorage : IAdminStorage
{
    public static List<Admin> admins = new();

    public async Task<bool> Create(Admin admin)
    {
        await Task.Delay(0);

        // adds admin to a list of admins? : Deze code was al in de template
        admins.Add(admin);
        return true;
    }

    public async Task<bool> Delete(int adminId)
    {
        await Task.Delay(0);
        admins.Remove(admins.Find(a => a.AdminId == adminId));
        return true;
    }

    public async Task<Admin?> Find(int adminId)
    {
        await Task.Delay(0);
        return admins.Find(a => a.AdminId == adminId);
    }

    public async Task<List<Admin>> FindMany(int[] adminIds)
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

    // this was added later when not needed anymore
    public Task<bool> Update(Admin admin)
    {
        throw new NotImplementedException();
    }
}
using WebCalendaar.Models;
public interface IAdminStorage
{
    Task Create(Admin admin);
    Task Delete(Guid adminId);
    Task<Admin?> Find(Guid adminId);
    Task<List<Admin>> FindMany(Guid[] adminIds);
}
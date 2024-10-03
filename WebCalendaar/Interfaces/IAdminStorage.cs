using WebCalendaar.Models;
public interface IAdminStorage
{
    Task<bool> Create(Admin admin);
    Task<bool> Update(Admin admin);
    Task<bool> Delete(int adminId);
    Task<Admin?> Find(int adminId);
    Task<List<Admin>> FindMany(int[] adminIds);
}
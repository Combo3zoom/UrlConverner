using DataLayer.Model;

namespace DataLayer.Repository.Interface;

public interface IDescriptionRepository: IRepository<Description, int>
{
    Task Update(string text);
}
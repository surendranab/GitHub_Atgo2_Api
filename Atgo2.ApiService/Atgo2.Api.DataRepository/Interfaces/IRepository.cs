using Atgo2.Api.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Atgo2.Api.DataRepository.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<BaseModel> Insert(TEntity item, int currentUserId);
        Task Delete(int id, int currentUserId);
        Task<BaseModel> Update(TEntity item, int currentUserId);
        Task<TEntity> FindById(int id, int currentUserId);
        Task<IEnumerable<TEntity>> FindAll(int currentUserId);
        Task<IEnumerable<TEntity>> FindAllGridData(bool isActive, string searchText, string sortColumn, string sortBy, int pageNumber, int pageSize, int currentUserId);

    }
}

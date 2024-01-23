using Ardalis.Specification;

namespace DepsTemplate.SharedKernel.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
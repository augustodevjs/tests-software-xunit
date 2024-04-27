using System.Linq.Expressions;

namespace Feature.Core;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    int Commit();
    void Remover(Guid id);
    void Atualizar(TEntity obj);
    TEntity ObterPorId(Guid id);
    void Adicionar(TEntity obj);
    IEnumerable<TEntity> ObterTodos();
    IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
}
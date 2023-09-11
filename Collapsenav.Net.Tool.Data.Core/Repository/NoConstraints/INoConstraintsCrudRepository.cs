namespace Collapsenav.Net.Tool.Data;
public interface INoConstraintsCrudRepository<T> : INoConstraintsModifyRepository<T>, INoConstraintsQueryRepository<T>
{
}
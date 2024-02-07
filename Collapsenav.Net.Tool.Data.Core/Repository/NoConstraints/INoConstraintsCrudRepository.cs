namespace Collapsenav.Net.Tool.Data;
/// <summary>
/// 无泛型约束的增删查改仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public interface INoConstraintsCrudRepository<T> : INoConstraintsModifyRepository<T>, INoConstraintsQueryRepository<T>
{
}
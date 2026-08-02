using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;
/// <summary>
/// 自定义的数据库上下文对象
/// </summary>
public interface IDB
{
    /// <summary>
    /// 获取 IQueryable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IQueryable<T> Set<T>() where T : class;
    /// <summary>
    /// 单个添加
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data">需要添加的实体</param>
    /// <returns></returns>
    T Add<T>(T? data) where T : class;
    /// <summary>
    /// 添加或更新
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    (T entity, bool isNew) AddOrUpdate<T>(T? data) where T : class;
    /// <summary>
    /// 添加或更新
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    Task<(T entity, bool isNew)> AddOrUpdateAsync<T>(T? data) where T : class;
    /// <summary>
    /// 批量添加
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="datas">需要添加的实体</param>
    /// <returns></returns>
    IEnumerable<T> Add<T>(IEnumerable<T>? datas) where T : class;
    /// <summary>
    /// 单个添加
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data">需要添加的实体</param>
    /// <returns></returns>
    Task<T> AddAsync<T>(T? data) where T : class;
    /// <summary>
    /// 批量添加
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="datas">需要添加的实体</param>
    Task<IEnumerable<T>> AddAsync<T>(IEnumerable<T>? datas) where T : class;
    /// <summary>
    /// 根据条件进行删除
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exp"></param>
    /// <param name="isTrue">是否软删除</param>
    /// <returns></returns>
    int Delete<T>(Expression<Func<T, bool>>? exp, bool isTrue = false) where T : class;
    /// <summary>
    /// 根据条件进行删除
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exp"></param>
    /// <param name="isTrue">是否软删除</param>
    /// <returns></returns>
    Task<int> DeleteAsync<T>(Expression<Func<T, bool>>? exp, bool isTrue = false) where T : class;
    /// <summary>
    /// 根据ID删除
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="ID"></typeparam>
    /// <param name="id"></param>
    /// <param name="isTrue">是否软删除</param>
    /// <returns></returns>
    int DeleteById<T, ID>(ID? id, bool isTrue = false) where T : class;
    /// <summary>
    /// 根据ID批量删除
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="ID"></typeparam>
    /// <param name="ids"></param>
    /// <param name="isTrue">是否软删除</param>
    /// <returns></returns>
    int DeleteByIds<T, ID>(IEnumerable<ID>? ids, bool isTrue = false) where T : class;
    /// <summary>
    /// 根据ID删除
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="ID"></typeparam>
    /// <param name="id"></param>
    /// <param name="isTrue">是否软删除</param>
    /// <returns></returns>
    Task<int> DeleteByIdAsync<T, ID>(ID? id, bool isTrue = false) where T : class;
    /// <summary>
    /// 根据ID批量删除
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="ID"></typeparam>
    /// <param name="ids"></param>
    /// <param name="isTrue">是否软删除</param>
    /// <returns></returns>
    Task<int> DeleteByIdsAsync<T, ID>(IEnumerable<ID>? ids, bool isTrue = false) where T : class;
    /// <summary>
    /// 单个修改
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    T Update<T>(T? data) where T : class;
    /// <summary>
    /// 单个修改
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    Task<T> UpdateAsync<T>(T? data) where T : class;
    /// <summary>
    /// 批量修改
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    IEnumerable<T> Update<T>(IEnumerable<T>? data) where T : class;
    /// <summary>
    /// 批量修改
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> UpdateAsync<T>(IEnumerable<T>? data) where T : class;
    /// <summary>
    /// 批量更新(默认带事务)
    /// </summary>
    /// <param name="where">匹配的查询表达式</param>
    /// <param name="entity">更新用的表达式</param>
    /// <returns></returns>
    Task<int> UpdateAsync<T>(Expression<Func<T, bool>>? where, Expression<Func<T, T>>? entity) where T : class;
    /// <summary>
    /// 根据id查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="ID"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    T FindById<T, ID>(ID? id) where T : class;
    /// <summary>
    /// 根据id查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="ID"></typeparam>
    /// <param name="ids"></param>
    /// <returns></returns>
    IEnumerable<T> FindByIds<T, ID>(IEnumerable<ID>? ids) where T : class;
    /// <summary>
    /// 根据id查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="ID"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T> FindByIdAsync<T, ID>(ID? id) where T : class;
    /// <summary>
    /// 根据id查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="ID"></typeparam>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> FindByIdsAsync<T, ID>(IEnumerable<ID>? ids) where T : class;
    IQueryable<T> Query<T>(Expression<Func<T, bool>>? exp = null) where T : class;
    void BeginTransaction();
    Task CommitAsync();
    void Rollback();
    int SaveChanges();
    Task<int> SaveChangesAsync();
}
/// <summary>
/// 自定义的数据库上下文对象
/// </summary>
/// <typeparam name="T">可以是EFCore,SqlSugar,Dapper等任意ORM框架的实体类型</typeparam>
public interface IDB<T> : IDB { }
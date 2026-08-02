#nullable disable
namespace Collapsenav.Net.Tool.Data;

public partial class EntityModifyRepository<T>
{
    public override Task<int> DeleteAsync<TKey>(TKey id, bool isTrue = false)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));
        if (isTrue)
            return base.DeleteAsync(id, true);
        return DeleteSoftByIdAsync(id);
    }
    public override Task<int> DeleteByIdsAsync<TKey>(IEnumerable<TKey> ids, bool isTrue = false)
    {
        if (ids == null)
            throw new ArgumentNullException(nameof(ids));
        if (isTrue)
            return base.DeleteByIdsAsync(ids, true);
        return DeleteSoftByIdsAsync(ids);
    }

    private async Task<int> DeleteSoftByIdAsync<TKey>(TKey id)
    {
        var entity = await _db.FindByIdAsync<T, TKey>(id);
        if (entity == null) return 0;
        entity.SoftDelete();
        return 1;
    }
    private async Task<int> DeleteSoftByIdsAsync<TKey>(IEnumerable<TKey> ids)
    {
        int count = 0;
        foreach (var id in ids)
        {
            var entity = await _db.FindByIdAsync<T, TKey>(id);
            if (entity == null) continue;
            entity.SoftDelete();
            count++;
        }
        return count;
    }
}

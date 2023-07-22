using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;

public class JoinItem<T>
        where T : class, IEntity
{
    private IRepository rep;
    public JoinItem(IRepository rep)
    {
        this.rep = rep;
    }

    public void LeftJoin<T2, KeyProp>(Expression<Func<T, KeyProp>> LKey, Expression<Func<T2, KeyProp>> RKey) where T2 : class, IEntity
    {
        var item = new JoinItem<T2>(rep);
    }
}
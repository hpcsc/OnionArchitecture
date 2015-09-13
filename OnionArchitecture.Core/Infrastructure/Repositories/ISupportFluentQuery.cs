namespace OnionArchitecture.Core.Infrastructure.Repositories
{
    public interface ISupportFluentQuery<out TQueryBuilder>
        where TQueryBuilder : IAmQueryBuilder
    {
        TQueryBuilder Query();
    }
}

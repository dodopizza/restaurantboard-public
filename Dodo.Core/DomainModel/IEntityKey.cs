namespace Dodo.Core.DomainModel
{
    public interface IEntityKey<TKey>
    {
        TKey Id { get; }
    }
}
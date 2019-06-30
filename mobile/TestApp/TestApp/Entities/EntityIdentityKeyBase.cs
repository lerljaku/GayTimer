namespace GayTimer.Entities
{
    public abstract class EntityIdentityKeyBase<TKey>
    {
        public TKey Id { get; set; }

        public string TableName => GetType().Name;
    }
}
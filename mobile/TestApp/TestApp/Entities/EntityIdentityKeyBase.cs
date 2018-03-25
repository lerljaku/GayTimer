namespace GayTimer.Entities
{
    public abstract class EntityIdentityKeyBase<TKey>
    {
        public TKey Id { get; set; }

        public abstract string TableName { get; }
    }
}
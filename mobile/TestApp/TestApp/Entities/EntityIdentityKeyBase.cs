using SQLite;

namespace GayTimer.Entities
{
    public abstract class EntityIdentityKeyBase<TKey>
    {
        [PrimaryKey, AutoIncrement]
        public TKey Id { get; set; }

        public string TableName => GetType().Name;
    }
}
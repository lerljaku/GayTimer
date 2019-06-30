namespace GayTimer.Entities
{
    public class Deck : EntityIdentityKeyBase<int>
    {
        public string Name { get; set; }

        public int PlayerId { get; set; }

        public string Commander { get; set; }

        public string Note { get; set; }
    }
}
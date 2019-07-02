using GayTimer.Entities;

namespace GayTimer.Events
{
    public class DeckInserted
    {
        public DeckInserted(Deck deck)
        {
            Deck = deck;
        }

        public Deck Deck { get; }
    }
}
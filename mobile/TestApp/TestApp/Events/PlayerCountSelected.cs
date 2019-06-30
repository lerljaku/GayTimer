namespace GayTimer.Events
{
    public class PlayerCountSelected
    {
        public PlayerCountSelected(int playerCount)
        {
            PlayerCount = playerCount;
        }

        public int PlayerCount { get; }
    }
}
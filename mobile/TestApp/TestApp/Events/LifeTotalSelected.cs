namespace GayTimer.Events
{
    public class LifeTotalSelected
    {
        public LifeTotalSelected(short startingLifeTotal)
        {
            StartingLifeTotal = startingLifeTotal;
        }

        public short StartingLifeTotal { get; }
    }
}
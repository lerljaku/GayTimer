using GayTimer.Entities;

namespace GayTimer.Services
{
    public interface ICurrentUser
    {
        Player User { get; set; }
    }

    public class CurrentUser : ICurrentUser
    {
        public Player User { get; set; }
    }
}
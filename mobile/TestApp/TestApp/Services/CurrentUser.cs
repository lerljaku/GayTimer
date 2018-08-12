using GayTimer.Entities;

namespace GayTimer.Services
{
    public interface ICurrentUser
    {
        Gay User { get; set; }
    }

    public class CurrentUser : ICurrentUser
    {
        public Gay User { get; set; }
    }
}
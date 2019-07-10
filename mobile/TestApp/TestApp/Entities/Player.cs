using System;

namespace GayTimer.Entities
{
    public class Player : EntityIdentityKeyBase<int>
    {
        public string Nick { get; set; }
        
        public DateTime Created { get; set; }

        public static Player Dummy(int i)
        {
            return new Player(){Nick = $"Gay {i}"};
        }
    }
}

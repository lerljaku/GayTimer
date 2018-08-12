using System;

namespace GayTimer.Entities
{
    public class Gay : EntityIdentityKeyBase<int>
    {
        public override string TableName { get; } = "Gay";

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Nick { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public DateTime Created { get; set; }
    }
}

using System;

namespace Day3
{
    public class Vote
    {
        public Party Party { get; }

        public Vote(Party party)
        {
            if(Utilities.IsNull(party))
            {
                throw new ArgumentNullException(nameof(party));
            }

            Party = party;
            //_voteDate = DateTime.UtcNow;  // jeśli nie ma konkretnych wymagań używamy UtcNow !!!!!!!!!!!!!PRZECZYTAĆ O DateTimeOffset!!!!!!!!!!!!!!
        }
    }
}

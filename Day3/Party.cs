using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
    public class Party
    {
        private string _name;
        private uint _voteCount;

        public Party(string name)
        {
            if(Utilities.IsNull(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            _name = name;
        }

        public uint GetVoteCount()
        {
            return _voteCount;
        }

        // Nie podoba mi się, że ta metoda jest publiczna
        public void AddVote()
        {
            _voteCount++;
        }

    }
}

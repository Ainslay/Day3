using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
    public class ElectoralCommission
    {
        private Dictionary<string, Vote> _votes;
        private DateTime _dateOfVote;

        public ElectoralCommission()
        {
            _votes = new Dictionary<string, Vote>();
            _dateOfVote = DateTime.UtcNow;
        }

        public void PlaceVote(Person person, Vote vote)
        {
            if(Utilities.IsNull(vote))
            {
                throw new ArgumentNullException(nameof(vote));
            }
            if (Utilities.IsNull(person))
            {
                throw new ArgumentNullException(nameof(person));
            }

            if (person.IsAdult() && !_votes.ContainsKey(person.GetPesel()))
            {
                _votes.Add(person.GetPesel(), vote);
                vote.Party.AddVote();
            }
        }

        public Party DetermineWinner(Dictionary<string, Party> parties)
        {
            if(Utilities.IsNull(parties))
            {
                throw new ArgumentNullException(nameof(parties));
            }

            if(parties.Any() && _votes.Count > 0)
            {
                foreach (var party in parties)
                {
                    foreach (var vote in _votes)
                    {
                        if (party.Key.Equals(vote.Value.Party))
                        {
                            party.Value.AddVote();
                        }
                    }
                }

                var winner = parties.Aggregate((first, second) => first.Value.GetVoteCount() > second.Value.GetVoteCount() ? first : second).Value;

                return winner;

                //var maxNumberOfVotes = parties.Values.Max(party => party.GetVoteCount());
                //var winner = parties.Values.Where(party => party.GetVoteCount() == maxNumberOfVotes);
            }

            return null;
        }
    }
}

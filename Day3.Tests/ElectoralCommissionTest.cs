using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Day3.Tests
{
    public class ElectoralCommissionTest
    {
        [Theory]
        [PlaceVoteData]
        public void Given_InvalidParameters_When_CallingPlaceVote_Then_ThrowsArgumentNullException(Person person, Vote vote)
        {
            var commission = new ElectoralCommission();
            
            Assert.Throws<ArgumentNullException>(() => commission.PlaceVote(person, vote));
        }

        [Fact]
        public void Given_ValidParameters_When_CallingPlaceVote_Then_IncrementsPartyVoteCount()
        {
            var commission = new ElectoralCommission();
            var dave = new Person("Dave", "Wozniak", "12345678901", new DateTime(1998, 3, 11));
            var party = new Party("WASD");
            var vote = new Vote(party);
            uint expected = 1;

            commission.PlaceVote(dave, vote);
            var actual = party.GetVoteCount();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Given_SameVoterMoreThanOnce_When_CallingPlaceVote_Then_DoesNotIncrementsPartyVoteCount()
        {
            var commission = new ElectoralCommission();
            var dave = new Person("Dave", "Wozniak", "12345678901", new DateTime(1998, 3, 11));
            var party = new Party("WASD");
            var vote = new Vote(party);
            uint expected = 1;

            commission.PlaceVote(dave, vote);
            commission.PlaceVote(dave, vote);
            var actual = party.GetVoteCount();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Gicen_NotAdultVoter_When_CallingPlaceVote_Then_DoesNotIncrementVoteCount()
        {
            var commission = new ElectoralCommission();
            var dave = new Person("Dave", "Wozniak", "12345678901", new DateTime(2005, 3, 11));
            var party = new Party("WASD");
            var vote = new Vote(party);
            uint expected = 0;

            commission.PlaceVote(dave, vote);
            var actual = party.GetVoteCount();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Given_ValidCollection_When_CallingDetermineWinner_Then_ReturnsWinner()
        {
            var commission = new ElectoralCommission();
            Dictionary<string, Party> parties = new Dictionary<string, Party>
            {
                { "WASD", new Party("WASD") },
                { "QWERTY", new Party("QWERTY") },
                { "SPACE", new Party("SPACE") }
            };

            var dave = new Person("Dave", "Wozniak", "12345678901", new DateTime(1998, 3, 11));
            var kitty = new Person("Kitty", "Wozniak", "92345678901", new DateTime(2000, 5, 1));
            var tom = new Person("Tom", "Wozniak", "82345678901", new DateTime(1980, 7, 12));
            var jerry = new Person("Jerry", "Wozniak", "74321678901", new DateTime(1974, 7, 12));

            var expected = parties["WASD"];

            dave.Vote(parties["WASD"], commission);
            tom.Vote(parties["QWERTY"], commission);
            kitty.Vote(parties["WASD"], commission);
            jerry.Vote(parties["SPACE"], commission);

            var actual = commission.DetermineWinner(parties);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Given_ValidCollectionWithoutAnyVotes_When_CallingDetermineWinner_Then_ReturnsNull()
        {
            var commission = new ElectoralCommission();
            Dictionary<string, Party> parties = new Dictionary<string, Party>
            {
                { "WASD", new Party("WASD") },
                { "QWERTY", new Party("QWERTY") },
                { "SPACE", new Party("SPACE") }
            };

            var result = commission.DetermineWinner(parties);

            Assert.Null(result);
        }

        [Fact]
        public void Given_NullCollection_When_CallingDetermineWinner_Then_ThrowsArgumentNullException()
        {
            var commission = new ElectoralCommission();
            Dictionary<string, Party> parties = null;

            Assert.Throws<ArgumentNullException>(() => commission.DetermineWinner(parties));
        }

        [Fact]
        public void Given_EmptyCollection_When_CallingDetermineWinner_Then_ReturnsNull()
        {
            var commission = new ElectoralCommission();
            var parties = new Dictionary<string, Party>();
            Party result;

            result = commission.DetermineWinner(parties);

            Assert.Null(result);
        }

        private class PlaceVoteDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo testMethod)
            {
                yield return new object[] { null, null };
                yield return new object[] { null, new Vote(new Party("WASD")) };
                yield return new object[] { new Person("Jan", "Kowalski", "12345678909", new DateTime(1998, 10, 10)), null };
            }
        }
    }
}

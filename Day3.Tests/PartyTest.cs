using System;

using Xunit;

namespace Day3.Tests
{
    public class PartyTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Given_InvalidNameParameter_When_ConstructingParty_Then_ThrowsArgumentNullException(string name)
        {
            Assert.Throws<ArgumentNullException>(() => new Party(name));
        }

        [Fact]
        public void Given_NoParams_When_CallingGetVoteCount_Then_ReturnsVoteCount()
        {
            var party = new Party("WASD");
            party.AddVote();
            uint expected = 1;

            var actual = party.GetVoteCount();

            Assert.Equal(expected, actual);
        }
    }
}

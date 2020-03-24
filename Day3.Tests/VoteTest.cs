using System;

using Xunit;

namespace Day3.Tests
{
    public class VoteTest
    {
        [Fact]
        public void Given_NullParameter_When_ConstructingVote_Then_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Vote(null));
        }
    }
}

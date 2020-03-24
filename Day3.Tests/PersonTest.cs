using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Day3.Tests
{
    public class PersonTest
    {
        // Ten assert mi siê nie podoba, ten test w ogóle jest zbêdny jeœli testujê wszystkie 
        // nieprawid³owe przypadki
        [Fact]
        public void Given_ValidParameters_When_ConstrutctingPerson_Then_ConstructsPerson()
        {
            var person = new Person("Jan", "Kowalski", "12345678909", new DateTime(1998, 12, 12));

            Assert.NotNull(person);
        }

        [Theory]
        [PersonConstructionData]
        public void Given_InvalidNameParameter_When_ConstructingPerson_Then_ThrowsArgumentNullException(string name)
        {
            Assert.Throws<ArgumentNullException>
                (
                   () => new Person(name, "Kowalski", "12345678909", new DateTime(1998, 12, 12))
                );
        }

        [Theory]
        [PersonConstructionData]
        public void Given_InvalidSurnameParameter_When_ConstructingPerson_Then_ThrowsArgumentNullException(string surname)
        {
            Assert.Throws<ArgumentNullException>
                (
                   () => new Person("Jan", surname, "12345678909", new DateTime(1998, 12, 12))
                );
        }

        [Theory]
        [PersonConstructionData]
        [InlineData("123")]
        [InlineData("1234ab56789")]
        public void Given_InvalidPeselParameter_When_ConstructingPerson_Then_ThrowsArgumentException(string pesel)
        {
            Assert.Throws<ArgumentException>
                (
                   () => new Person("Jan", "Kowalski", pesel, new DateTime(1998, 12, 12))
                );
        }

        [Theory]
        [PersonBirthdayData]    // InlineData nie chcia³o przyjaæ czystego DateTime
        public void Given_InvalidBirthdayParameter_When_ConstructingPerson_Then_ThrowsArgumentException(DateTime birthday)
        {
            Assert.Throws<ArgumentException>
                (
                   () => new Person("Jan", "Kowalski", "12345678909", birthday)
                );
        }

        private class PersonConstructionDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo testMethod)
            {
                yield return new object[] { null };
                yield return new object[] { "" };
                yield return new object[] { "   " };
            }
        }

        private class PersonBirthdayDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo testMethod)
            {
                yield return new object[] { new DateTime(1900, 12, 12) };
                yield return new object[] { new DateTime(2023, 5, 22) };
            }
        }
    }
}

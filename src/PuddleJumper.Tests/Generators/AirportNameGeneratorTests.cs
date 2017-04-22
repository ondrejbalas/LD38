using NUnit.Framework;
using PuddleJumper.Core.Generators;

namespace PuddleJumper.Tests.Generators
{
    public class AirportNameGeneratorTests
    {
        [Test]
        public void GetAirportName_AnyCharacter_ProducesAirportNameStartingWithThatCharacter()
        {
            var gen = new AirportNameGenerator();

            for (int i = 'A'; i <= 'Z'; i++)
            {
                var name = gen.GetAirportName((char) i);
                Assert.That(name[0], Is.EqualTo(i));
            }
        }
    }
}
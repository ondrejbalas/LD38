using Duality;
using NUnit.Framework;
using PuddleJumper.Core.Helpers;

namespace PuddleJumper.Tests.Helpers
{
    public class MathHelperTests
    {
        [Test]
        public void GetDistance_ReturnsDistance()
        {
            var v1 = new Vector2(100, 100);
            var v2 = new Vector2(200, 200);
            Assert.That(v1.GetDistance(v2), Is.EqualTo(MathF.Sqrt(20000)).Within(1.0f));
        }
    }
}
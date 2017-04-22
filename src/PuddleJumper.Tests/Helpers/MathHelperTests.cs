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

        [Test]
        public void GetColorString_ReturnsColorString()
        {
            var green = 1.0f;
            var yellow = 0.6f;
            var orange = 0.4f;
            var darkred = 0.0f;

            Assert.That(MathHelpers.GetColorString(green), Is.EqualTo("/c00ff00ff"));
            Assert.That(MathHelpers.GetColorString(yellow), Is.EqualTo("/cffff00ff"));
            Assert.That(MathHelpers.GetColorString(orange), Is.EqualTo("/cff8000ff"));
            Assert.That(MathHelpers.GetColorString(darkred), Is.EqualTo("/c800000ff"));
        }
    }
}
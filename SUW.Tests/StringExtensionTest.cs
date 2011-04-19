using NUnit.Framework;
using SUW.Extensions;

namespace SUW.Tests
{
    [TestFixture]
    public class StringExtensionTest
    {
        [Test]
        public void ShouldRemoveExtraSpaces()
        {
            Assert.AreEqual("this is a clean string", " this  is a  clean string   ".WithoutExtraSpaces());
        }

        [Test]
        public void ShouldSurroundStringWithQuotes()
        {
            Assert.AreEqual("\"Text here\"", "Text here".WithQuotes());
        }
    }
}

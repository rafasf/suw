using System;
using NUnit.Framework;

namespace SUW.Tests
{
    [TestFixture]
    public class InputParserTest
    {
        [Test]
        public void ShouldHaveThreeParameters()
        {
            var parser = new InputParser(@"-c C:\RootDir C:\Shortcuts");
            Assert.AreEqual(3, parser.Parameters.Count);
        }

        [Test]
        public void ShouldHaveTwoParameters()
        {
            var parser = new InputParser(@"-c C:\RootDir");
            Assert.AreEqual(2, parser.Parameters.Count);
        }

        [Test]
        public void ShouldAcceptDirectoryWithSpace()
        {
            var parser = new InputParser(@"-u C:\Root Dir C:\Shortcut");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldNotAcceptOneParameter()
        {
            new InputParser(@"-c");
        }

        [Test]
        public void ShouldMapParametersCorrectly()
        {
            var parser = new InputParser(@"-c C:\RootDir C:\Shortcurts");

            Assert.AreEqual("-c", parser.Parameters[ParameterName.Operation]);
            Assert.AreEqual(@"C:\RootDir", parser.Parameters[ParameterName.RootDir]);
            Assert.AreEqual(@"C:\Shortcurts", parser.Parameters[ParameterName.ShortcutDir]);
        }
    }
}

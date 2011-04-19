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
            var parameters = new[] {"-c", @"C:\RootDir", @"C:\Shortcuts"};
            var parser = new InputParser(parameters);

            Assert.AreEqual(3, parser.Parameters.Count);
        }

        [Test]
        public void ShouldHaveTwoParameters()
        {
            var parameters = new[] {"-c", @"C:\RootDir"};
            var parser = new InputParser(parameters);

            Assert.AreEqual(2, parser.Parameters.Count);
        }

        [Test]
        public void ShouldAcceptDirectoryWithSpace()
        {
            var parameters = new[] {"-u", @"C:\Root Dir", @"C:\Shortcut"};
            var parser = new InputParser(parameters);

            Assert.AreEqual("-u", parser.Parameters[ParameterName.Operation]);
            Assert.AreEqual(@"C:\Root Dir", parser.Parameters[ParameterName.RootDir]);
            Assert.AreEqual(@"C:\Shortcut", parser.Parameters[ParameterName.ShortcutDir]);
     } 

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldNotAcceptOneParameter()
        {
            new InputParser(new[] {"-c"});
        }

        [Test]
        public void ShouldMapParametersCorrectly()
        {
            var parameters = new[] { "-u", @"C:\RootDir", @"C:\Shortcuts" };
            var parser = new InputParser(parameters);

            Assert.AreEqual("-u", parser.Parameters[ParameterName.Operation]);
            Assert.AreEqual(@"C:\RootDir", parser.Parameters[ParameterName.RootDir]);
            Assert.AreEqual(@"C:\Shortcuts", parser.Parameters[ParameterName.ShortcutDir]);
        }
    }
}

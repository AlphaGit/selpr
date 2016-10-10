using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SELPR.Commands;

namespace SecurityEventLogProcessReader.UnitTest
{
    [TestClass]
    public class BrowseFileCommandTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldValidateParameters()
        {
            new BrowseFileCommand(null);
        }
    }
}

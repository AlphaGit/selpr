using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SELPR;
using SELPR.Services;

namespace SecurityEventLogProcessReader.UnitTest.Services
{
    [TestClass]
    public class EventLogFileServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldCheckForNullSecurityEventLogFileParser()
        {
            var processTreeGenerator = Mock.Of<IProcessTreeGenerator>();

            new EventLogFileService(null, processTreeGenerator);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldCheckForNullProcessTreeGenerator()
        {
            var securityEventLogFileParser = Mock.Of<ISecurityEventLogFileParser>();

            new EventLogFileService(securityEventLogFileParser, null);
        }

        [TestMethod]
        public void OpenFile_ShouldCallOpenEventLogFileFromSecurityEventLogFileParser()
        {
            var securityEventLogFileParserMock = new Mock<ISecurityEventLogFileParser>();
            var processTreeGenerator = Mock.Of<IProcessTreeGenerator>();

            securityEventLogFileParserMock.Setup(m => m.OpenEventLogFile(It.IsAny<string>())).Returns(new List<SecurityEventLogEntry>());

            var eventLogFileService = new EventLogFileService(securityEventLogFileParserMock.Object, processTreeGenerator);
            eventLogFileService.OpenFile("someFile.etvx");

            securityEventLogFileParserMock.Verify(m => m.OpenEventLogFile(It.Is<string>(s => s == "someFile.etvx")), Times.Once);
        }

        [TestMethod]
        public void OpenFile_ShouldReturnTheResultFromParsingTheEntriesIntoATree()
        {
            var securityEventLogFileParser = Mock.Of<ISecurityEventLogFileParser>();
            var processTreeGeneratorMock = new Mock<IProcessTreeGenerator>();

            var expectedList = new List<ProcessDescriptor>()
            {
                new ProcessDescriptor(),
                new ProcessDescriptor()
            };
            processTreeGeneratorMock.Setup(m => m.ParseLogEntriesToProcessTree(It.IsAny<List<SecurityEventLogEntry>>())).Returns(expectedList);

            var eventLogFileService = new EventLogFileService(securityEventLogFileParser, processTreeGeneratorMock.Object);
            var result = eventLogFileService.OpenFile("someFile.etvx");

            processTreeGeneratorMock.Verify(m => m.ParseLogEntriesToProcessTree(It.IsAny<List<SecurityEventLogEntry>>()), Times.Once);
            CollectionAssert.AreEqual(expectedList, result);
        }
    }
}
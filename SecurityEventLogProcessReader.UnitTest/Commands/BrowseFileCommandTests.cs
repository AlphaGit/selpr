using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SELPR.Commands;
using SELPR.UiAbstractions;

namespace SecurityEventLogProcessReader.UnitTest.Commands
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

        [TestMethod]
        public void Execute_ShouldSetTheMultiSelectOptionToFalse()
        {
            var openFileDialogMock = new Mock<IOpenFileDialog>();
            var browseFileCommand = new BrowseFileCommand(openFileDialogMock.Object);

            browseFileCommand.Execute();

            openFileDialogMock.VerifySet(ofd => ofd.Multiselect = false);
        }

        [TestMethod]
        public void Execute_ShouldSetTheAllowedFiles()
        {
            var openFileDialogMock = new Mock<IOpenFileDialog>();
            var browseFileCommand = new BrowseFileCommand(openFileDialogMock.Object);

            browseFileCommand.Execute();

            openFileDialogMock.VerifySet(ofd => ofd.Filter = It.IsRegex(@"\|\*\.evtx(\||$)"));
        }

        [TestMethod]
        public void Execute_ShouldShowTheDialog()
        {
            var openFileDialogMock = new Mock<IOpenFileDialog>();
            var browseFileCommand = new BrowseFileCommand(openFileDialogMock.Object);

            browseFileCommand.Execute();

            openFileDialogMock.Verify(ofd => ofd.ShowDialog(), Times.Once);
        }

        [TestMethod]
        public void Execute_ShouldReturnTheOpenFileDialogFileName()
        {
            var expectedResult = "File test value";
            var openFileDialogMock = new Mock<IOpenFileDialog>();
            openFileDialogMock.SetupGet(ofd => ofd.FileName).Returns(expectedResult);
            openFileDialogMock.Setup(ofd => ofd.ShowDialog()).Returns(true);
            var browseFileCommand = new BrowseFileCommand(openFileDialogMock.Object);

            var result = browseFileCommand.Execute();

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Execute_ShouldReturnNullIfTheUserCancelsTheDialog()
        {
            var openFileDialogMock = new Mock<IOpenFileDialog>();
            openFileDialogMock.SetupGet(ofd => ofd.FileName);
            openFileDialogMock.Setup(ofd => ofd.ShowDialog()).Returns(false);
            var browseFileCommand = new BrowseFileCommand(openFileDialogMock.Object);

            var result = browseFileCommand.Execute();

            Assert.IsNull(result);
        }
    }
}

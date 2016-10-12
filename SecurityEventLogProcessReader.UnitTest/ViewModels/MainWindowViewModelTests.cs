using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SELPR;
using SELPR.Commands;
using SELPR.Services;
using SELPR.ViewModels;

namespace SecurityEventLogProcessReader.UnitTest.ViewModels
{
    [TestClass]
    public class MainWindowViewModelTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ChecksForNullIBrowsseFileCommand()
        {
            var eventLogFileService = Mock.Of<IEventLogFileService>();

            new MainWindowViewModel(null, eventLogFileService);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Constructor_ChecksForNullIEventLogFileService()
        {
            var browseFileCommand = Mock.Of<IBrowseFileCommand>();

            new MainWindowViewModel(browseFileCommand, null);
        }

        [TestMethod]
        public async Task BrowseFileCommand_OnExecutedExecutesPassedCommand()
        {
            var eventLogFileService = Mock.Of<IEventLogFileService>();
            var browseFileCommandMock = new Mock<IBrowseFileCommand>();

            var mainWindowViewModel = new MainWindowViewModel(browseFileCommandMock.Object, eventLogFileService);

            await mainWindowViewModel.BrowseFileCommand.Execute();

            browseFileCommandMock.Verify(m => m.Execute(), Times.Once);
        }

        [TestMethod]
        public async Task BrowseFileCommand_OnExecutedShowsProcessCanvas()
        {
            var eventLogFileServiceMock = new Mock<IEventLogFileService>();
            var browseFileCommandMock = new Mock<IBrowseFileCommand>();

            var processList = new List<ProcessDescriptor>()
            {
                new ProcessDescriptor()
            };

            browseFileCommandMock.Setup(m => m.Execute()).Returns("someFile.etvx");
            eventLogFileServiceMock.Setup(m => m.OpenFile("someFile.etvx")).Returns(processList);

            var mainWindowViewModel = new MainWindowViewModel(browseFileCommandMock.Object, eventLogFileServiceMock.Object);

            await mainWindowViewModel.BrowseFileCommand.Execute();

            eventLogFileServiceMock.Verify(m => m.OpenFile("someFile.etvx"), Times.Once);

            Assert.IsTrue(mainWindowViewModel.IsProcessCanvasVisible);
            Assert.IsFalse(mainWindowViewModel.IsBrowseButtonVisible);
        }
    }
}
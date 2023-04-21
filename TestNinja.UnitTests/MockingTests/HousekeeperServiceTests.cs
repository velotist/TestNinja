using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;
using IEmailSender = TestNinja.Mocking.IEmailSender;

namespace TestNinja.UnitTests.MockingTests
{
    [TestFixture]
    public class HousekeeperServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IExtraMessageBox> _messageBox;
        private HousekeeperService _service;
        private readonly DateTime _statementDate = new DateTime(2023, 1, 1);
        private Housekeeper _housekeeper;
        private string _filename;

        [SetUp]
        public void SetUp()
        {
            _housekeeper = new Housekeeper
            {
                Email = "a",
                FullName = "b",
                Oid = 1,
                StatementEmailBody = "c"
            };
            _unitOfWork = new Mock<IUnitOfWork>();
            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IExtraMessageBox>();
            
            _service = new HousekeeperService(
                _unitOfWork.Object,
                _statementGenerator.Object,
                _emailSender.Object,
                _messageBox.Object);

            _unitOfWork
                .Setup(unitOfWork =>
                    unitOfWork.Query<Housekeeper>())
                .Returns(new List<Housekeeper>
                    {
                        _housekeeper
                    }
                    .AsQueryable());
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _statementGenerator
                .Verify(sg=>
                sg.SaveStatement(
                    _housekeeper.Oid,
                    _housekeeper.FullName,
                    _statementDate));
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void SendStatementEmails_NoValidHousekeeperEmail_ShouldNotGenerateStatements(string email)
        {
            _housekeeper.Email = email;

            _service.SendStatementEmails(_statementDate);

            _statementGenerator
                .Verify(sg =>
                        sg.SaveStatement(
                            _housekeeper.Oid,
                            _housekeeper.FullName,
                            _statementDate),
                    Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailStatement()
        {
            _filename = "a";
            _statementGenerator
                .Setup(statementGenerator =>
                    statementGenerator
                        .SaveStatement(
                            _housekeeper.Oid,
                            _housekeeper.FullName,
                            _statementDate))
                .Returns(_filename);

            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(emailSender =>
                emailSender
                    .EmailFile(
                        _housekeeper.Email,
                        _housekeeper.StatementEmailBody, 
                        _filename,
                        It.IsAny<string>()));
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void SendStatementEmails_NoValidStatementFilename_ShouldNotEmailStatement(string filename)
        {
            _statementGenerator
                .Setup(statementGenerator =>
                    statementGenerator
                        .SaveStatement(
                            _housekeeper.Oid,
                            _housekeeper.FullName,
                            _statementDate))
                .Returns(filename);

            _service.SendStatementEmails(_statementDate);

            _emailSender
                .Verify(emailSender =>
                        emailSender.EmailFile(
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<string>()),
                    Times.Never);
        }
    }
}

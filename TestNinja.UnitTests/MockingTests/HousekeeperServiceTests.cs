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
        private string _statementFilename;

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
            _unitOfWork
                .Setup(unitOfWork =>
                    unitOfWork.Query<Housekeeper>())
                .Returns(new List<Housekeeper>
                    {
                        _housekeeper
                    }
                    .AsQueryable());

            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator
                .Setup(statementGenerator =>
                    statementGenerator
                        .SaveStatement(
                            _housekeeper.Oid,
                            _housekeeper.FullName,
                            _statementDate))
                .Returns(() => _statementFilename);

            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IExtraMessageBox>();
            
            _service = new HousekeeperService(
                _unitOfWork.Object,
                _statementGenerator.Object,
                _emailSender.Object,
                _messageBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);

            VerifyStatementGenerated();
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void SendStatementEmails_NoValidHousekeeperEmail_ShouldNotGenerateStatements(string email)
        {
            _housekeeper.Email = email;

            _service.SendStatementEmails(_statementDate);

            VerifyStatementNotGenerated();
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailStatement()
        {
            _statementFilename = "a";

            _service.SendStatementEmails(_statementDate);

            VerifyEmailSent();
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void SendStatementEmails_NoValidStatementFilename_ShouldNotEmailStatement(string statementFilename)
        {
            _statementFilename = statementFilename;

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        private void VerifyEmailSent()
        {
            _emailSender.Verify(emailSender =>
                emailSender
                    .EmailFile(
                        _housekeeper.Email,
                        _housekeeper.StatementEmailBody,
                        _statementFilename,
                        It.IsAny<string>()));
        }

        private void VerifyEmailNotSent()
        {
            _emailSender
                .Verify(emailSender =>
                        emailSender.EmailFile(
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<string>()),
                    Times.Never);
        }

        private void VerifyStatementGenerated()
        {
            _statementGenerator
                .Verify(sg =>
                    sg.SaveStatement(
                        _housekeeper.Oid,
                        _housekeeper.FullName,
                        _statementDate));
        }

        private void VerifyStatementNotGenerated()
        {
            _statementGenerator
                .Verify(sg =>
                        sg.SaveStatement(
                            _housekeeper.Oid,
                            _housekeeper.FullName,
                            _statementDate),
                    Times.Never);
        }
    }
}

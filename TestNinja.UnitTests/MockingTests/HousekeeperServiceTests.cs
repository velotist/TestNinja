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
        public void SendStatementEmails_HousekeeperHasNoEmail_ShouldNotGenerateStatements()
        {
            _housekeeper.Email = null;

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
        public void SendStatementEmails_HousekeeperEmailIsWhitespace_ShouldNotGenerateStatements()
        {
            _housekeeper.Email = " ";

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
        public void SendStatementEmails_HousekeeperEmailIsEmpty_ShouldNotGenerateStatements()
        {
            _housekeeper.Email = "";

            _service.SendStatementEmails(_statementDate);

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

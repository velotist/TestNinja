using System;

namespace TestNinja.Mocking
{
    // Application services are responsible for orchestration.
    // This class is a perfect example for that.
    // Because the method is orchestrating a few different things.
    // So therefore we name this instead of HousekeeperHelper HousekeeperService.
    public class HousekeeperService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStatementGenerator _statementGenerator;
        private readonly IEmailSender _emailSender;
        private readonly IExtraMessageBox _extraMessageBox;

        // For changing the signature of the class, we assume that we don't create breaking changes
        public HousekeeperService(
            IUnitOfWork unitOfWork,
            IStatementGenerator statementGenerator,
            IEmailSender emailSender,
            IExtraMessageBox extraMessageBox)
        {
            _extraMessageBox = extraMessageBox;
            _emailSender = emailSender;
            _statementGenerator = statementGenerator;
            _unitOfWork = unitOfWork;
        }

        public void SendStatementEmails(DateTime statementDate)
        {
            var housekeepers = _unitOfWork.Query<Housekeeper>();

            foreach (var housekeeper in housekeepers)
            {
                if (housekeeper.Email == null)
                    continue;

                var statementFilename = _statementGenerator.SaveStatement(
                    housekeeper.Oid,
                    housekeeper.FullName,
                    statementDate);

                if (string.IsNullOrWhiteSpace(statementFilename))
                    continue;

                var emailAddress = housekeeper.Email;
                var emailBody = housekeeper.StatementEmailBody;

                try
                {
                    _emailSender.EmailFile(
                        emailAddress,
                        emailBody,
                        statementFilename,
                        $"Sandpiper Statement {statementDate:yyyy-MM} {housekeeper.FullName}");
                }
                catch (Exception e)
                {
                    _extraMessageBox.Show(
                        e.Message,
                        $"Email failure: {emailAddress}",
                        MessageBoxButtons.Ok);
                }
            }
        }
    }

    public enum MessageBoxButtons
    {
        Ok
    }

    public class MainForm
    {
        public bool HousekeeperStatementsSending { get; set; }
    }

    public class DateForm
    {
        public DateForm(string statementDate, object endOfLastMonth)
        {
        }

        public DateTime Date { get; set; }

        public DialogResult ShowDialog()
        {
            return DialogResult.Abort;
        }
    }

    public enum DialogResult
    {
        Abort,
        Ok
    }

    public class SystemSettingsHelper
    {
        public static string EmailSmtpHost { get; set; }
        public static int EmailPort { get; set; }
        public static string EmailUsername { get; set; }
        public static string EmailPassword { get; set; }
        public static string EmailFromEmail { get; set; }
        public static string EmailFromName { get; set; }
    }

    public class Housekeeper
    {
        public string Email { get; set; }
        public int Oid { get; set; }
        public string FullName { get; set; }
        public string StatementEmailBody { get; set; }
    }

    public class HousekeeperStatementReport
    {
        public HousekeeperStatementReport(int housekeeperOid, DateTime statementDate)
        {
        }

        public bool HasData { get; set; }

        public void CreateDocument()
        {
        }

        public void ExportToPdf(string filename)
        {
        }
    }
}
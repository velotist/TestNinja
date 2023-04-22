using System;
using System.IO;
using TestNinja.Mocking.Interfaces;

namespace TestNinja.Mocking.Housekeeper
{
    public class StatementGenerator : IStatementGenerator
    {
        public string SaveStatement(int housekeeperOid, string housekeeperName, DateTime statementDate)
        {
            var report = new HousekeeperStatementReport(housekeeperOid, statementDate);

            if (!report.HasData)
                return string.Empty;

            report.CreateDocument();

            var filename = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                $"Sandpiper Statement {statementDate:yyyy-MM} {housekeeperName}.pdf");

            report.ExportToPdf(filename);

            return filename;
        }
    }
}
using TestNinja.Mocking.Housekeeper;

namespace TestNinja.Mocking.Interfaces
{
    public interface IExtraMessageBox
    {
        void Show(string s, string housekeeperStatements, MessageBoxButtons ok);
    }
}
using TCSA.OOP.CodingTracker.Controllers;

namespace TCSA.OOP.CodingTracker;

internal class UserInterface(SessionController sessionController)
{
    internal int Run(string[] args)
    {
        if (args.Length == 0)
            return RunMenu();

        throw new NotImplementedException("Command line parameters are not implemented.");
    }

    private int RunMenu()
    {
        return 0;
    }
}
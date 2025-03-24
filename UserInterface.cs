using Spectre.Console;
using Spectre.Console.Cli;
using TCSA.OOP.CodingTracker.Controllers;
using TCSA.OOP.CodingTracker.View;

namespace TCSA.OOP.CodingTracker;

internal enum MenuOptions
{
    Project,
    Exit
}

internal class UserInterface ()
{
    
    internal int Run(string[] args)
    {
        if (args.Length > 0)
            return RunApp(args);
        
        return RunMenu();
    }

    private int RunApp(string[] args)
    {
        var app = new CommandApp();
    
        app.Configure(config =>
        {
            config.AddBranch<ProjectSettings>("project", project =>
            {
                project.AddCommand<ProjectListCommand>("list");
            });
        });

        return app.Run(args);
    }

    private int RunMenu()
    {
        var controller = new ProjectController();
        
        while (true)
        {
            Console.Clear();
            
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>()
                    .Title("What do you want to do next?")
                    .AddChoices(Enum.GetValues<MenuOptions>()));
            
            if (choice == MenuOptions.Exit)
                return 0;

            if (controller.List() == 1)
                return 1;
            
            AnsiConsole.MarkupLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
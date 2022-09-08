using FolderManager.Src.ViewModels;
using Stylet;
using StyletIoC;

namespace FolderManager
{
    public class Bootstrapper : Bootstrapper<FolderManagerViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            // Configure the IoC container in here
        }

        protected override void Configure()
        {
            // Perform any other configuration before the application starts
        }
    }
}

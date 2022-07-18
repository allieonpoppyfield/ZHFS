using Autofac;
using Microsoft.Extensions.DependencyInjection;
using ZHFS.Database;

namespace ZHFS
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            var builder = new ContainerBuilder();
            builder.RegisterType<AppDbContext>();
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }

    }
}
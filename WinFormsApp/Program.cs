using Implementation;
using Implementation.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace WinFormsApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Configurer le conteneur de services
            var services = new ServiceCollection();

            services.AddTransient<IFileWrapper, FileWrapper>();
            services.AddTransient<IFileByteReader, FileByteReader>();
            services.AddSingleton<IFileLogger, FileLogger>();
            services.AddSingleton<Form1>();

            var serviceProvider = services.BuildServiceProvider();

            // Résoudre le formulaire principal et démarrer l'application
            var form = serviceProvider.GetRequiredService<Form1>();
            Application.Run(form);
        }
    }
}
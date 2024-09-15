using Implementation;
using Implementation.Interfaces;
using Implementation.Utility;
using Implementation.Utility.Interfaces;
using Implementation.Wrapper;
using Implementation.Wrapper.Interfaces;
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
            services.AddTransient<IJsonFileManager, JsonFileManager>();
            services.AddTransient<IImageManager, ImageManager>();
            services.AddTransient<IUsefulFunctions, UsefulFunctions>();

            services.AddTransient<IGraphics>(sp =>
            {
                // Cr�ation d'un Graphics ici, par exemple � partir d'un Bitmap
                var bitmap = new Bitmap(100, 100); // Exemple, ajustez selon vos besoins
                var graphics = Graphics.FromImage(bitmap);
                return new GraphicsWrapper(graphics);
            });

            services.AddSingleton<Form1>();

            var serviceProvider = services.BuildServiceProvider();

            // R�soudre le formulaire principal et d�marrer l'application
            var form = serviceProvider.GetRequiredService<Form1>();
            Application.Run(form);
        }
    }
}
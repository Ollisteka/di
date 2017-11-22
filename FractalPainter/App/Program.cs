using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using FractalPainting.App.Actions;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
using Ninject.Extensions.Factory;

namespace FractalPainting.App
{
    internal static class Program
    {
		private static MainForm CreateMainForm()
		{
			var container = new StandardKernel();

			// Регистрируем все имеющиеся реализации ConsoleCommand:
			container.Bind<IUiAction>().To<SaveImageAction>();
			container.Bind<IUiAction>().To<KochFractalAction>();
			container.Bind<IUiAction>().To<ImageSettingsAction>();
			container.Bind<IUiAction>().To<DragonFractalAction>();
			container.Bind<IUiAction>().To<PaletteSettingsAction>();

			container.Bind<Palette>()
				.ToSelf()
				.InSingletonScope();

			container.Bind<PictureBoxImageHolder, IImageHolder>()
				.To<PictureBoxImageHolder>()
				.InSingletonScope();

			container.Bind<IObjectSerializer>().To<XmlObjectSerializer>()
				.WhenInjectedInto<SettingsManager>();
			container.Bind<IBlobStorage>().To<FileBlobStorage>()
				.WhenInjectedInto<SettingsManager>();

			container.Bind<AppSettings, IImageDirectoryProvider>()
				.ToMethod(ctx => ctx.Kernel.Get<SettingsManager>());

			container.Bind<IDragonPainterFactory>().ToFactory();

			container.Bind<KochPainter>().ToSelf();
			return container.Get<MainForm>();
		}
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
				
				Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(CreateMainForm());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
	}
}
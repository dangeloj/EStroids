namespace EStroids
{
	using System;
	using System.Diagnostics;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Windows.Forms;
	using EStroids.EntitySystem;
	using EStroids.EntitySystem.Systems;

	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			using (var source = new CancellationTokenSource())
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				var form = new Form1();
				var manager = new EntityManager();
				var keyboard = new Keyboard();
				var systems = new SystemBase[] {
					new RenderingSystem(form, manager)
				};

				form.KeyDown += (s, e) => keyboard.KeyDown(e.KeyCode);
				form.KeyUp += (s, e) => keyboard.KeyUp(e.KeyCode);
				form.FormClosing += (s, e) => source.Cancel();
				form.Load += (s, e) => Run(systems, source.Token);

				Application.Run(form);
			}
		}

		private static void Run(SystemBase[] systems, CancellationToken token)
		{
			Task.Factory.StartNew(() =>
			{
				foreach (var system in systems) system.Initialize();

				double t = 0;
				var watch = Stopwatch.StartNew();

				while(token.IsCancellationRequested == false)
				{
					var currentTick = watch.Elapsed.TotalSeconds;
					var frameTime = currentTick - t;
					t = currentTick;

					foreach (var system in systems) system.Frame(frameTime);
				}

				watch.Stop();

				foreach (var system in systems) system.Shutdown();
			}, token);
		}
	}
}
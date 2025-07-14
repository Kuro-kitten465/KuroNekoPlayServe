using Kuro.PlayServe.Menus;

namespace Kuro.PlayServe.Cores
{
    public class Application
    {
        private readonly CancellationTokenSource _cts = new();
        private readonly MenuManager _menuManager= new();

        private Configurations _configurations;

        public static Configurations Configurations { get; private set; }

        public void Run()
        {
            while (!_cts.IsCancellationRequested)
            {
                try
                {
                    _menuManager.OnMenuClosed += SetupConfigurations;
                    _menuManager.Open<ConfigMenu>().Wait();
                    _menuManager.OnMenuClosed -= SetupConfigurations;

                    _menuManager.Open<MainMenu>().Wait();
                }
                catch (Exception ex)
                {
                    WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        private void SetupConfigurations(object obj)
        {
            _configurations = (Configurations)obj;
            Configurations = _configurations;
        }

        public void Stop()
        {
            _cts.Cancel();
        }
    }
}

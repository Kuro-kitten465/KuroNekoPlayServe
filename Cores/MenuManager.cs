using Kuro.PlayServe.Menus;

namespace Kuro.PlayServe.Cores
{
    public class MenuManager
    {
        private readonly List<IMenuService> _menus = [];

        public event Action<object>? OnMenuClosed;

        public MenuManager()
        {
            var configMenu = new ConfigMenu();
            configMenu.OnLoadingCompleted += configs =>
            {
                OnMenuClosed?.Invoke(configs);
            };

            _menus.Add(configMenu);
            _menus.Add(new MainMenu());
            _menus.Add(new GameMenu());
        }

        public async Task Open<TMenu>() where TMenu : IMenuService
        {
            Clear();
            var menu = _menus.FirstOrDefault(m => m.GetType() == typeof(TMenu)) ?? throw new InvalidOperationException($"Menu of type {typeof(TMenu).Name} not found.");
            await menu.OpenMenu();
        }
    }
}

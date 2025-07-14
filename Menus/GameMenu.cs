using Kuro.PlayServe.Cores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuro.PlayServe.Menus
{
    public class GameMenu : IMenuService
    {
        public async Task OpenMenu()
        {
            WriteLine($"{this}");
        }
    }
}

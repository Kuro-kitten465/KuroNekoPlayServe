using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuro.PlayServe.Cores
{
    public class InputSystem
    {
        public event Action? ESCKeyPressed;

        public void StartListening()
        {
            Task.Run(() => ListenForKeyPresses());
        }

        private void ListenForKeyPresses()
        {
            while (true)
            {
                var key = ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    ESCKeyPressed?.Invoke();
                    break;
                }
            }
        }
    }
}

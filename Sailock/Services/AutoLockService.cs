using System;
using System.Windows.Threading;

namespace Sailock.Services
{
    public class AutoLockService
    {
        private readonly DispatcherTimer _timer;
        private Action _onLockout;
        private bool _isEnabled;

        public AutoLockService()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(2)
            };
            _timer.Tick += (s, e) =>
            {
                _timer.Stop();
                _onLockout?.Invoke();
            };
        }

        /// <summary>
        /// Activa el auto-lock. onLockout se llama al expirar el tiempo.
        /// </summary>
        public void Enable(Action onLockout)
        {
            _onLockout = onLockout;
            _isEnabled = true;
            Reset();
        }

        /// <summary>
        /// Desactiva el auto-lock completamente.
        /// </summary>
        public void Disable()
        {
            _isEnabled = false;
            _timer.Stop();
        }

        /// <summary>
        /// Reinicia el contador. Llamar en cada interacción del usuario.
        /// </summary>
        public void Reset()
        {
            if (!_isEnabled) return;
            _timer.Stop();
            _timer.Start();
        }
    }
}
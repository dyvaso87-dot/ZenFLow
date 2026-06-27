using System.Diagnostics;
using System.Windows.Threading;

namespace ZenFlow.Logica
{
    public class MotorEnfoque
    {
        private DispatcherTimer _timer;
        private int _segundosRestantes;
        private List<string> _appsABloquear;
        public bool EnSesion { get; private set; }

        public event Action<int>? TiempoActualizado;
        public event Action? SesionTerminada;

        public MotorEnfoque()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTick;
        }

        public void IniciarSesion(int minutos, List<string> appsABloquear)
        {
            _segundosRestantes = minutos * 60;
            _appsABloquear = appsABloquear;
            EnSesion = true;
            _timer.Start();
        }

        public void Pausar()
        {
            if (_timer.IsEnabled) _timer.Stop();
            else _timer.Start();
        }

        public void Terminar()
        {
            _timer.Stop();
            EnSesion = false;
            DesbloquearApps();
        }

        private void OnTick(object? sender, EventArgs e)
        {
            _segundosRestantes--;
            TiempoActualizado?.Invoke(_segundosRestantes);
            BloquearApps();

            if (_segundosRestantes <= 0)
            {
                Terminar();
                SesionTerminada?.Invoke();
            }
        }

        private void BloquearApps()
        {
            foreach (var app in _appsABloquear)
            {
                var procesos = Process.GetProcessesByName(app);
                foreach (var proceso in procesos)
                {
                    try { proceso.CloseMainWindow(); }
                    catch { }
                }
            }
        }

        private void DesbloquearApps()
        {
            // Las apps se desbloquean solas al dejar de cerrarlas
        }
    }
}
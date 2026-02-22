using Bridge.Implementation;

namespace Bridge.Abstraction
{
    /// <summary>
    /// SoundSystem concrete abstraction
    /// Implements sound system-specific functionality
    /// </summary>
    public class SoundSystem : Device
    {
        private int _bass = 50;
        private int _treble = 50;
        private SoundMode _soundMode = SoundMode.Stereo;

        public SoundSystem(IDeviceImplementation implementation) : base(implementation)
        {
        }

        public override void TurnOn()
        {
            _implementation.PowerOn();
            Console.WriteLine($"Sound System '{Name}' is now ON");
        }

        public override void TurnOff()
        {
            _implementation.PowerOff();
            Console.WriteLine($"Sound System '{Name}' is now OFF");
        }

        public override string GetStatus()
        {
            var baseStatus = _implementation.GetDeviceStatus();
            return $"{baseStatus}\nBass: {_bass}%\nTreble: {_treble}%\nMode: {_soundMode}";
        }

        public override void Operate()
        {
            if (_implementation.IsPoweredOn)
            {
                AdjustBass(10);
                AdjustTreble(-5);
                ChangeSoundMode(SoundMode.Surround);
                Console.WriteLine($"Sound System Operation: Bass {_bass}%, Treble {_treble}%, Mode {_soundMode}");
            }
            else
            {
                Console.WriteLine("Cannot operate Sound System - device is powered off");
            }
        }

        public void AdjustBass(int adjustment)
        {
            _bass = Math.Max(0, Math.Min(100, _bass + adjustment));
            Console.WriteLine($"Bass adjusted to {_bass}%");
        }

        public void AdjustTreble(int adjustment)
        {
            _treble = Math.Max(0, Math.Min(100, _treble + adjustment));
            Console.WriteLine($"Treble adjusted to {_treble}%");
        }

        public void ChangeSoundMode(SoundMode mode)
        {
            _soundMode = mode;
            Console.WriteLine($"Sound mode changed to {_soundMode}");
        }

        public int Bass => _bass;
        public int Treble => _treble;
        public SoundMode SoundMode => _soundMode;
    }

    /// <summary>
    /// Sound mode enumeration
    /// </summary>
    public enum SoundMode
    {
        Stereo,
        Surround,
        Dolby,
        Rock,
        Jazz,
        Classical
    }
}
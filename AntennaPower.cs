using System;

namespace AntennaControl
{
    class AntennaPower
    {
        private AppSettings _settings;
        private GPIO _gpio;
        public AntennaPower()
        {
            _gpio = new GPIO();
            _gpio.Initialize();
            _settings = Helper.JsonHelper("appsettings.json");
        }

        public bool Open()
        {
            // gpio set io
            // check the value from specified GPO
            ushort baseAddress = (ushort)_gpio.SuperIoInw(0x07, 0x62);

            byte value = _gpio.ReadGpioVal(baseAddress, _settings.GPO);

            // case 1, no more action
            if (value == 1)
            {
                return true;
            }

            // case 0, write 1 to the GPO
            if (value == 0)
            {
                _gpio.SetGpioVal(baseAddress, 1, _settings.GPO);
            }

            return true;
        }

        public bool Close()
        {
            // gpio set io
            // check the value from specified GPO
            ushort baseAddress = (ushort)_gpio.SuperIoInw(0x07, 0x62);

            byte value = _gpio.ReadGpioVal(baseAddress, _settings.GPO);

            // case 0, no more action
            if (value == 0)
            {
                return true;
            }

            // case 1, write 1 to the GPO
            if (value == 1)
            {
                _gpio.SetGpioVal(baseAddress, 0, _settings.GPO);
            }
            return true;
        }

        public bool Status()
        {
            ushort baseAddress = (ushort)_gpio.SuperIoInw(0x07, 0x20);
            Console.WriteLine("Chip Name: {0}", Convert.ToString(baseAddress, 16));

            baseAddress = (ushort)_gpio.SuperIoInw(0x07, 0x62);
            Console.WriteLine("SIO Base: {0}", Convert.ToString(baseAddress, 16));
            return true;
        }

        public void Listen()
        {
            // gpio read io
            // check the value from specified GPI in a loop check
            // case 1, set GPO 1
            // case 0, set GPO 0
        }
    }
}
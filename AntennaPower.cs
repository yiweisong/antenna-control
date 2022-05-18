using System;

namespace AntennaControl
{
    class AntennaPower
    {
        private AppSettings _settings = Helper.JsonHelper("appsettings.json");
        private GPIO _gpio;
        public AntennaPower()
        {
            _gpio = new GPIO();
            _gpio.Initialize();
        }

        public bool Open()
        {
            // gpio set io
            // check the value from specified GPO
            byte value = _gpio.ReadGpioVal(_settings.GPO);

            // case 1, no more action
            if (value == 1)
            {
                return true;
            }

            // case 0, write 1 to the GPO
            if (value == 0)
            {
                _gpio.SetGpioVal(_settings.GPO, 1);
            }

            return true;
        }

        public bool Close()
        {
            // gpio set io
            // check the value from specified GPO
            byte value = _gpio.ReadGpioVal(_settings.GPO);

            // case 0, no more action
            if (value == 0)
            {
                return true;
            }

            // case 1, write 1 to the GPO
            if (value == 1)
            {
                _gpio.SetGpioVal(_settings.GPO, 0);
            }
            return true;
        }

        bool Status()
        {
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
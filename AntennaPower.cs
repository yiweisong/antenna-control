using System;

namespace AntennaControl
{
    class AntennaPower
    {
        private AppSettings _settings;
        private GPIO _gpio;
        private ScriptManager _scriptManager;

        public AntennaPower()
        {
            _settings = Helper.JsonHelper("appsettings.json");

            _gpio = new GPIO(_settings);
            _gpio.Initialize();

            _scriptManager = new ScriptManager(_gpio);
        }

        public bool Open()
        {
            string[] openScripts = _settings.actions.open;
            try
            {
                _scriptManager.Run(openScripts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            string[] statusScripts = _settings.actions.status;
            try
            {
                byte status = _scriptManager.Run(statusScripts).ToByte();
                return status == 1;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Close()
        {
            string[] closeScripts = _settings.actions.close;
            try
            {
                _scriptManager.Run(closeScripts);
            }
            catch (Exception ex)
            {
                return false;
            }

            string[] statusScripts = _settings.actions.status;
            try
            {
                byte status = _scriptManager.Run(statusScripts).ToByte();
                return status == 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public byte Status()
        {
            string[] gpioScripts = _settings.actions.status;
            try
            {
                return _scriptManager.Run(gpioScripts).ToByte();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool Info()
        {
            // ushort baseAddress = (ushort)_gpio.SuperIoInw(0x07, 0x20);
            // Console.WriteLine("Chip Name: {0}", Convert.ToString(baseAddress, 16));
            // Console.WriteLine("Base Address: {0}", _settings.baseAddress);
            // Console.WriteLine("GPO Power Address: {0}", _settings.baseAddress + Convert.ToUInt16(_settings.GPO.offset, 16));
            // Console.WriteLine("GPO Power Bit: {0}", _settings.GPO.location);
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
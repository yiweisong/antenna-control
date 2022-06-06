using System;

namespace AntennaControl
{
    class ScriptResult
    {
        private bool _success;
        private string _data;

        public ScriptResult(bool success, string data)
        {
            this._success = success;
            this._data = data;
        }

        public bool Success
        {
            get
            {
                return this._success;
            }
        }

        public string Data
        {
            get
            {
                return this._data;
            }
        }

        public byte ToByte()
        {
            return Convert.ToByte(this._data);
        }

        public bool ToBoolean()
        {
            return Convert.ToBoolean(this._data);
        }
    }

    class CommandManager
    {
        public static ScriptResult Run(GPIO io, string statement)
        {
            string[] tokens = statement.Split('-');

            if (tokens.Length != 3)
            {
                return new ScriptResult(false, String.Empty);
            }

            string ioType = tokens[0];
            string key = tokens[1];
            string action = tokens[2];

            string result = string.Empty;

            switch (action)
            {
                case "on":
                    result = io.Write(ioType, key, 1).ToString();
                    break;
                case "off":
                    result = io.Write(ioType, key, 0).ToString();
                    break;
                case "status":
                    result = io.Read(ioType, key).ToString();
                    break;
                default:
                    result = io.Read(ioType, key).ToString();
                    break;
            }

            return new ScriptResult(true, result);
        }
    }

    class ScriptManager
    {
        private GPIO _gpio;

        public ScriptManager(GPIO gpio)
        {
            this._gpio = gpio;
        }

        public ScriptResult Run(string[] scripts)
        {
            ScriptResult lastResult=null;

            foreach (string statement in scripts)
            {
                lastResult = CommandManager.Run(this._gpio, statement);
                if (!lastResult.Success)
                {
                    throw new Exception("Command run failed");
                }
            }

            if (lastResult!=null)
            {
                throw new Exception("Please configure the script");
            }

            return lastResult;
        }
    }
}
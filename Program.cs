using System;

namespace AntennaControl
{
    class Program
    {
        const string SUCCESS = "success";
        const string FAIL = "fail";

        static void Main(string[] args)
        {
            AntennaPower powerControl = new AntennaPower();

            string action = string.Empty;

            if (args.Length > 0)
            {
                action = args[0];
            }

            if (string.IsNullOrEmpty(action))
            {
                Console.Write(FAIL);
                return;
            }

            if (string.Equals(action, "info"))
            {
                powerControl.Info();
                return;
            }

            if (string.Equals(action, "open"))
            {
                bool result = powerControl.Open();
                Console.Write("{0}-{1}", action, result ? SUCCESS : FAIL);
                return;
            }

            if (string.Equals(action, "close"))
            {
                bool result = powerControl.Close();
                Console.Write("{0}-{1}", action, result ? SUCCESS : FAIL);
                return;
            }

            if (string.Equals(action, "status"))
            {
                // 0: power off
                // 1: power on
                byte result = powerControl.Status();
                Console.Write("{0}-{1}", action, result);
                return;
            }

            Console.Write("No support action:{0}", action);
        }
    }
}

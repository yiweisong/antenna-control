using System;

namespace AntennaControl
{
    class Program
    {
        const string SUCCESS = "success";
        const string FAIL = "fail";

        static void Main(string[] args)
        {
            //AntennaPower powerControl = new AntennaPower();

            string action = string.Empty;

            if (args.Length > 0)
            {
                action = args[0];
            }

            if (string.IsNullOrEmpty(action))
            {
                Console.WriteLine(FAIL);
                return;
            }

            if (string.Equals(action, "open"))
            {
                bool result = true; //powerControl.Open();
                Console.WriteLine("{0}-{1}", action, result ? SUCCESS : FAIL);
                return;
            }

            if (string.Equals(action, "close"))
            {
                bool result = true; //powerControl.Close();
                Console.WriteLine("{0}-{1}", action, result ? SUCCESS : FAIL);
                return;
            }

            Console.WriteLine("No support action:{0}", action);
        }
    }
}

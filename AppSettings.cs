using System;
using System.IO;
//using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AntennaControl
{
    public class GPIOOptions
    {
        public string offset { get; set; }
        public int location { get; set; }
    }

    public class AppSettings
    {
        public ushort baseAddress { get; set; }
        public GPIOOptions GPO { get; set; }
        public GPIOOptions GPI { get; set; }
    }

    public class Helper
    {
        public static AppSettings JsonHelper(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
            {
                var pvFile = File.Create(jsonFilePath);
                pvFile.Flush();
                pvFile.Dispose();
                return null;
            }

            using (var stream = new FileStream(jsonFilePath, FileMode.OpenOrCreate))
            {
                try
                {
                    StreamReader sr = new StreamReader(stream);
                    JsonSerializer serializer = new JsonSerializer
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        Converters = { new JavaScriptDateTimeConverter() }
                    };
                    //构建Json.net的读取流  
                    using (var reader = new JsonTextReader(sr))
                    {
                        return serializer.Deserialize<AppSettings>(reader);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Convert error" + ex.Message);
                    return null;
                }
            }
        }
    }

}

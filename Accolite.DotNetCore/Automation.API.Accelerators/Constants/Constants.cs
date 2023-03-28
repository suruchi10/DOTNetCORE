using System.IO;

namespace Automation.API.Accelerators.Constants
{
    public static class Constants
    {      
        public const string Name = "Name";
        public const string EndPointDetails = "EndpointDetails";     
        public const string MiddleName = "MiddleName";
        public const string LastName = "LastName";
        public const string URL = "http://eaapp.somee.com/";
        public static string Path = Directory.GetCurrentDirectory();
        public static string URI = "http://localhost:3000/";
        public static string POST = "posts/";
        public static string NewPath = Path.Substring(0, Path.Length - 24) + "\\" + "Test Results" + "\\";
    }
}
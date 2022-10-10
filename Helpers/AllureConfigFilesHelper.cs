using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    public class AllureConfigFilesHelper
    {
        public static string CreateBatFile()
        {
            string path = Browser.RootPathReport() + "allure serve.bat";
            string allureResultsDirectory = Browser.RootPathReport() + "allure-results";
            string allureResults = "allure serve " + allureResultsDirectory;
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists == true)
            {
                fileInf.Delete();
            }
            if (!Directory.Exists(allureResultsDirectory) == true)
            {
                Directory.CreateDirectory(allureResultsDirectory);
            }
            using (FileStream fstream = new FileStream($"{path}", FileMode.OpenOrCreate))
            {
                byte[] array = Encoding.Default.GetBytes(allureResults);
                fstream.Write(array, 0, array.Length);
            }
            return path;
        }

        public static void RemoveBatFile()
        {
            string path = Browser.RootPathReport() + "allure serve.bat";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists == true)
            {
                fileInf.Delete();
            }
        }

        public static string CopyJsonConfigFile()
        {

            FileInfo fileInf = new FileInfo(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + "allureConfig.json");
            if (fileInf.Exists == true)
            {
                fileInf.Delete();
            }
            string mainpath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + "allureConfig.json";
            string str = Path.Combine(Browser.RootPath() + "allure-results");
            string path = str.Replace("\\", "\\\\");

            string body = string.Format("{{" +
                            "\"allure\"" + ":" + "{{" +
                                                "\"directory\":" + "\"" + $"{path}" + "\"" + "," +
                                                "\"links\":" + "[" +
                                                                    "\"" + "{{" + "link" + "}}" + "\"" + "," +
                                                                    "\"https://testrail.com/browse/" + "{{" + "tms" + "}}" + "\"" + "," +
                                                                    "\"https://jira.com/browse/" + "{{" + "issue" + "}}" + "\"" +
                                                               "]" +
                                                    "}}" + "," +
                            "\"specflow\":" + "{{" +
                                                    "\"stepArguments\":" + "{{" +
                                                                                "\"convertToParameters\":" + "\"true\"" + "," +
                                                                                "\"paramNameRegex\":" + "\"\"" + "," +
                                                                                "\"paramValueRegex\":" + "\"\"" +
                                                                                "}}" + "," +
                                                    "\"grouping\":" + "{{" +
                                                                            "\"suites\":" + "{{" +
                                                                                                    "\"parentSuite\":" + "\"^parentSuite:?(.+)\"" + "," +
                                                                                                    "\"suite\":" + "\"^suite:?(.+)\"" + "," +
                                                                                                    "\"subSuite\":" + "\"^subSuite:?(.+)\"" +
                                                                                                    "}}" + "," +
                                                                            "\"behaviors\":" + "{{" +
                                                                                                        "\"epic\":" + "\"^epic:?(.+)\"" + "," +
                                                                                                        "\"story\":" + "\"^story:(.+)\"" +
                                                                                                    "}}" +
                                                                            "}}" + "," +
                                                                            "\"labels\":" + "{{" +
                                                                                                    "\"owner\":" + "\"^author:?(.+)\"" + "," +
                                                                                                    "\"severity\":" + "\"^(normal|blocker|critical|minor|trivial)\"" +
                                                                                                    "}}" + "," +
                                                                            "\"links\":" + "{{" +
                                                                                                "\"tms\":" + "\"^story:(.+)\"" + "," +
                                                                                                "\"issue\":" + "\"^issue:(.+)\"" + "," +
                                                                                                "\"link\":" + "\"^link:(.+)\"" +
                                                                                            "}}" +
                                                                        "}}" +
                                             "}}");

            string jsonBody = body.Replace("[[", "[").Replace("]]", "]");
            File.WriteAllText(mainpath, jsonBody);
            return mainpath;
        }
    }

    public class ForceCloseDriver
    {
        public static string CreateBatFile()
        {
            string path = Browser.RootPathReport() + "_!CloseOpenWith.bat";
            string forceCloseAppList = string.Format(
                "TASKKILL" + " /IM " + "\"OpenWith.exe\"" + " /F " + "\n" +
                "TASKKILL" + " /IM " + "\"chromedriver.exe\"" + " /F " + "\n" +
                "TASKKILL" + " /IM " + "\"java.exe\"" + " /F " + "\n" +
                "TASKKILL" + " /IM " + "\"node.exe\"" + " /F " + "\n" +
                "TASKKILL" + " /IM " + "\"AppleMobileDeviceService.exe\"" + " /F " + "\n" +
                "TASKKILL" + " /IM " + "\"APSDaemon.exe\"" + " /F " + "\n" +
                "TASKKILL" + " /IM " + "\"ICloudServices.exe\"" + " /F " + "\n" +
                "TASKKILL" + " /IM " + "\"mDNSResponder.exe\"" + " /F " + "F" + "\n" +
                "TASKKILL" + " /IM " + "\"altserver.exe\"" + " /F " + "\n" +
                "TASKKILL" + " /IM " + "\"Screencast-O-Matic.exe\"" + " /F " + "\n"
                );
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists == true)
            {
                fileInf.Delete();
            }
            using (FileStream fstream = new FileStream($"{path}", FileMode.OpenOrCreate))
            {
                byte[] array = Encoding.Default.GetBytes(forceCloseAppList);
                fstream.Write(array, 0, array.Length);
            }
            return path;
        }
    }
}

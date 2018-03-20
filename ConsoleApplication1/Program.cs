using CloudFlareUtilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static async Task MainAsync()
        {
            var handler = new ClearanceHandler
            {
                MaxRetries = 1
            };
            var cookies = new CookieContainer();
            //            cookies.SetCookies(new Uri("https://www.xspeeds.eu/"), "__cfduid=d71dfc6228c26ebde95f0b379e9253ffe1487271678");
            // cookies.SetCookies(new Uri("https://www.xspeeds.eu/"), "cf_clearance=2df1f62ba9d0e50458dea4cc527e974efa07ad4a-1487322311-3600");
            var cUri = new Uri("//www.xspeeds.eu");
            cookies.SetCookies(cUri, "c_secure_uid=306823");
            cookies.SetCookies(cUri, "c_secure_pass=8f59d7effc354e497f11fd5d83cdec6f");
            cookies.SetCookies(cUri, "acqu=Mg%3D%3D");
            cookies.SetCookies(cUri, "__cfduid=d9fc947a687eab43d25235a490c5cc8d31487682251");
            cookies.SetCookies(cUri, "cf_clearance=e9f60983df2ba46dda980fe9ac86a4af93bbe1fe-1487682256-3600");
            HttpClientHandler clientHandlr = new HttpClientHandler
                {
                    CookieContainer = cookies,
                    AllowAutoRedirect = false, // Do not use this - Bugs ahoy! Lost cookies and more.
                    UseCookies = true,
                    Proxy = null,
                    UseProxy = false,
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };

            handler.InnerHandler = clientHandlr;

            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Ubuntu Chrome/47.0.2526.73 Safari/537.36");
            Console.WriteLine("b");
            var content = await client.GetStringAsync("https://www.xspeeds.eu/browse.php");
            Console.WriteLine(content);
        }

        static void Main(string[] args)
        {
            TextWriterTraceListener writer = new TextWriterTraceListener(System.Console.Out);
            Debug.Listeners.Add(writer);

            MainAsync().Wait();
        }
    }
}

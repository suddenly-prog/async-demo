using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ConsoleApp
{
    class Models
    {
        public void APMDemonstration()
        {
            WebRequest request = WebRequest.Create("http://apress.com");
            request.BeginGetResponse(Callback, request);
        }

        private void Callback(IAsyncResult iar)
        {
            WebRequest req = (WebRequest)iar.AsyncState;
            try
            {
                WebResponse resp = req.EndGetResponse(iar);

                long result = resp.ContentLength;

                //ProcessResponse(resp);
            }
            catch (Exception x)
            {
                //LogError(x);
            }
        }

        public void APMDemonstration2()
        {
            WebRequest request = WebRequest.Create("http://apress.com");
            request.BeginGetResponse(iar =>
            {
                WebRequest req = (WebRequest)iar.AsyncState;
                try
                {
                    WebResponse resp = req.EndGetResponse(iar);

                    long result = resp.ContentLength;

                    //ProcessResponse(resp);
                }
                catch (Exception ex)
                {
                    //LogError(ex);
                }
            }, request);
        }



        public void EAPDemonstration()
        {
            WebClient client = new WebClient();

            client.DownloadStringCompleted += OnDownloadDataCompleted;
            client.DownloadStringAsync(new Uri("http://apress.com"));
        }

        private void OnDownloadDataCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                string resp = e.Result;

                long result = resp.Length;

                //ProcessResponse(resp);
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }
        }

        public void EAPDemonstration2()
        {
            WebClient client = new WebClient();

            client.DownloadStringCompleted += (sender, e) =>
            {
                try
                {
                    string resp = e.Result;

                    long result = resp.Length;

                    //ProcessResponse(resp);
                }
                catch (Exception x)
                {
                    //LogError(x);
                }
            };
            client.DownloadStringAsync(new Uri("http://apress.com"));
        }
    }
}

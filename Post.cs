using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Api_Caller
{
    class Post
    {
        private string Fct = String.Empty;
        private string Password = String.Empty;
        private string Url = String.Empty;

        public Post(string in_Api_Url, string in_password, string in_function)
        {
            try
            {
                Url = in_Api_Url;
                Password = in_password;
                Fct = in_function;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public String Send(Dictionary<string, string> in_Params)
        {
            try
            {
                String Response = String.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

                String datas = String.Empty;
                Int32 cpt = 0;
                in_Params.Add("password", Password);
                in_Params.Add("function", Fct);
                foreach (var kv in in_Params)
                {
                    if (cpt == 0)
                    {
                        datas += String.Format("{0}={1}", kv.Key, kv.Value);
                    }
                    else
                    {
                        datas += String.Format("&{0}={1}", kv.Key, kv.Value);
                    }

                    cpt++;
                }

                byte[] data = Encoding.UTF8.GetBytes(datas);

                request.Method = "POST";
                request.Referer = "Api Wrapper Morrigan";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.Credentials = CredentialCache.DefaultCredentials;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var web_response = (HttpWebResponse)request.GetResponse();

                Response = new StreamReader(web_response.GetResponseStream(), Encoding.UTF8).ReadToEnd();

                return Response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

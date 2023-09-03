﻿namespace SIS.HTTP
{
    using System.Net;
    using System.Text;
    public class HttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.Headers = new List<Header> { };
            this.Cookies = new List<Cookie> { };
            this.FormData = new Dictionary<string, string>() { };

            var lines = requestString.Split(new string[] { HttpConstants.NewLine },
                StringSplitOptions.None);
            var headerLine = lines[0];
            var headerLineParts = headerLine.Split(' ');

            this.Method = (HttpMethod)Enum.Parse(typeof(HttpMethod), headerLineParts[0], true);
            this.Path = headerLineParts[1];

            int lineIndex = 1;
            bool isInHeaders = true;
            StringBuilder bodyBuilder = new StringBuilder();
            while (lineIndex < lines.Length)
            {
                var line = lines[lineIndex];
                lineIndex++;
                if (string.IsNullOrWhiteSpace(line))
                {
                    isInHeaders = false;
                }
                if (isInHeaders)
                {
                    this.Headers.Add(new Header(line));
                }
                else
                {
                    bodyBuilder.AppendLine(line);

                }
            }
            if (this.Headers.Any(x => x.Name == HttpConstants.RequestCookieHeader))
            {
                var cookiesAsString = this.Headers.FirstOrDefault(x => x.Name == HttpConstants.RequestCookieHeader).Value;
                var cookies = cookiesAsString.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var cookieAsString in cookies)
                {
                    this.Cookies.Add(new Cookie(cookieAsString));
                }
            }
            this.Body = bodyBuilder.ToString().Replace(Environment.NewLine, "");
            var parameters = this.Body.Split('&');
            foreach (var parameter in parameters)
            {
                if (!string.IsNullOrEmpty(parameter))
                {
                    var parameterParts = parameter.Split('=');
                    var name = parameterParts[0];
                    var value = WebUtility.UrlDecode(parameterParts[1]);
                    if (!this.FormData.ContainsKey(name))
                    {
                        this.FormData.Add(name, value);
                    }
                }

            }
        }
        public string Path { get; set; }
        public HttpMethod Method { get; set; }
        public ICollection<Header> Headers { get; set; }
        public ICollection<Cookie> Cookies { get; set; }
        public IDictionary<string, string> FormData { get; set; }
        public string Body { get; set; }

    }
}

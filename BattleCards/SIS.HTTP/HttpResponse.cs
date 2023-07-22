using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SIS.HTTP
{
    public class HttpResponse
    {
        
        
        public HttpResponse(string contentType, byte[] body, HttpStatus statusCode = HttpStatus.OK)
        {
            if (body == null)
            {
                throw new ArgumentException(nameof(body));
            }
            this.StatusCode = statusCode;
            this.Body = body;
            this.Headers = new List<Header>
            {
                {new Header("Content-Type",contentType)},
                {new Header("Content-Length", body.Length.ToString())}
            };
            this.Cookies = new List<Cookie>();
        }
        public override string ToString()
        {
            StringBuilder sb= new StringBuilder();
            sb.Append($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}" + HttpConstants.NewLine);
            foreach (var header in this.Headers)
            {
                sb.Append(header.ToString() + HttpConstants.NewLine);
            }
            foreach (var cookie in this.Cookies)
            {
                sb.Append("Set-Cookie: " + cookie.ToString() + HttpConstants.NewLine);
            }
            sb.Append(HttpConstants.NewLine);
            return sb.ToString();
        }
        public HttpStatus StatusCode { get; set; }
        public ICollection<Header> Headers { get; set; }
        public ICollection<Cookie> Cookies { get; set; }
        public byte[] Body { get; set; }

    }
}

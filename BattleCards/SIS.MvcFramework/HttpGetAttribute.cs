namespace SIS.MvcFramework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class HttpGetAttribute : BaseHttpAttribute
    {
        public HttpGetAttribute()
        {

        }
        public HttpGetAttribute(string url)
        {
            this.Url = url;
        }
        public override HTTP.HttpMethod Method => HTTP.HttpMethod.Get;
    }
}

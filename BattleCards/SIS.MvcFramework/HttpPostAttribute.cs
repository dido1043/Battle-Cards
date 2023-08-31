using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.MvcFramework
{
    public class HttpPostAttribute : BaseHttpAttribute
    {
        public HttpPostAttribute()
        { }
        public HttpPostAttribute(string url)
        {
            this.Url = url;
        }
        public override HTTP.HttpMethod Method => HTTP.HttpMethod.Post;
    }
}

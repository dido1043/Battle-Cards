namespace SIS.MvcFramework
{
    using SIS.HTTP;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class BaseHttpAttribute : Attribute
    {
        public string Url { get; set; }

        public abstract SIS.HTTP.HttpMethod Method { get; }
    }
}

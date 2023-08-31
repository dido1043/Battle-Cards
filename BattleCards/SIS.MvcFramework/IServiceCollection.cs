namespace SIS.MvcFramework
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IServiceCollection
    {
        void Add<TSource, TDestination>()
            where TDestination : TSource;

        object CreateInstance(Type type);

        T CreateInstance<T>();
    }
}

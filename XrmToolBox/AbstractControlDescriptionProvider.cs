using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace XrmToolBox
{
    public class AbstractControlDescriptionProvider<TAbstract, TBase> : TypeDescriptionProvider
    {
        public AbstractControlDescriptionProvider()
            : base(TypeDescriptor.GetProvider(typeof(TAbstract)))
        {
        }

        public override Type GetReflectionType(Type objectType, object instance)
        {
            if (objectType.FullName == typeof(TAbstract).FullName)
                return typeof(TBase);

            return base.GetReflectionType(objectType, instance);
        }

        public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
        {
            if (objectType.FullName == typeof(TAbstract).FullName)
                objectType = typeof(TBase);

            return base.CreateInstance(provider, objectType, argTypes, args);
        }
    }
}

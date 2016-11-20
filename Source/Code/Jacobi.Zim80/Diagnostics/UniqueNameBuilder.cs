using System;

namespace Jacobi.Zim80.Diagnostics
{
    internal class UniqueNameBuilder
    {
        private int _counter;

        private int NextId()
        {
            _counter++;
            return _counter;
        }

        public string NewId<T>(string name = null)
        {
            var type = typeof(T);

            if (String.IsNullOrEmpty(name))
            {
                return String.Format("{0}_{1}", TypeName(type), NextId());
            }

            return String.Format("{1} ({0})", TypeName(type), name);
        }

        public string TypeName<T>()
        {
            return TypeName(typeof(T));
        }

        public string TypeName(Type type)
        {
            if (type.ContainsGenericParameters)
            {
                var genArgs = type.GetGenericArguments();
                return type.MakeGenericType(genArgs).Name;
            }

            return type.Name;
        }
    }
}

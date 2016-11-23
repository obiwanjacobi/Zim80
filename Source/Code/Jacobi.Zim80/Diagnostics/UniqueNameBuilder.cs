using System;
using System.Linq;
using System.Text;

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

        public string NewId<T>(string name = null, string ownerName = null, string netName = null)
        {
            var builder = new StringBuilder();
            var type = typeof(T);

            if (!String.IsNullOrEmpty(ownerName))
            {
                builder.Append(ownerName);
                builder.Append(".");
            }

            if (!String.IsNullOrEmpty(name))
            {
                builder.Append(name);
            }
            else
            {
                builder.Append(TypeName(type));
                builder.Append("_");
                builder.Append(NextId());
            }

            if (!String.IsNullOrEmpty(netName))
            {
                builder.Append("=>");
                builder.Append(netName);
            }

            builder.AppendFormat(" ({0})", TypeName(type));
            return builder.ToString();
        }

        public string TypeName<T>()
        {
            return TypeName(typeof(T));
        }

        public string TypeName(Type type)
        {
            if (type.IsGenericType)
            {
                var genArgs = type.GetGenericArguments();
                return String.Format("{0}<{1}>", 
                    type.Name.Split('`')[0], 
                    String.Join(", ", genArgs.Select(a => a.Name)));
            }

            return type.Name;
        }
    }
}

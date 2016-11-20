using Jacobi.Zim80.Diagnostics.DgmlModel;
using System.IO;
using System.Xml.Serialization;

namespace Jacobi.Zim80.Diagnostics
{
    internal static class DgmlSerializer
    {
        public const string Namespace = "http://schemas.microsoft.com/vs/2009/dgml";

        private static readonly XmlSerializer _serializer = new XmlSerializer(typeof(DirectedGraph), Namespace);

        public static void Serialize(Stream targetStream, DirectedGraph graph)
        {
            _serializer.Serialize(targetStream, graph);
        }
    }
}

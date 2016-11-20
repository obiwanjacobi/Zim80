using Jacobi.Zim80.Diagnostics;
using Jacobi.Zim80.Model;
using System.IO;

namespace Jacobi.Zim80.UnitTests
{
    internal static class TestExtensions
    {
        public static string Save(this SimulationModel model)
        {
            GraphicModelBuilder builder = new GraphicModelBuilder();

            if (model.Cpu != null)
                builder.Add(model.Cpu);

            if (model.Memory != null)
                builder.Add(model.Memory);

            if (model.IoSpace != null)
                builder.Add(model.IoSpace);

            var filePath = Path.GetTempFileName();
            filePath = Path.Combine(
                Path.GetDirectoryName(filePath), 
                Path.GetFileNameWithoutExtension(filePath) + ".dgml");
            using (var stream = File.Create(filePath))
            {
                builder.Save(stream);
            }

            return filePath;
        }
    }
}

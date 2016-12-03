using Jacobi.Zim80.Logic;

namespace Jacobi.Zim80.Diagnostics
{
    public static class LogicAnalyzerExtensions
    {
        // Paste output into http://wavedrom.com/editor.html
        public static string ToWaveJson(this LogicAnalyzer analyzer)
        {
            if (analyzer == null) return string.Empty;

            var builder = new WaveDromBuilder();

            builder.AddRange(analyzer.SignalStreams);
            builder.AddRange(analyzer.BusStreams);
            return builder.ToString();
        }
    }
}

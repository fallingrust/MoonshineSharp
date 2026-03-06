
using MoonshineSharp;
using System.Runtime.InteropServices;
using System.Text;
using static MoonshineSharp.MoonshineSharp;
namespace Moonshine.Autogen
{
    internal unsafe class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var version = moonshine_get_version();
            Console.WriteLine($"Moonshine version: {version}");
        }
    }
}

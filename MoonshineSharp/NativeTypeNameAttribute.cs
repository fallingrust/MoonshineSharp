using System.Diagnostics;

namespace MoonshineSharp
{
    [Conditional("DEBUG")]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
    internal sealed class NativeTypeNameAttribute : Attribute
    {
        public string Name { get; }
     
        public NativeTypeNameAttribute(string name)
        {
            Name = name;
        }
    }
}

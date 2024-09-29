using System.Reflection;
using MapItWire.Net.Utils;
using Newtonsoft.Json;
using Xunit.Sdk;

namespace MapItWire.Net.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class MapItWireRequestDataAttribute(
    string requestIdentifier) : DataAttribute
{
    public override object?[][] GetData(
        MethodInfo testMethod)
    {
        ArgumentNullException.ThrowIfNull(testMethod);
        ParameterInfo[] parameterInfos = testMethod.GetParameters();
        if (parameterInfos.Length != 1)
        {
            throw new InvalidOperationException(
                "Test method must have exactly one parameter when using MapItWireRequestDataAttribute");
        }

        string fileData = RequestUtils.GetRequest(requestIdentifier);
        object? jsonData = JsonConvert.DeserializeObject(
            fileData,
            parameterInfos[0].ParameterType);

        return [[jsonData]];
    }
}
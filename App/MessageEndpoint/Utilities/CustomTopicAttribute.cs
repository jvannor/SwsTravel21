using Dapr;
using System;

namespace MessageEndpoint.Utilities
{
    // https://github.com/dapr/dotnet-sdk/blob/master/examples/AspNetCore/ControllerSample/CustomTopicAttribute.cs

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CustomTopicAttribute : Attribute, ITopicMetadata
    {
        public CustomTopicAttribute(string componentName, string topicName)
        {
            this.PubsubName = Environment.ExpandEnvironmentVariables(componentName);
            this.Name = Environment.ExpandEnvironmentVariables(topicName);
        }

        public new string Match { get; } = string.Empty;

        public string Name { get; } = String.Empty;

        public int Priority { get; } = 0;

        public string PubsubName { get; } = String.Empty;
    }
}

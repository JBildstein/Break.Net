using System;
using Xunit.Abstractions;

namespace BreakDotNet.UnitTests
{
    public class LabeledData<T> : IXunitSerializable
    {
        public string Label
        {
            get;
            private set;
        }
        public T Data
        {
            get;
            private set;
        }

        public LabeledData()
        {
            Label = string.Empty;
        }

        public LabeledData(string label, T data)
        {
            Label = label ?? throw new ArgumentNullException(nameof(label));
            Data = data;
        }

        public override string ToString()
        {
            return Label;
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
            Label = info.GetValue<string>(nameof(Label));
            Data = info.GetValue<T>(nameof(Data));
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(Label), Label, typeof(string));
            info.AddValue(nameof(Data), Data, typeof(T));
        }
    }
}

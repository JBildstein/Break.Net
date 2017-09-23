using System;
using Xunit.Abstractions;

namespace BreakDotNet.UnitTests
{
    public class ChangeModelData : IXunitSerializable
    {
        public Type OldModel
        {
            get;
            private set;
        }
        public Type NewModel
        {
            get;
            private set;
        }
        public string ExpectedChangeId
        {
            get;
            private set;
        }
        public int ExpectedChangeCount
        {
            get;
            private set;
        }

        public ChangeModelData()
        { }

        public ChangeModelData(Type oldModel, Type newModel, string expectedChangeId, int expectedChangeCount = 1)
        {
            OldModel = oldModel;
            NewModel = newModel;
            ExpectedChangeId = expectedChangeId;
            ExpectedChangeCount = expectedChangeCount;
        }

        public static object[] WithLabel(string label, Type oldModel, Type newModel, string expectedChangeId, int expectedChangeCount = 1)
        {
            return new object[] { new LabeledData<ChangeModelData>(label, new ChangeModelData(oldModel, newModel, expectedChangeId, expectedChangeCount)) };
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
            OldModel = info.GetValue<Type>(nameof(OldModel));
            NewModel = info.GetValue<Type>(nameof(NewModel));
            ExpectedChangeId = info.GetValue<string>(nameof(ExpectedChangeId));
            ExpectedChangeCount = info.GetValue<int>(nameof(ExpectedChangeCount));
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(OldModel), OldModel, typeof(Type));
            info.AddValue(nameof(NewModel), NewModel, typeof(Type));
            info.AddValue(nameof(ExpectedChangeId), ExpectedChangeId, typeof(string));
            info.AddValue(nameof(ExpectedChangeCount), ExpectedChangeCount, typeof(int));
        }
    }
}

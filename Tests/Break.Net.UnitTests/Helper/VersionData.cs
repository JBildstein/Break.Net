using System;
using BreakDotNet.Changes;
using Xunit.Abstractions;

namespace BreakDotNet.UnitTests
{
    public class VersionData : IXunitSerializable
    {
        public Version OldVersion
        {
            get;
            private set;
        }
        public Version ExpectedVersion
        {
            get;
            private set;
        }
        public IChange Change
        {
            get;
            private set;
        }

        public VersionData()
        { }

        public VersionData(Version oldVersion, Version expectedVersion, ChangeSeverity severity)
        {
            OldVersion = oldVersion;
            ExpectedVersion = expectedVersion;
            Change = GetChange(severity);
        }

        private static IChange GetChange(ChangeSeverity severity)
        {
            switch (severity)
            {
                case ChangeSeverity.Major:
                    return new MajorChange();
                case ChangeSeverity.Minor:
                    return new MajorChange();
                case ChangeSeverity.Patch:
                    return new MajorChange();

                default:
                    throw new InvalidOperationException($"Unknown change severity of {severity}");
            }
        }

        public static object[] WithLabel(string label, Version oldVersion, Version expectedVersion, ChangeSeverity change)
        {
            return new object[] { new LabeledData<VersionData>(label, new VersionData(oldVersion, expectedVersion, change)) };
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
            OldVersion = info.GetValue<Version>(nameof(OldVersion));
            ExpectedVersion = info.GetValue<Version>(nameof(ExpectedVersion));
            Change = GetChange(info.GetValue<ChangeSeverity>(nameof(Change)));
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(OldVersion), OldVersion, typeof(Version));
            info.AddValue(nameof(ExpectedVersion), ExpectedVersion, typeof(Version));
            info.AddValue(nameof(Change), Change.Severity, typeof(ChangeSeverity));
        }
    }
}

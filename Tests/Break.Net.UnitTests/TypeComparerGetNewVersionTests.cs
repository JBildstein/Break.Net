using System;
using System.Collections.Generic;
using BreakDotNet.Changes;
using Xunit;

namespace BreakDotNet.UnitTests
{
    public class TypeComparerGetNewVersionTests : TypeComparerTestsBase
    {
        [Theory(DisplayName = "GetNewVersion Increment")]
        [MemberData(nameof(Data))]
        public void GetNewVersion_ProvideChanges_IncrementsVersion(LabeledData<VersionData> d)
        {
            var changeList = new List<IChange> { d.Data.Change };

            Version newVersion = TypeComparer.GetNewVersion(d.Data.OldVersion, changeList);

            Assert.Equal(d.Data.ExpectedVersion, newVersion);
        }

        [Fact(DisplayName = "GetNewVersion Invalid Change throws Exception")]
        public void GetNewVersion_InvalidChange_ThrowsException()
        {
            var changeList = new List<IChange> { new InvalidChange() };

            Assert.Throws<InvalidOperationException>(() => TypeComparer.GetNewVersion(new Version(), changeList));
        }

        public static object[][] Data =
        {
            VersionData.WithLabel("Major", new Version(1, 1, 1), new Version(2, 0, 0), ChangeSeverity.Major),
            VersionData.WithLabel("Minor", new Version(1, 1, 1), new Version(1, 2, 0), ChangeSeverity.Minor),
            VersionData.WithLabel("Patch", new Version(1, 1, 1), new Version(1, 1, 2), ChangeSeverity.Patch),
        };
    }
}

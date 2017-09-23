using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace BreakDotNet.UnitTests
{
    public class TypeComparerCompareTests : TypeComparerTestsBase
    {
        [Theory(DisplayName = "Compare Types")]
        [MemberData(nameof(ConstructorChangeData.Data), MemberType = typeof(ConstructorChangeData))]
        [MemberData(nameof(EventChangeData.Data), MemberType = typeof(EventChangeData))]
        [MemberData(nameof(FieldChangeData.Data), MemberType = typeof(FieldChangeData))]
        [MemberData(nameof(MethodChangeData.Data), MemberType = typeof(MethodChangeData))]
        [MemberData(nameof(PropertyChangeData.Data), MemberType = typeof(PropertyChangeData))]
        [MemberData(nameof(TypeChangeData.Data), MemberType = typeof(TypeChangeData))]
        public void Compare_ChangedTypeModels_ReturnsChangeInfo(LabeledData<ChangeModelData> d)
        {
            TypeComparer comparer = CreateComparer();

            IEnumerable<IChange> changes = CompareTypes(comparer, d.Data.OldModel, d.Data.NewModel);

            Assert.Equal(d.Data.ExpectedChangeCount, changes.Count());
            Assert.Single(changes, t => t.Id == d.Data.ExpectedChangeId);
        }

        private IEnumerable<IChange> CompareTypes(TypeComparer comparer, Type oldType, Type newType)
        {
            IEnumerable<TypeInfo> oldEnumerable = oldType == null ? Enumerable.Empty<TypeInfo>() : new TypeInfo[] { oldType.GetTypeInfo() };
            IEnumerable<TypeInfo> newEnumerable = newType == null ? Enumerable.Empty<TypeInfo>() : new TypeInfo[] { newType.GetTypeInfo() };

            return comparer.Compare(oldEnumerable, newEnumerable);
        }
    }
}

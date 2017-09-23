using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace BreakDotNet.UnitTests
{
    public class TypeComparerArgumentTests : TypeComparerTestsBase
    {
        [Fact(DisplayName = "Constructor with settings null throws Exception")]
        public void Constructor_SettingsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new TypeComparer(null));
        }

        [Fact(DisplayName = "Compare with Assembly null throws Exception")]
        public void Compare_AssemblyNull_ThrowsException()
        {
            TypeComparer comparer = CreateComparer();
            var assembly = Assembly.GetExecutingAssembly();

            Assert.Throws<ArgumentNullException>(() => comparer.Compare(null, assembly));
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(assembly, null));
        }

        [Fact(DisplayName = "Compare with Type List null throws Exception")]
        public void Compare_TypeListNull_ThrowsException()
        {
            TypeComparer comparer = CreateComparer();
            var list = new List<Type>();

            Assert.Throws<ArgumentNullException>(() => comparer.Compare(null, list));
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(list, null));
        }

        [Fact(DisplayName = "Compare with TypeInfo List null throws Exception")]
        public void Compare_TypeInfoListNull_ThrowsException()
        {
            TypeComparer comparer = CreateComparer();
            var list = new List<TypeInfo>();

            Assert.Throws<ArgumentNullException>(() => comparer.Compare(null, list));
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(list, null));
        }

        [Fact(DisplayName = "Compare with Type List with null throws Exception")]
        public void Compare_TypeListWithNull_ThrowsException()
        {
            TypeComparer comparer = CreateComparer();
            var listGood = new List<Type>();
            var listBad = new List<Type>() { null };

            Assert.Throws<ArgumentNullException>(() => comparer.Compare(listGood, listBad));
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(listBad, listGood));
        }

        [Fact(DisplayName = "Compare with TypeInfo List with null throws Exception")]
        public void Compare_TypeInfoListWithNull_ThrowsException()
        {
            TypeComparer comparer = CreateComparer();
            var listGood = new List<TypeInfo>();
            var listBad = new List<TypeInfo>() { null };

            Assert.Throws<ArgumentNullException>(() => comparer.Compare(listGood, listBad));
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(listBad, listGood));
        }

        [Fact(DisplayName = "GetNewVersion with Assembly null throws Exception")]
        public void GetNewVersion_AssemblyNull_ThrowsException()
        {
            var changes = new List<IChange>();

            Assert.Throws<ArgumentNullException>(() => TypeComparer.GetNewVersion((Assembly)null, changes));
        }

        [Fact(DisplayName = "GetNewVersion with Version null throws Exception")]
        public void GetNewVersion_VersionNull_ThrowsException()
        {
            var changes = new List<IChange>();

            Assert.Throws<ArgumentNullException>(() => TypeComparer.GetNewVersion((Version)null, changes));
        }

        [Fact(DisplayName = "GetNewVersion with Change List null throws Exception")]
        public void GetNewVersion_ChangeListNull_ThrowsException()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var version = new Version();

            Assert.Throws<ArgumentNullException>(() => TypeComparer.GetNewVersion(assembly, null));
            Assert.Throws<ArgumentNullException>(() => TypeComparer.GetNewVersion(version, null));
        }
    }
}

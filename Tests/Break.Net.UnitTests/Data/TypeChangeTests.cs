extern alias NewModels;
extern alias OldModels;
using BreakDotNet.Changes;
using New = NewModels::BreakDotNet.UnitTests.Models.Types;
using Old = OldModels::BreakDotNet.UnitTests.Models.Types;

namespace BreakDotNet.UnitTests
{
    public class TypeChangeData
    {
        public static object[][] Data =
        {
            ChangeModelData.WithLabel("Type Abstract", typeof(Old.AbstractChangeModel), typeof(New.AbstractChangeModel), TypeAbstractChange.IdConstant),
            ChangeModelData.WithLabel("Type Add", null, typeof(New.AddChangeModel), TypeAddChange.IdConstant),
            ChangeModelData.WithLabel("Type Concrete", typeof(Old.ConcreteChangeModel), typeof(New.ConcreteChangeModel), TypeConcreteChange.IdConstant),
            ChangeModelData.WithLabel("Type Sealed", typeof(Old.SealedChangeModel), typeof(New.SealedChangeModel), TypeSealedChange.IdConstant),
            ChangeModelData.WithLabel("Type Unsealed", typeof(Old.UnsealedChangeModel), typeof(New.UnsealedChangeModel), TypeUnsealedChange.IdConstant),
            ChangeModelData.WithLabel("Type Remove", typeof(Old.RemoveChangeModel), null, TypeRemoveChange.IdConstant),
            ChangeModelData.WithLabel("Type Base Class to Struct", typeof(Old.BaseClassToStructChangeModel), typeof(New.BaseClassToStructChangeModel), TypeBaseChange.IdConstant, 3),
            ChangeModelData.WithLabel("Type Base Class to Enum", typeof(Old.BaseClassToEnumChangeModel), typeof(New.BaseClassToEnumChangeModel), TypeBaseChange.IdConstant, 4),
            ChangeModelData.WithLabel("Type Base Class to Interface", typeof(Old.BaseClassToInterfaceChangeModel), typeof(New.BaseClassToInterfaceChangeModel), TypeBaseChange.IdConstant, 3),
            ChangeModelData.WithLabel("Type Base Struct to Class", typeof(Old.BaseStructToClassChangeModel), typeof(New.BaseStructToClassChangeModel), TypeBaseChange.IdConstant, 3),
            ChangeModelData.WithLabel("Type Base Struct to Enum", typeof(Old.BaseStructToEnumChangeModel), typeof(New.BaseStructToEnumChangeModel), TypeBaseChange.IdConstant, 2),
            ChangeModelData.WithLabel("Type Base Struct to Interface", typeof(Old.BaseStructToInterfaceChangeModel), typeof(New.BaseStructToInterfaceChangeModel), TypeBaseChange.IdConstant, 3),
            ChangeModelData.WithLabel("Type Base Enum to Class", typeof(Old.BaseEnumToClassChangeModel), typeof(New.BaseEnumToClassChangeModel), TypeBaseChange.IdConstant, 4),
            ChangeModelData.WithLabel("Type Base Enum to Struct", typeof(Old.BaseEnumToStructChangeModel), typeof(New.BaseEnumToStructChangeModel), TypeBaseChange.IdConstant, 2),
            ChangeModelData.WithLabel("Type Base Enum to Interface", typeof(Old.BaseEnumToInterfaceChangeModel), typeof(New.BaseEnumToInterfaceChangeModel), TypeBaseChange.IdConstant, 4),
            ChangeModelData.WithLabel("Type Base Interface to Class", typeof(Old.BaseInterfaceToClassChangeModel), typeof(New.BaseInterfaceToClassChangeModel), TypeBaseChange.IdConstant, 3),
            ChangeModelData.WithLabel("Type Base Interface to Struct", typeof(Old.BaseInterfaceToStructChangeModel), typeof(New.BaseInterfaceToStructChangeModel), TypeBaseChange.IdConstant, 3),
            ChangeModelData.WithLabel("Type Base Interface to Enum", typeof(Old.BaseInterfaceToEnumChangeModel), typeof(New.BaseInterfaceToEnumChangeModel), TypeBaseChange.IdConstant, 4),
        };
    }
}

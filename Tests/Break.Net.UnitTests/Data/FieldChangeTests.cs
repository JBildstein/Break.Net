extern alias NewModels;
extern alias OldModels;
using BreakDotNet.Changes;
using New = NewModels::BreakDotNet.UnitTests.Models.Fields;
using Old = OldModels::BreakDotNet.UnitTests.Models.Fields;

namespace BreakDotNet.UnitTests
{
    public class FieldChangeData
    {
        public static object[][] Data =
        {
            ChangeModelData.WithLabel("Field Add", typeof(Old.AddChangeModel), typeof(New.AddChangeModel), FieldAddChange.IdConstant),
            ChangeModelData.WithLabel("Field Constant Add", typeof(Old.ConstantAddChangeModel), typeof(New.ConstantAddChangeModel), FieldConstantAddChange.IdConstant),
            ChangeModelData.WithLabel("Field Constant Remove", typeof(Old.ConstantRemoveChangeModel), typeof(New.ConstantRemoveChangeModel), FieldConstantRemoveChange.IdConstant),
            ChangeModelData.WithLabel("Field Readonly Add", typeof(Old.ReadonlyAddChangeModel), typeof(New.ReadonlyAddChangeModel), FieldReadonlyAddChange.IdConstant),
            ChangeModelData.WithLabel("Field Readonly Remove", typeof(Old.ReadonlyRemoveChangeModel), typeof(New.ReadonlyRemoveChangeModel), FieldReadonlyRemoveChange.IdConstant),
            ChangeModelData.WithLabel("Field Remove", typeof(Old.RemoveChangeModel), typeof(New.RemoveChangeModel), FieldRemoveChange.IdConstant),
            ChangeModelData.WithLabel("Field Type", typeof(Old.TypeChangeModel), typeof(New.TypeChangeModel), FieldTypeChange.IdConstant),
        };
    }
}

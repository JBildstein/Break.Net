extern alias NewModels;
extern alias OldModels;
using BreakDotNet.Changes;
using New = NewModels::BreakDotNet.UnitTests.Models.Constructors;
using Old = OldModels::BreakDotNet.UnitTests.Models.Constructors;

namespace BreakDotNet.UnitTests
{
    public class ConstructorChangeData
    {
        public static object[][] Data =
        {
            ChangeModelData.WithLabel("Constructor Add", typeof(Old.AddChangeModel), typeof(New.AddChangeModel), ConstructorAddChange.IdConstant),
            ChangeModelData.WithLabel("Constructor Parameter Default Value", typeof(Old.ParameterDefaultValueChangeModel), typeof(New.ParameterDefaultValueChangeModel), ConstructorParameterDefaultValueChange.IdConstant),
            ChangeModelData.WithLabel("Constructor Parameter Meta Out", typeof(Old.ParameterMetaOutChangeModel), typeof(New.ParameterMetaOutChangeModel), ConstructorParameterMetaChange.IdConstant),
            ChangeModelData.WithLabel("Constructor Parameter Meta Ref", typeof(Old.ParameterMetaRefChangeModel), typeof(New.ParameterMetaRefChangeModel), ConstructorParameterMetaChange.IdConstant),
            ChangeModelData.WithLabel("Constructor Parameter Meta Default", typeof(Old.ParameterMetaDefaultChangeModel), typeof(New.ParameterMetaDefaultChangeModel), ConstructorParameterMetaChange.IdConstant),
            ChangeModelData.WithLabel("Constructor Parameter Name", typeof(Old.ParameterNameChangeModel), typeof(New.ParameterNameChangeModel), ConstructorParameterNameChange.IdConstant),
            ChangeModelData.WithLabel("Constructor Parameter Type", typeof(Old.ParameterTypeChangeModel), typeof(New.ParameterTypeChangeModel), ConstructorParameterTypeChange.IdConstant),
            ChangeModelData.WithLabel("Constructor Remove", typeof(Old.RemoveChangeModel), typeof(New.RemoveChangeModel), ConstructorRemoveChange.IdConstant),
        };
    }
}

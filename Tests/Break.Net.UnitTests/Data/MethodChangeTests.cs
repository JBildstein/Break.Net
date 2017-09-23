extern alias NewModels;
extern alias OldModels;
using BreakDotNet.Changes;
using New = NewModels::BreakDotNet.UnitTests.Models.Methods;
using Old = OldModels::BreakDotNet.UnitTests.Models.Methods;

namespace BreakDotNet.UnitTests
{
    public class MethodChangeData
    {
        public static object[][] Data =
        {
            ChangeModelData.WithLabel("Method Add", typeof(Old.AddChangeModel), typeof(New.AddChangeModel), MethodAddChange.IdConstant),
            ChangeModelData.WithLabel("Method Parameter Default Value", typeof(Old.ParameterDefaultValueChangeModel), typeof(New.ParameterDefaultValueChangeModel), MethodParameterDefaultValueChange.IdConstant),
            ChangeModelData.WithLabel("Method Parameter Meta Out", typeof(Old.ParameterMetaOutChangeModel), typeof(New.ParameterMetaOutChangeModel), MethodParameterMetaChange.IdConstant),
            ChangeModelData.WithLabel("Method Parameter Meta Ref", typeof(Old.ParameterMetaRefChangeModel), typeof(New.ParameterMetaRefChangeModel), MethodParameterMetaChange.IdConstant),
            ChangeModelData.WithLabel("Method Parameter Meta Default", typeof(Old.ParameterMetaDefaultChangeModel), typeof(New.ParameterMetaDefaultChangeModel), MethodParameterMetaChange.IdConstant),
            ChangeModelData.WithLabel("Method Parameter Name", typeof(Old.ParameterNameChangeModel), typeof(New.ParameterNameChangeModel), MethodParameterNameChange.IdConstant),
            ChangeModelData.WithLabel("Method Parameter Type", typeof(Old.ParameterTypeChangeModel), typeof(New.ParameterTypeChangeModel), MethodParameterTypeChange.IdConstant),
            ChangeModelData.WithLabel("Method Remove", typeof(Old.RemoveChangeModel), typeof(New.RemoveChangeModel), MethodRemoveChange.IdConstant),
            ChangeModelData.WithLabel("Method Return Type", typeof(Old.ReturnTypeChangeModel), typeof(New.ReturnTypeChangeModel), MethodReturnTypeChange.IdConstant),
        };
    }
}

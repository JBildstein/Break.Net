extern alias NewModels;
extern alias OldModels;
using BreakDotNet.Changes;
using New = NewModels::BreakDotNet.UnitTests.Models.Properties;
using Old = OldModels::BreakDotNet.UnitTests.Models.Properties;

namespace BreakDotNet.UnitTests
{
    public class PropertyChangeData
    {
        public static object[][] Data =
        {
            ChangeModelData.WithLabel("Property Add", typeof(Old.AddChangeModel), typeof(New.AddChangeModel), PropertyAddChange.IdConstant),
            ChangeModelData.WithLabel("Property Getter Add", typeof(Old.GetterAddModel), typeof(New.GetterAddModel), PropertyGetterAddChange.IdConstant),
            ChangeModelData.WithLabel("Property Getter Remove", typeof(Old.GetterRemoveModel), typeof(New.GetterRemoveModel), PropertyGetterRemoveChange.IdConstant),
            ChangeModelData.WithLabel("Property Remove", typeof(Old.RemoveChangeModel), typeof(New.RemoveChangeModel), PropertyRemoveChange.IdConstant),
            ChangeModelData.WithLabel("Property Setter Add", typeof(Old.SetterAddChangeModel), typeof(New.SetterAddChangeModel), PropertySetterAddChange.IdConstant),
            ChangeModelData.WithLabel("Property Setter Remove", typeof(Old.SetterRemoveChangeModel), typeof(New.SetterRemoveChangeModel), PropertySetterRemoveChange.IdConstant),
            ChangeModelData.WithLabel("Property Type", typeof(Old.TypeChangeModel), typeof(New.TypeChangeModel), PropertyTypeChange.IdConstant),
        };
    }
}

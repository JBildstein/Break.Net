extern alias NewModels;
extern alias OldModels;
using BreakDotNet.Changes;
using New = NewModels::BreakDotNet.UnitTests.Models.Events;
using Old = OldModels::BreakDotNet.UnitTests.Models.Events;

namespace BreakDotNet.UnitTests
{
    public class EventChangeData
    {
        public static object[][] Data =
        {
            ChangeModelData.WithLabel("Event Add", typeof(Old.AddChangeModel), typeof(New.AddChangeModel), EventAddChange.IdConstant),
            ChangeModelData.WithLabel("Event Remove", typeof(Old.RemoveChangeModel), typeof(New.RemoveChangeModel), EventRemoveChange.IdConstant),
            ChangeModelData.WithLabel("Event Type", typeof(Old.TypeChangeModel), typeof(New.TypeChangeModel), EventTypeChange.IdConstant),
        };
    }
}

using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "GetPosition",
    description: "Stores the world position of a GameObject (or its local position if Use Local Position " +
                 "is enabled in the Inspector). Fails if the target is missing. " +
                 "Feeds the other vector nodes, which work on positions.",
    story: "[Result] = Position of [Target]",
    category: "Math/Vector",
    id: "e84a1d6c3b9f4207a5c8e1b0d4f62a93")]
public partial class GetPositionAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Result;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    //Optional setting, shown in the node Inspector rather than in the story
    [SerializeReference] public BlackboardVariable<bool> UseLocalPosition = new BlackboardVariable<bool>(false);

    protected override Status OnStart()
    {
        if (Result == null || Target == null || Target.Value == null)
        {
            return Status.Failure;
        }

        Transform t = Target.Value.transform;
        bool local = UseLocalPosition != null && UseLocalPosition.Value;

        Result.Value = local ? t.localPosition : t.position;
        return Status.Success;
    }
}

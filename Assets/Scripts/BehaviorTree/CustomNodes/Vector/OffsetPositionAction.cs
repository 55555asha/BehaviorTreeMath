using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "OffsetPosition",
    description: "Origin + Direction * Distance. By default the direction is normalized first, " +
                 "so Distance is the real distance moved. Turn off Normalize Direction (Inspector) " +
                 "to use the direction's own length as a multiplier.",
    story: "[Result] = [Origin] + [Direction] * [Distance]",
    category: "Math/Vector",
    id: "49c6e2b8a1d34f75a0e9c5b3d7f18a60")]
public partial class OffsetPositionAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Result;
    [SerializeReference] public BlackboardVariable<Vector3> Origin;
    [SerializeReference] public BlackboardVariable<Vector3> Direction;
    [SerializeReference] public BlackboardVariable<float> Distance;

    //Optional setting, shown in the node Inspector rather than in the story
    [SerializeReference] public BlackboardVariable<bool> NormalizeDirection = new BlackboardVariable<bool>(true);

    protected override Status OnStart()
    {
        if (Result == null || Origin == null || Direction == null || Distance == null)
        {
            return Status.Failure;
        }

        Vector3 direction = Direction.Value;
        if (NormalizeDirection == null || NormalizeDirection.Value)
        {
            direction = direction.normalized;
        }

        Result.Value = Origin.Value + direction * Distance.Value;
        return Status.Success;
    }
}

using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "DirectionTo",
    description: "Direction from one position to another. Normalized by default (zero if the " +
                 "positions coincide). Optionally flattened onto the ground plane (Y = 0).",
    story: "[Result] = Direction from [From] to [To]",
    category: "Math/Vector",
    id: "6d9e1c3a8f5b42e7a0c4b2d85f1e9a73")]
public partial class DirectionToAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Result;
    [SerializeReference] public BlackboardVariable<Vector3> From;
    [SerializeReference] public BlackboardVariable<Vector3> To;

    //Optional settings, shown in the node Inspector rather than in the story
    [SerializeReference] public BlackboardVariable<bool> FlattenY = new BlackboardVariable<bool>(false);
    [SerializeReference] public BlackboardVariable<bool> Normalize = new BlackboardVariable<bool>(true);

    protected override Status OnStart()
    {
        if (Result == null || From == null || To == null)
        {
            return Status.Failure;
        }

        Vector3 direction = To.Value - From.Value;

        if (FlattenY != null && FlattenY.Value)
        {
            direction.y = 0f;
        }

        if (Normalize == null || Normalize.Value)
        {
            //Vector3.normalized already returns zero for (near) zero-length vectors
            direction = direction.normalized;
        }

        Result.Value = direction;
        return Status.Success;
    }
}

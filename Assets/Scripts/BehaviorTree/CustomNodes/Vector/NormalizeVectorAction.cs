using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "NormalizeVector",
    description: "Stores the vector scaled to length 1. A zero vector stays zero.",
    story: "[Result] = Normalized [Vector]",
    category: "Math/Vector",
    id: "a7035f8c2e1d4b96c4f8a1e3d0b57926")]
public partial class NormalizeVectorAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Result;
    [SerializeReference] public BlackboardVariable<Vector3> Vector;

    protected override Status OnStart()
    {
        if (Result == null || Vector == null)
        {
            return Status.Failure;
        }

        Result.Value = Vector.Value.normalized;
        return Status.Success;
    }
}

using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "ScaleVector",
    description: "Multiplies a vector by a float.",
    story: "[Result] = [Vector] * [Scalar]",
    category: "Math/Vector",
    id: "8a14d6f2e5c7490bb3a0d1e9f6c42587")]
public partial class ScaleVectorAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Result;
    [SerializeReference] public BlackboardVariable<Vector3> Vector;
    [SerializeReference] public BlackboardVariable<float> Scalar;

    protected override Status OnStart()
    {
        if (Result == null || Vector == null || Scalar == null)
        {
            return Status.Failure;
        }

        Result.Value = Vector.Value * Scalar.Value;
        return Status.Success;
    }
}

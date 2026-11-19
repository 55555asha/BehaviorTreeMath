using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

public enum MagnitudeMode
{
    Magnitude, SqrMagnitude
}

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "VectorMagnitude",
    description: "Length of a vector (or its square, which is cheaper to compute).",
    story: "[Result] = [Mode] of [Vector]",
    category: "Math/Vector",
    id: "2b8e4d7a0c9f4163e5a7b1d8c3f06e94")]
public partial class VectorMagnitudeAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Result;
    [SerializeReference] public BlackboardVariable<MagnitudeMode> Mode;
    [SerializeReference] public BlackboardVariable<Vector3> Vector;

    protected override Status OnStart()
    {
        if (Result == null || Mode == null || Vector == null)
        {
            return Status.Failure;
        }

        Result.Value = Mode.Value == MagnitudeMode.SqrMagnitude
            ? Vector.Value.sqrMagnitude
            : Vector.Value.magnitude;

        return Status.Success;
    }
}

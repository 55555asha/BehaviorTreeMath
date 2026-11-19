using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "BreakVector",
    description: "Splits a Vector3 into its X, Y and Z floats. Outputs you don't need can stay unlinked.",
    story: "Break [Vector] into [X], [Y], [Z]",
    category: "Math/Vector",
    id: "c3b92e6a4d8f4015a7e1b5c9d0f38e24")]
public partial class BreakVectorAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Vector;
    [SerializeReference] public BlackboardVariable<float> X;
    [SerializeReference] public BlackboardVariable<float> Y;
    [SerializeReference] public BlackboardVariable<float> Z;

    protected override Status OnStart()
    {
        if (Vector == null)
        {
            return Status.Failure;
        }

        Vector3 v = Vector.Value;

        if (X != null) X.Value = v.x;
        if (Y != null) Y.Value = v.y;
        if (Z != null) Z.Value = v.z;

        return Status.Success;
    }
}

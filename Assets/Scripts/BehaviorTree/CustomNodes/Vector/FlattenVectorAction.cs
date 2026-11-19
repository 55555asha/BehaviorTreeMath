using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

public enum FlattenAxis
{
    X, Y, Z
}

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "FlattenVector",
    description: "Sets one component of a vector to a fixed value (0 by default). " +
                 "Flattening Y is the usual way to project a 3D position or direction onto the ground.",
    story: "[Result] = flatten [Vector] on [Axis] to [Value]",
    category: "Math/Vector",
    id: "0f8d4b1e7a3c4952b6a0d8e3c5f17b96")]
public partial class FlattenVectorAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Result;
    [SerializeReference] public BlackboardVariable<Vector3> Vector;
    [SerializeReference] public BlackboardVariable<FlattenAxis> Axis = new BlackboardVariable<FlattenAxis>(FlattenAxis.Y);
    [SerializeReference] public BlackboardVariable<float> Value = new BlackboardVariable<float>(0f);

    protected override Status OnStart()
    {
        if (Result == null || Vector == null || Axis == null || Value == null)
        {
            return Status.Failure;
        }

        Vector3 v = Vector.Value;

        switch (Axis.Value)
        {
            case FlattenAxis.X: v.x = Value.Value; break;
            case FlattenAxis.Z: v.z = Value.Value; break;
            default:            v.y = Value.Value; break;
        }

        Result.Value = v;
        return Status.Success;
    }
}

using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

public enum DistanceMode
{
    Distance, //Straight-line distance
    SqrDistance, //Squared distance, cheaper, good for comparisons
    PlanarDistance, //Ignores Y (distance on the XZ plane)
    PlanarSqrDistance // Squared, ignores Y
}

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "Distance",
    description: "Distance between two positions. Squared variants skip the square root. " +
                 "Planar variants ignore the Y axis.",
    story: "[Result] = [Mode] between [A] and [B]",
    category: "Math/Vector",
    id: "f2c80b4e91a6473d85e1c7a09d3b6f42")]
public partial class DistanceAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Result;
    [SerializeReference] public BlackboardVariable<DistanceMode> Mode;
    [SerializeReference] public BlackboardVariable<Vector3> A;
    [SerializeReference] public BlackboardVariable<Vector3> B;

    protected override Status OnStart()
    {
        if (Result == null || Mode == null || A == null || B == null)
        {
            return Status.Failure;
        }

        Vector3 delta = A.Value - B.Value;

        switch (Mode.Value)
        {
            case DistanceMode.SqrDistance:
                Result.Value = delta.sqrMagnitude;
                break;
            case DistanceMode.PlanarDistance:
                delta.y = 0f;
                Result.Value = delta.magnitude;
                break;
            case DistanceMode.PlanarSqrDistance:
                delta.y = 0f;
                Result.Value = delta.sqrMagnitude;
                break;
            default:
                Result.Value = delta.magnitude;
                break;
        }

        return Status.Success;
    }
}

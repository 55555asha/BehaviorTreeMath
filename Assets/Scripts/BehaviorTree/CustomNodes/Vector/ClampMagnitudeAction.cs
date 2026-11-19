using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "ClampMagnitude",
    description: "Limits a vector's length to MaxLength while keeping its direction. " +
                 "Optionally also enforces a minimum length (Inspector). " +
                 "A zero vector stays zero because it has no direction.",
    story: "[Result] = [Vector] clamped to length [MaxLength]",
    category: "Math/Vector",
    id: "d5f1a39c7e2b4086b9c3e0a4f8d71b52")]
public partial class ClampMagnitudeAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Result;
    [SerializeReference] public BlackboardVariable<Vector3> Vector;
    [SerializeReference] public BlackboardVariable<float> MaxLength;

    //Optional setting, shown in the node Inspector rather than in the story
    [SerializeReference] public BlackboardVariable<float> MinLength = new BlackboardVariable<float>(0f);

    protected override Status OnStart()
    {
        if (Result == null || Vector == null || MaxLength == null)
        {
            return Status.Failure;
        }

        Vector3 v = Vector.Value;
        float magnitude = v.magnitude;

        if (magnitude < 1e-6f)
        {
            Result.Value = Vector3.zero;
            return Status.Success;
        }

        float max = Mathf.Max(0f, MaxLength.Value);
        float min = MinLength != null ? Mathf.Clamp(MinLength.Value, 0f, max) : 0f;
        float clamped = Mathf.Clamp(magnitude, min, max);

        Result.Value = v * (clamped / magnitude);
        return Status.Success;
    }
}

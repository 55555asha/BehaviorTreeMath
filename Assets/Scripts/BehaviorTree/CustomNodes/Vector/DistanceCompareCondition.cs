using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

public enum DistanceComparison
{
    Below, Above
}

[Serializable, GeneratePropertyBag]
[Condition(
    name: "Distance Compare",
    story: "Distance between [A] and [B] is [Comparison] [Threshold]",
    category: "Math/Vector",
    id: "b61e9f3a5c2d4780a4c7e0b9d3f52a18")]
public partial class DistanceCompareCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Vector3> A;
    [SerializeReference] public BlackboardVariable<Vector3> B;
    [SerializeReference] public BlackboardVariable<DistanceComparison> Comparison;
    [SerializeReference] public BlackboardVariable<float> Threshold;

    //Optional setting, shown in the Inspector rather than in the story
    [SerializeReference] public BlackboardVariable<bool> Planar = new BlackboardVariable<bool>(false);

    public override bool IsTrue()
    {
        if (A == null || B == null || Comparison == null || Threshold == null)
        {
            return false;
        }

        Vector3 delta = A.Value - B.Value;
        if (Planar != null && Planar.Value)
        {
            delta.y = 0f;
        }

        //Compare squared values to avoid a square root
        float threshold = Mathf.Abs(Threshold.Value);
        float sqrDistance = delta.sqrMagnitude;
        float sqrThreshold = threshold * threshold;

        return Comparison.Value == DistanceComparison.Below
            ? sqrDistance < sqrThreshold
            : sqrDistance > sqrThreshold;
    }
}

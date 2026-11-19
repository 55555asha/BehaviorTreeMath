using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "RandomFloat",
    description: "Stores a random float between Min and Max (both inclusive).",
    story: "[Result] = Random float between [Min] and [Max]",
    category: "Math/Random",
    id: "b85b5c0e01491d0fe74d579acb48c447")]
public partial class RandomFloatAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Result;
    [SerializeReference] public BlackboardVariable<float> Min;
    [SerializeReference] public BlackboardVariable<float> Max;
 
    protected override Status OnStart()
    {
        if (Result == null || Min == null || Max == null)
        {
            return Status.Failure;
        }
 
        //UnityEngine.Random.Range also works when Min > Max, so no swap needed.
        Result.Value = UnityEngine.Random.Range(Min.Value, Max.Value);
        return Status.Success;
    }
}
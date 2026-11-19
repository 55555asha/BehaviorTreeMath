using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "RandomBool",
    description: "Stores true with the given probability (0 = never, 1 = always).",
    story: "[Result] = Random bool with [Chance] chance of true",
    category: "Math/Random",
    id: "a0ff550b75a55e970d7b80e7f943b4d5")]
public partial class RandomBoolAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> Result;
    [SerializeReference] public BlackboardVariable<float> Chance;

    protected override Status OnStart()
    {
        if (Result == null || Chance == null)
        {
            return Status.Failure;
        }

        float p = Mathf.Clamp01(Chance.Value);

        
        Result.Value = p >= 1f || UnityEngine.Random.value < p; //Random.value is in [0, 1] inclusive, so handle p = 1 explicitly to guarantee true.
        return Status.Success;
    }
}


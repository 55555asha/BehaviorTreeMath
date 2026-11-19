using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "RandomInt",
    description: "Stores a random int between Min and Max. Max is inclusive by default. " +
                 "With Avoid Repeat, the value currently stored in Result is never picked again.",
    story: "[Result] = Random integer between [Min] and [Max]",
    category: "Math/Random",
    id: "f18a0ec0cc8af9795faa9fe2c37f58b4")]
public partial class RandomIntAction : Action
{
    [SerializeReference] public BlackboardVariable<int> Result;
    [SerializeReference] public BlackboardVariable<int> Min;
    [SerializeReference] public BlackboardVariable<int> Max;

    //Optional settings, shown in the node Inspector rather than in the story.
    [SerializeReference] public BlackboardVariable<bool> InclusiveMax = new BlackboardVariable<bool>(true);
    [SerializeReference] public BlackboardVariable<bool> AvoidRepeat = new BlackboardVariable<bool>(false);

    protected override Status OnStart()
    {
        if (Result == null || Min == null || Max == null)
        {
            return Status.Failure;
        }

        int lo = Math.Min(Min.Value, Max.Value);
        int hi = Math.Max(Min.Value, Max.Value);

        //UnityEngine.Random.Range(int, int) has an exclusive upper bound.
        bool inclusive = InclusiveMax == null || InclusiveMax.Value;
        int hiExclusive = inclusive ? hi + 1 : hi;

        int count = hiExclusive - lo;
        if (count <= 0)
        {
            Result.Value = lo;
            return Status.Success;
        }

        bool avoidRepeat = AvoidRepeat != null && AvoidRepeat.Value;
        int previous = Result.Value;

        if (avoidRepeat && count > 1 && previous >= lo && previous < hiExclusive)
        {
            //Draw from one fewer value, then skip over the previous one.
            int value = UnityEngine.Random.Range(lo, hiExclusive - 1);
            if (value >= previous)
            {
                value++;
            }
            Result.Value = value;
        }
        else
        {
            Result.Value = UnityEngine.Random.Range(lo, hiExclusive);
        }

        return Status.Success;
    }
}

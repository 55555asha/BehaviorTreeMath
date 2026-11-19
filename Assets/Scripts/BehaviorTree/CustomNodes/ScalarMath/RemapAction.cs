using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "Remap",
    description: "Remaps a value from an InRange to an OutRange.",
    story: "[Result] = Remap [V] from an ([InMin], [InMax]) range to an ([OutMin], [OutMax]) range.",
    category: "Math/ScalarMath",
    id: "d85497e0bb4dad3e899f2f3ac18cf779")]
public partial class RemapAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Result;
    [SerializeReference] public BlackboardVariable<float> V;
    [SerializeReference] public BlackboardVariable<float> InMin;
    [SerializeReference] public BlackboardVariable<float> InMax;
    [SerializeReference] public BlackboardVariable<float> OutMin;
    [SerializeReference] public BlackboardVariable<float> OutMax;

    protected override Status OnStart()
    {
        if (Result == null || V == null || InMin == null || InMax == null || OutMin == null || OutMax == null)
        {
            return Status.Failure;
        }

        float v = V.Value;
        float inMin = InMin.Value;
        float inMax = InMax.Value;
        float outMin = OutMin.Value;
        float outMax = OutMax.Value;

        Result.Value = outMin + (((v - inMin) * (outMax - outMin)) / (inMax - inMin));

        return Status.Success;
    }
}


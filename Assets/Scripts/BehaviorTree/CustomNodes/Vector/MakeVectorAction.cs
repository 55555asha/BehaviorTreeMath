using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "MakeVector",
    description: "Builds a Vector3 from three floats.",
    story: "[Result] = Vector3( [X], [Y], [Z] )",
    category: "Math/Vector",
    id: "7e0a5c3f9b1d4826c8d4e2a6f0b93c17")]
public partial class MakeVectorAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Result;
    [SerializeReference] public BlackboardVariable<float> X;
    [SerializeReference] public BlackboardVariable<float> Y;
    [SerializeReference] public BlackboardVariable<float> Z;

    protected override Status OnStart()
    {
        if (Result == null || X == null || Y == null || Z == null)
        {
            return Status.Failure;
        }

        Result.Value = new Vector3(X.Value, Y.Value, Z.Value);
        return Status.Success;
    }
}

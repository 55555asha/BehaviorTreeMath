using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

public enum VectorOperation
{
    Add, Subtract, Multiply, Divide
}

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "MathVector",
    description: "Vector math: Add, Subtract, and component-wise Multiply / Divide. " +
                 "Dividing by a zero component gives 0 for that component.",
    story: "[Result] = [Op]( [A], [B] )",
    category: "Math/Vector",
    id: "3e5b7a91c2d84f06a1b9e4d70c8f2a35")]
public partial class MathVectorAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Result;
    [SerializeReference] public BlackboardVariable<Vector3> A;
    [SerializeReference] public BlackboardVariable<VectorOperation> Op;
    [SerializeReference] public BlackboardVariable<Vector3> B;

    protected override Status OnStart()
    {
        if (Result == null || A == null || Op == null || B == null)
        {
            return Status.Failure;
        }

        Vector3 a = A.Value;
        Vector3 b = B.Value;

        Result.Value = Op.Value switch
        {
            VectorOperation.Add => a + b,
            VectorOperation.Subtract => a - b,
            VectorOperation.Multiply => Vector3.Scale(a, b),
            VectorOperation.Divide => new Vector3(SafeDivide(a.x, b.x), SafeDivide(a.y, b.y), SafeDivide(a.z, b.z)),
            _ => a
        };

        return Status.Success;
    }

    private static float SafeDivide(float a, float b) => b != 0f ? a / b : 0f;
}

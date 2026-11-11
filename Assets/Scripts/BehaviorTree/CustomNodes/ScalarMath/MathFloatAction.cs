using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

public enum MathOperation
{
    Add, Subtract, Multiply, Divide, Modulo, Pow, Min, Max
}

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "MathFloat",
    description: "Different Math operations for float values.",
    story: "[Result] = [Op]( [A], [B])",
    category: "Math/ScalarMath",
    id: "2d67de33d546845005dd591d675b98ed")]
public partial class MathFloatAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Result;
    [SerializeReference] public BlackboardVariable<float> A;
    [SerializeReference] public BlackboardVariable<MathOperation> Op;
    [SerializeReference] public BlackboardVariable<float> B;

    protected override Status OnStart()
    {
        if(Result == null || A == null || Op == null || B == null)
        {
            return Status.Failure;
        }
        
        float a = A.Value;
        float b = B.Value;

        Result.Value = Op.Value switch
        {
            MathOperation.Add => a + b,
            MathOperation.Subtract => a - b,
            MathOperation.Multiply => a * b,
            MathOperation.Divide => b != 0f? a / b : 0f,
            MathOperation.Modulo => b != 0f? a % b : 0f,
            MathOperation.Pow => Mathf.Pow(a, b),
            MathOperation.Min => Mathf.Min(a, b),
            MathOperation.Max => Mathf.Max(a, b),
            _ => a
        };

        return Status.Success;
    }
    
}


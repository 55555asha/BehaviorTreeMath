using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "MathInt",
    description: "Different Math operations for integer values.",
    story: "[Result] = [Op]( [A], [B])",
    category: "Math/ScalarMath",
    id: "d9f4d7d6f538de4f16ff7a820b26ba42")]
public partial class MathIntAction : Action
{
    [SerializeReference] public BlackboardVariable<int> Result;
    [SerializeReference] public BlackboardVariable<int> A;
    [SerializeReference] public BlackboardVariable<MathOperation> Op;
    [SerializeReference] public BlackboardVariable<int> B;

    protected override Status OnStart()
    {
        if(Result == null || A == null || Op == null || B == null)
        {
            return Status.Failure;
        }
        
        int a = A.Value;
        int b = B.Value;

        Result.Value = Op.Value switch
        {
            MathOperation.Add => a + b,
            MathOperation.Subtract => a - b,
            MathOperation.Multiply => a * b,
            MathOperation.Divide => b != 0? a / b : 0,
            MathOperation.Modulo => b != 0? a % b : 0,
            MathOperation.Pow => (int)Math.Pow(a, b),
            MathOperation.Min => Math.Min(a, b),
            MathOperation.Max => Math.Max(a, b),
            _ => a
        };

        return Status.Success;
    }
    
}



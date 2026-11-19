using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

public enum UnaryIntOperation
{
    Abs, Negate, Sign, Square, Increment, Decrement, Half, Double
}

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "UnaryInt",
    description: "Unary Math operations for Integer values.",
    story: "[Result] = [Op]( [A] )",
    category: "Math/ScalarMath",
    id: "79c999f4817f7bbf762bcd9e8fb4e299")]
public partial class UnaryIntAction : Action
{
    [SerializeReference] public BlackboardVariable<int> Result;
    [SerializeReference] public BlackboardVariable<UnaryIntOperation> Op;
    [SerializeReference] public BlackboardVariable<int> A;

    protected override Status OnStart()
    {
        if (Result == null || Op == null || A == null)
        {
            return Status.Failure;
        }

        int a = A.Value;
 
        Result.Value = Op.Value switch
        {
            UnaryIntOperation.Abs => Math.Abs(a),
            UnaryIntOperation.Negate => -a,
            UnaryIntOperation.Sign => Math.Sign(a),
            UnaryIntOperation.Square => a * a,
            UnaryIntOperation.Increment => a + 1,
            UnaryIntOperation.Decrement => a - 1,
            UnaryIntOperation.Half => a / 2, //Integer division, truncates toward zero
            UnaryIntOperation.Double => a * 2,
            _ => a
        };
        
        return Status.Success;
    }
}


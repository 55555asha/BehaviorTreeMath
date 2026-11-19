using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

public enum UnaryFloatOperation
{
    Abs, Negate, Sign, Floor, Ceil, Round, Frac, Sqrt, Square, Reciprocal, Saturate, Sin, Cos, DegToRad, RadToDeg
}

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "UnaryFloat",
    description: "Unary math operations for float values (Abs, Floor, Sqrt, ...).",
    story: "[Result] = [Op]( [A] )",
    category: "Math/ScalarMath",
    id: "f162bd5c145105e4555455f156358a5a")]
public partial class UnaryFloatAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Result;
    [SerializeReference] public BlackboardVariable<UnaryFloatOperation> Op;
    [SerializeReference] public BlackboardVariable<float> A;

    protected override Status OnStart()
    {
        if (Result == null || Op == null || A == null)
        {
            return Status.Failure;
        }

        float a = A.Value;

        Result.Value = Op.Value switch
        {
            UnaryFloatOperation.Abs => Mathf.Abs(a),
            UnaryFloatOperation.Negate => -a,
            UnaryFloatOperation.Sign => a == 0f ? 0f : Mathf.Sign(a), //Mathf.Sign(0) returns 1, so handle zero explicitly.
            UnaryFloatOperation.Floor => Mathf.Floor(a),
            UnaryFloatOperation.Ceil => Mathf.Ceil(a),
            UnaryFloatOperation.Round => Mathf.Round(a),
            UnaryFloatOperation.Frac => a - Mathf.Floor(a),
            UnaryFloatOperation.Sqrt => Mathf.Sqrt(Mathf.Max(0f, a)), //Negative input would give NaN, clamp to 0 instead.
            UnaryFloatOperation.Square => a * a,
            UnaryFloatOperation.Reciprocal => a != 0f ? 1f / a : 0f, //Same convention as MathFloat's Divide: division by zero yields 0.
            UnaryFloatOperation.Saturate => Mathf.Clamp01(a),
            UnaryFloatOperation.Sin => Mathf.Sin(a), //Radians
            UnaryFloatOperation.Cos => Mathf.Cos(a), //Radians
            UnaryFloatOperation.DegToRad => a * Mathf.Deg2Rad,
            UnaryFloatOperation.RadToDeg => a * Mathf.Rad2Deg,
            _ => a
        };

        return Status.Success;
    }
}
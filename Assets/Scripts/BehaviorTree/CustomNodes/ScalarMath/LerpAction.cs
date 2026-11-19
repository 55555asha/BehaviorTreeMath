using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.VisualScripting;

public enum LerpOperation
{
    Lerp, InverseLerp
}

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "Lerp",
    description: "Lerp and InverseLerp operations for float values.",
    story: "[Result] = [Op]( [A], [B], [T]",
    category: "Math/ScalarMath",
    id: "177f490309ceef88e3e5864a4e132334")]
public partial class LerpAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Result;
    [SerializeReference] public BlackboardVariable<LerpOperation> Op;
    [SerializeReference] public BlackboardVariable<float> A;
    [SerializeReference] public BlackboardVariable<float> B;
    [SerializeReference] public BlackboardVariable<float> T;
    
    protected override Status OnStart()
    {
        if(Result == null || Op == null|| A == null || B == null || T == null)
        {
            return Status.Failure;
        }
        
        float a = A.Value;
        float b = B.Value;
        float t = T.Value;

        Result.Value = Op.Value switch
        {
            LerpOperation.Lerp => a + t * (b - a),
            LerpOperation.InverseLerp => (t - a) / (b - a), //Consider T the X value in an InverseLerp
            _ => a
        };
        
        return Status.Success;
    }

}


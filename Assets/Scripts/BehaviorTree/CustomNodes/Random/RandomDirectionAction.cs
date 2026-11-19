using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

public enum RandomDirectionMode
{
    Sphere3D, // Any direction in 3D
    PlanarXZ, // Flat on the ground plane (Y = 0) (Top down)
    PlanarXY, // Flat on the XY plane (Z = 0), (2D side)
    Cone // Inside a cone around ConeAxis (Vision Cone)
}

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "RandomDirection",
    description: "Stores a random unit direction. Modes: Sphere3D, PlanarXZ, PlanarXY, or Cone " +
                 "(uses Cone Axis and Cone Half Angle from the Inspector).",
    story: "[Result] = Random direction ([Mode])",
    category: "Math/Random",
    id: "f810dbad5601f12699f375c6d313a2c3")]
public partial class RandomDirectionAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Result;
    [SerializeReference] public BlackboardVariable<RandomDirectionMode> Mode;

    // Only used by Cone mode. Shown in the node Inspector.
    [SerializeReference] public BlackboardVariable<Vector3> ConeAxis = new BlackboardVariable<Vector3>(Vector3.forward);
    [Tooltip("Half angle of the cone in degrees (0 to 180).")]
    [SerializeReference] public BlackboardVariable<float> ConeHalfAngle = new BlackboardVariable<float>(30f);

    protected override Status OnStart()
    {
        if (Result == null || Mode == null)
        {
            return Status.Failure;
        }

        switch (Mode.Value)
        {
            case RandomDirectionMode.PlanarXZ:
            {
                float a = UnityEngine.Random.Range(0f, Mathf.PI * 2f);
                Result.Value = new Vector3(Mathf.Cos(a), 0f, Mathf.Sin(a));
                break;
            }
            case RandomDirectionMode.PlanarXY:
            {
                float a = UnityEngine.Random.Range(0f, Mathf.PI * 2f);
                Result.Value = new Vector3(Mathf.Cos(a), Mathf.Sin(a), 0f);
                break;
            }
            case RandomDirectionMode.Cone:
            {
                Result.Value = RandomInCone();
                break;
            }
            case RandomDirectionMode.Sphere3D:
            {
                Result.Value = UnityEngine.Random.onUnitSphere;
                break;
            }
        }

        return Status.Success;
    }

    private Vector3 RandomInCone()
    {
        float halfAngle = Mathf.Clamp(ConeHalfAngle != null ? ConeHalfAngle.Value : 30f, 0f, 180f) * Mathf.Deg2Rad;

        //Sampling cos(theta) uniformly gives a uniform distribution over the cone's solid angle.
        float cosTheta = UnityEngine.Random.Range(Mathf.Cos(halfAngle), 1f);
        float sinTheta = Mathf.Sqrt(Mathf.Max(0f, 1f - cosTheta * cosTheta));
        float phi = UnityEngine.Random.Range(0f, Mathf.PI * 2f);

        //Direction in a cone pointing along +Z, then rotated onto the requested axis.
        Vector3 local = new Vector3(sinTheta * Mathf.Cos(phi), sinTheta * Mathf.Sin(phi), cosTheta);

        Vector3 axis = ConeAxis != null ? ConeAxis.Value : Vector3.forward;
        axis = axis.sqrMagnitude > 1e-6f ? axis.normalized : Vector3.forward;

        return Quaternion.FromToRotation(Vector3.forward, axis) * local;
    }
}


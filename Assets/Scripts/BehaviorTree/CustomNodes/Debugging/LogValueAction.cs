using System;
using Unity.Behavior;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "LogValueAction",
    description: "Logs the value of a variable to the console",
    story: "Logs the value of [Value]",
    category: "Debugging",
    id: "75b9a3b2167866a5829a751ec319a362")]
public partial class LogValueAction : Action
{
    [SerializeReference] public BlackboardVariable Value;

    protected override Status OnStart()
    {
        if (Value == null)
        {
            return Status.Failure;
        }

        if (Value is BlackboardVariable<int> valInt)
        {
            Debug.Log($"{Value.Name} = {valInt.Value}");
        }
        else if (Value is BlackboardVariable<float> valFloat)
        {
            Debug.Log($"{Value.Name} = {valFloat.Value}");
        }
        else if (Value is BlackboardVariable<double> valDouble)
        {
            Debug.Log($"{Value.Name} = {valDouble.Value}");
        }
        else if (Value is BlackboardVariable<string> valString)
        {
            Debug.Log($"{Value.Name} = {valString.Value}");
        }
        else if (Value is BlackboardVariable<bool> valBool)
        {
            Debug.Log($"{Value.Name} = {valBool.Value}");
        }
        else
        {
            Debug.Log($"{Value.Name} = {Value.ObjectValue}");
            Value.ObjectValue = Value.ObjectValue;
        }

        return Status.Running;
    }
}


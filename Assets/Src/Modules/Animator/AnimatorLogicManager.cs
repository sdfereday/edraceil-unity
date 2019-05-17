using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

// Logic Helpers
public static class Logic
{
    // Floats
    public static bool FloatEqual(float a, float b) => a == b;
    public static bool FloatGreaterThan(float a, float b) => a > b;
    public static bool FloatLessThan(float a, float b) => a < b;

    // Bools
    public static bool BoolTrue(bool a, bool b) => a == b;
    public static bool BoolFalse(bool a, bool b) => a != b;
}

// Conditions
public abstract class ConditionObject<T>
{
    public string Id;
    public T Value;
    public T Expected;
    public Func<T, T, bool> LogicMethod;

    public ConditionObject(string id, T initial, T expected, Func<T, T, bool> logicMethod)
    {
        Id = id;
        Value = initial;
        Expected = expected;
        LogicMethod = logicMethod;
    }

    public bool Assert() => LogicMethod(Value, Expected);
}

public class FloatCondition : ConditionObject<float>
{
    public FloatCondition(string id, float expected, float initial, Func<float, float, bool> logicMethod)
        : base(id, initial, expected, logicMethod) { }
}

public class BoolCondition : ConditionObject<bool>
{
    public BoolCondition(string id, bool expected, bool initial, Func<bool, bool, bool> logicMethod)
        : base(id, initial, expected, logicMethod) { }
}

[Serializable]
public class AnimGate
{
    public string playAnimation;
    public bool isTrigger = false;

    // You'd not be able to make a mixed list considering a list expects a particular type.
    public List<ConditionObject<float>> floatConditions;
    public List<ConditionObject<bool>> boolConditions;

    public void SetFloat(string query, float value)
    {
        if (floatConditions != null)
        {
            floatConditions.FirstOrDefault(condition => condition.Id == query)
                .Value = value;
        }
    }

    public void SetBool(string query, bool value)
    {
        if (boolConditions != null)
        { 
            boolConditions.FirstOrDefault(condition => condition.Id == query)
                .Value = value;
        }
    }

    public bool IsTruthy()
    {
        return floatConditions != null && floatConditions.All(x => x.Assert()) ||
            boolConditions != null && boolConditions.All(x => x.Assert());
    }
}

[RequireComponent(typeof(SpriteAnimator))]
public class AnimatorLogicManager : MonoBehaviour
{
    public SpriteAnimator SpriteAnimator;
    private List<AnimGate> GatesForPlayerTest;

    public void SetFloat(string id, float value)
    {
        if (GatesForPlayerTest != null)
            GatesForPlayerTest.ForEach(x => x.SetFloat(id, value));
    }

    public void SetBool(string id, bool value)
    {
        if (GatesForPlayerTest != null)
            GatesForPlayerTest.ForEach(x => x.SetBool(id, value));
    }

    private void Start()
    {
        // Condition id's MUST match your 'set' calls, or nothing will change.
        // Thinking this could work well in a static script for each different 'thing'.
        // TODO: const these magic strings
        GatesForPlayerTest = new List<AnimGate>() {
            new AnimGate()
            {
                playAnimation = "PlayerIdle",
                floatConditions = new List<ConditionObject<float>>()
                {
                    new FloatCondition("playerMagnitude", 0, 0, (float current, float expected)
                        => Logic.FloatEqual(current, expected)) 
                }
            },
            new AnimGate()
            {
                playAnimation = "PlayerRun",
                floatConditions = new List<ConditionObject<float>>()
                {
                    new FloatCondition("playerMagnitude", 0, 0, (float current, float expected)
                        => Logic.FloatGreaterThan(current, expected))
                }
            }
        };
    }

    private void Update()
    {
        AnimGate firstTruthyGate = GatesForPlayerTest.FirstOrDefault(x => x.IsTruthy());

        if (firstTruthyGate != null)
            SpriteAnimator.PlayAnimation(firstTruthyGate.playAnimation);
    }
}
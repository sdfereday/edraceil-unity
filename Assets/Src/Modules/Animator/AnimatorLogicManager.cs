using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// Values
public abstract class ConditionValue
{
    public string Id;
}

public class FloatValue : ConditionValue
{
    public float Value;
}

public class BoolValue : ConditionValue
{
    public bool Value;
}

// Conditions
public abstract class ConditionObject<T> where T : ConditionValue
{
    public string Id;
    public T ConditionValue;

    public abstract bool Assert();
}

public class FloatEqualTo : ConditionObject<FloatValue>
{
    public float Expected;
    public FloatEqualTo(string id, float expected, float initial = 0)
    {
        Id = id;
        Expected = expected;

        ConditionValue = new FloatValue()
        {
            Id = id,
            Value = initial
        };
    }

    public override bool Assert() => ConditionValue.Value == Expected;
}

public class FloatGreaterThan : ConditionObject<FloatValue>
{
    public float Expected;
    public FloatGreaterThan(string id, float expected, float initial = 0)
    {
        Id = id;
        Expected = expected;

        ConditionValue = new FloatValue()
        {
            Id = id,
            Value = initial
        };
    }

    public override bool Assert() => ConditionValue.Value > Expected;
}

public class BoolCondition : ConditionObject<BoolValue>
{
    public bool Expected;
    public BoolCondition(string id, bool expected, bool initial = false)
    {
        Id = id;
        Expected = expected;

        ConditionValue = new BoolValue()
        {
            Id = id,
            Value = initial
        };
    }

    public override bool Assert() => ConditionValue.Value == Expected;
}

[System.Serializable]
public class AnimGate
{
    public string playAnimation;
    public bool isTrigger;

    // You'd not be able to make a mixed list considering a list expects a particular type.
    public List<ConditionObject<FloatValue>> floatConditions;
    public List<ConditionObject<BoolValue>> boolConditions;

    public void setFloat(string query, float value)
    {
        if (floatConditions != null)
        {
            floatConditions.FirstOrDefault(condition => condition.Id == query)
                .ConditionValue.Value = value;
        }
    }

    public void setBool(string query, bool value)
    {
        if (boolConditions != null)
        { 
            boolConditions.FirstOrDefault(condition => condition.Id == query)
                .ConditionValue.Value = value;
        }
    }

    // All conditions MUST be true before this can apply.
    public bool isTruthy()
    {
        return floatConditions != null && floatConditions.All(x => x.Assert()) ||
            boolConditions != null && boolConditions.All(x => x.Assert());
    }
}

[RequireComponent(typeof(SpriteAnimator))]
public class AnimatorLogicManager : MonoBehaviour
{
    public SpriteAnimator SpriteAnimator;

    // NOTE: TEST A BUILD IF THIS WORKS, NEED TO KNOW IF SO's KEEP SPRITE REFS.
    // List of connecting gates for 'this' entity (will differ per entity I guess)
    private List<AnimGate> GatesForPlayerTest;

    public void SetFloat(string id, float value)
    {
        if (GatesForPlayerTest != null)
            GatesForPlayerTest.ForEach(x => x.setFloat(id, value));
    }

    public void SetBool(string id, bool value)
    {
        if (GatesForPlayerTest != null)
            GatesForPlayerTest.ForEach(x => x.setBool(id, value));
    }

    private void Start()
    {
        // Condition id's MUST match your 'set' calls, or nothing will change.
        // Thinking this could work well in a static script for each different 'thing'.
        GatesForPlayerTest = new List<AnimGate>() {
            new AnimGate()
            {
                playAnimation = "PlayerIdle", // TODO: const this
                isTrigger = false,
                floatConditions = new List<ConditionObject<FloatValue>>()
                {
                    new FloatEqualTo("playerMagnitude", 0) // TODO: const this
                }
            },
            new AnimGate()
            {
                playAnimation = "PlayerRun", // TODO: const this
                isTrigger = false,
                floatConditions = new List<ConditionObject<FloatValue>>()
                {
                    new FloatGreaterThan("playerMagnitude", 0) // TODO: const this
                }
            }
        };
    }

    private void Update()
    {
        AnimGate firstTruthyGate = GatesForPlayerTest.FirstOrDefault(x => x.isTruthy());

        if (firstTruthyGate != null)
            SpriteAnimator.PlayAnimation(firstTruthyGate.playAnimation);
    }
}
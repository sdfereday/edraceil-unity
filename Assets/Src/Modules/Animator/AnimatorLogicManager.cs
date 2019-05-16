using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New 2D Animation Object", menuName = "2D Animation Object", order = 51)]
public class AnimationObject : ScriptableObject
{
    public string Name;
    public Sprite[] Frames;
    public int FPS = 32;
    public bool Loops = false;
    public int FrameCount => Frames.Length;
    public float Duration => FrameCount * FPS;
    public float SecsPerFrame => 1f / FPS;

    public Sprite Play(int FrameNumber = 0)
    {
        return Frames[FrameNumber];
    }
}

[System.Serializable]
public class AnimatorLogic
{
    // We pass the whole data object so the animator stays unopinionated (may need revising, might be a bad idea yet)
    public AnimationObject AnimationData;
    public Dictionary<string, float> FloatValues;
    public Dictionary<string, bool> BoolValues;

    private void Start()
    {
        FloatValues = new Dictionary<string, float>();
        BoolValues = new Dictionary<string, bool>();
    }

    public void RegisterFloat(string floatName, float initialValue) => FloatValues.Add(floatName, initialValue);
    public void SetFloat(string floatName, float floatValue) => FloatValues[floatName] = floatValue;
    public bool FloatTruthy(string floatName, float compareTo) => FloatValues[floatName] == compareTo;
}

[RequireComponent(typeof(SpriteAnimator))]
public class AnimatorLogicManager : MonoBehaviour
{
    // NOTE: TEST A BUILD IF THIS WORKS, NEED TO KNOW IF SO's KEEP SPRITE REFS.
    /*
     - Feed in a float value (already registered at this point)
     - If any of them are truthy, or the first one that's truth, will be loaded in
     and passed to the animator.
     - Anything that effects the state of animation such as moving, etc will be able to directly
     access this manager. Could be done via an interface, but right now I don't care about that.
     */
    // The animator to pass the data to.
    public SpriteAnimator Animator;

    // Logic nodes that this manager uses (you could use different ways to make these, but we'll use inspector for now)
    public List<AnimatorLogic> LogicNodes;
}
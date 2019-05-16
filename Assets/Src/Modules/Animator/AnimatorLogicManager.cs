using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(SpriteAnimator))]
public class AnimatorLogicManager : MonoBehaviour
{
    // NOTE: TEST A BUILD IF THIS WORKS, NEED TO KNOW IF SO's KEEP SPRITE REFS.
    /*
     - Feed in values (external sources will just do this in their own way, easier)
     - If any of them are truthy, or the first one that's truth, will be loaded in
     and passed to the animator.
     - Anything that effects the state of animation such as moving, etc will be able to directly
     access this manager. Could be done via an interface, but right now I don't care about that.
     */
    
    // The animator to pass the data to.
    public SpriteAnimator Animator;

    // ... dicts not supported in editor
    [System.Serializable]
    public class FloatNode
    {
        public string Name;
        public AnimationObject AnimationData;
        public string WatchesProperty;
        public float ExpectedValue;

        // See below for explaination (this is just one node type of many)
        public bool Assert(float v) => ExpectedValue == v;
    }

    public List<FloatNode> FloatNodes;

    // List of floats or bools? Register them?
    public Dictionary<string, bool> BoolValues;
    public void AddBool(string boolName, bool initialValue) => BoolValues.Add(boolName, initialValue);
    public void SetBool(string boolName, bool toValue) => BoolValues[boolName] = toValue;

    public Dictionary<string, float> FloatValues;
    public void AddFloat(string floatName, float initialValue) => FloatValues.Add(floatName, initialValue);
    public void SetFloat(string floatName, float toValue) => FloatValues[floatName] = toValue;

    // setTrigger planned (this is for one-shot things that don't loop)
    // ...

    private void Update()
    {
        // This would work, but it
        var first = FloatNodes.FirstOrDefault(node => FloatValues[node.WatchesProperty] == node.ExpectedValue);

        // using floatValues, see if any nodes

        /*
        Think mechanim.

        You have created nodes that house animations. But they're linked by
        a transition between them. This could be a float, or bool, etc.

        If we were to hard-code this:
        AnimObject,
        Name (not required but useful for debugging),
        Transition ->
            Select a value type the transition uses
            Put in what it expects
            Also make it aware of what node to go to if this thing is true

        So you'll need a node that has an outbound transition to another node.

        And a transition back to the origin when it's done (if it's done).

        What do all these transitions have in common? They return a bool of yes
        or no if a condition has been met.
        This means that each transition can have a different method of producing
        that thing.

        Each node will be looped through, and knowing the property it's interested in,
        we get the value for that property and check against the expected.

        OR, for each node, whatever its function, 'get' the property and return
        true if it matches. 
         */
    }
}
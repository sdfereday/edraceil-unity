using UnityEngine;

public abstract class AnimCondition : MonoBehaviour
{
    public AnimationState Anim;
    public abstract bool Truthy();
}

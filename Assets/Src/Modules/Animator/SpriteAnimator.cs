using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;

    private int CurrentFrame;
    private AnimationObject CurrentAnim;
    private float NextFrameTime;
    
    private void Update()
    {
        if (Time.time < NextFrameTime || CurrentAnim == null) return;

        CurrentFrame += 1;

        if (CurrentFrame >= CurrentAnim.FrameCount)
        {
            if (!CurrentAnim.Loops)
            {
                // ... Pop back to the central decision state / exit out, as this was likely a trigger
                return;
            }

            CurrentFrame = 0;
        }

        SpriteRenderer.sprite = CurrentAnim.GetFrame(CurrentFrame);
        NextFrameTime += CurrentAnim.SecsPerFrame;
    }

    public void PlayAnimation(AnimationObject animationObject) => CurrentAnim = animationObject;
}

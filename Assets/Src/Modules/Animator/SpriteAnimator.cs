using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// Stick this on whatever object handles displaying animations
public class SpriteAnimator : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;

    private int CurrentFrame;
    private AnimationObject CurrentAnim;
    private float NextFrameTime;
    
    private void Update()
    {
        //CurrentAnim = Animations.FirstOrDefault(x => x.Truthy()).Anim;

        if (Time.time < NextFrameTime || CurrentAnim == null) return;

        CurrentFrame += 1;

        if (CurrentFrame >= CurrentAnim.FrameCount)
        {
            if (!CurrentAnim.Loops)
            {
                // ... Pop back to the central decision state
                return;
            }

            CurrentFrame = 0;
        }

        SpriteRenderer.sprite = CurrentAnim.Play(CurrentFrame);
        NextFrameTime += CurrentAnim.SecsPerFrame;
    }

    public void PlayAnimation(AnimationObject animationObject)
    {

    }
}

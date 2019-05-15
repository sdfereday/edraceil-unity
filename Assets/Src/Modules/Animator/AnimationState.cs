using UnityEngine;

[System.Serializable]
public class AnimationState
{
    public string Id;
    public MonoBehaviour Condition;
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

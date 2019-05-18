using UnityEngine;

public class StartsSaveGame : MonoBehaviour, IInteractible
{
    public Transform Transform
    {
        get
        {
            return transform;
        }
    }

    public INTERACTIBLE_TYPE InteractibleType
    {
        get
        {
            return INTERACTIBLE_TYPE.SAVE_THE_GAME;
        }
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        // ...
    }
}

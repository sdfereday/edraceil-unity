using UnityEngine;

public class IsMechanism : MonoBehaviour, IMechanism
{
    public ToggledSprite toggledSpriteComponent;

    private bool MechanismActive = false;
    private void UpdateSprite()
    {
        if (MechanismActive)
        {
            toggledSpriteComponent.On();
        }
        else
        {
            toggledSpriteComponent.Off();
        }
    }

    private void Start()
    {
        UpdateSprite();
    }

    public void Activate()
    {
        MechanismActive = true;
        UpdateSprite();
    }

    public void Deactivate()
    {
        MechanismActive = false;
        UpdateSprite();
    }
}

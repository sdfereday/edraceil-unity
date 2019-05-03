using UnityEngine;

public class IsMechanism : MonoBehaviour, IMechanism
{
    private ToggledSprite toggledSpriteComponent;
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
        toggledSpriteComponent = GetComponent<ToggledSprite>();
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

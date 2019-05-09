using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class IsMechanismTrigger : MonoBehaviour, IInteractible
{
    public ToggledSprite toggledSpriteComponent;
    public bool MechanismActive = false;
    public List<Transform> mechanisms;

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
            return INTERACTIBLE_TYPE.MECHANISM_TRIGGER;
        }
    }
    
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

    private void UpdateMechanisms()
    {
        mechanisms
            .Select(x => x.GetComponent<IMechanism>())
            .ToList()
            .ForEach(x => {
                if (MechanismActive)
                {
                    x.Activate();
                }
                else
                {
                    x.Deactivate();
                }
            });
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        mechanisms.ForEach(m => Gizmos.DrawLine(transform.position, m.position));
    }

    private void Start()
    {
        UpdateSprite();
        UpdateMechanisms();
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        MechanismActive = !MechanismActive;
        UpdateSprite();
        UpdateMechanisms();
    }
}

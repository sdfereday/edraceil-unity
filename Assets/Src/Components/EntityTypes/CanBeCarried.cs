using UnityEngine;

public class CanBeCarried : MonoBehaviour, IInteractible, ICarryable
{
    public bool CanInteract { get; private set; }
    public bool CanThrow { get { return true; } }
    public bool CanCarry { get; private set; }
    public int CurrentWeightValue { get { return 0; } }

    public Collider2D Collider;

    private void Start()
    {
        SetInteractible(true);
        SetCarryable(true);
    }

    public void SetInteractible(bool state)
    {
        CanInteract = state;
        Collider.enabled = state;
    }

    public void SetCarryable(bool state)
    {
        CanCarry = state;
    }

    public INTERACTIBLE_TYPE InteractibleType
    {
        get
        {
            return INTERACTIBLE_TYPE.TRANSPORTABLE;
        }
    }
    public Transform Use(Collider2D collider, INPUT_TYPE inputType)
    {
        return transform;
    }
}

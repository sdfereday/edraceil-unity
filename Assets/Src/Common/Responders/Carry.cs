using System;
using UnityEngine;

public class Carry : MonoBehaviour
{
    private ICarryable carryable;
    private Transform objectToCarry;
    private SortZ sortZ;
    private SpriteRenderer spr;

    public Throw throwAction;
    public Vector2 offset = Vector2.zero;
    public bool IsBeingCarried { get; set; }

    private void Awake()
    {
        sortZ = GetComponent<SortZ>();
        spr = GetComponent<SpriteRenderer>();
        throwAction = GetComponent<Throw>();
    }
    
    private void Update()
    {
        if (IsBeingCarried)
        {
            objectToCarry.position = new Vector2(transform.position.x + offset.x, transform.position.y + offset.y);
            // gameObject.transform.Translate (newpos.x, newpos.y, Time.deltaTime);
            // ^- Use if you want some sort of actual transition (perhaps pushing or pulling).
            if (throwAction != null)
                throwAction.Update();
        }
    }

    public void StartCarrying(INTERACTIBLE_TYPE originType, Transform _objectToCarry)
    {
        objectToCarry = _objectToCarry;
        carryable = objectToCarry.GetComponent<ICarryable>();

        if (IsBeingCarried)
        {
            StopCarrying();
            return;
        }

        // carryable.SetInteractible(false);
        IsBeingCarried = true;
    }

    public void StopCarrying()
    {
        // carryable.SetInteractible(true);
        objectToCarry.position = new Vector2(transform.position.x + offset.x, transform.position.y);
        IsBeingCarried = false;

        if (throwAction != null) {
            throwAction.StartThrow(transform);
        }
    }
}
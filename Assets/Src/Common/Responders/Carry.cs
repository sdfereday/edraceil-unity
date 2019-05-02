using System;
using UnityEngine;

public class Carry : MonoBehaviour
{
    private ICarryable carryable;
    private Transform objectToCarry;
    private SortZ sortZ;
    private SpriteRenderer spr;
    private bool beingCarried = false;

    // Throw the object if certain conditions met.
    public Throw throwAction;

    public bool isActive = false;

    public bool isBeingCarried
    {
        get
        {
            return beingCarried;
        }
    }
    public Vector2 offset = Vector2.zero;

    private void Awake()
    {
        sortZ = GetComponent<SortZ>();
        spr = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        if (beingCarried)
        {
            objectToCarry.position = new Vector2(transform.position.x + offset.x, transform.position.y + offset.y);
            // gameObject.transform.Translate (newpos.x, newpos.y, Time.deltaTime);
            // ^- Use if you want some sort of actual transition (perhaps pushing or pulling).

            // throwAction.Update();
        }
    }

    public void StartCarrying(INTERACTIBLE_TYPE originType, Transform origin, Action onDidCarry = null)
    {
        objectToCarry = origin;
        carryable = objectToCarry.GetComponent<ICarryable>();

        if (carryable == null)
        {
            return;
        }

        if (beingCarried)
        {
            StopCarrying();
            return;
        }

        // carryable.SetInteractible(false);
        beingCarried = true;
        isActive = true;

        if (onDidCarry != null)
            onDidCarry();
    }

    public void StopCarrying()
    {
        objectToCarry.position = new Vector2(transform.position.x + offset.x, transform.position.y);
        // carryable.SetInteractible(true);
        isActive = false;
        beingCarried = false;
    }

    public bool IsActive()
    {
        return isActive;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartsDialog : MonoBehaviour, IInteractible
{
    public INTERACTIBLE_TYPE InteractibleType {
        get {
            return INTERACTIBLE_TYPE.DIALOG;
        }
    }
    public Transform Use(Collider2D collider, INPUT_TYPE inputType)
    {
        return transform;
    }
}

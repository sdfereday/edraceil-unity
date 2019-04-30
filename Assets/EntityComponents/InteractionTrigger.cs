using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(ActionResponder))]
public class InteractionTrigger : MonoBehaviour
{
    [SerializeField]
    public Transform interactPosition;
    public float interactRadius;
    public LayerMask selectObjectsToHit;
    public ActionResponder actionResponder;

    private Collider2D RootCollider { get; set; }
    private Collider2D CurrentCollider { get; set; }

    private void Start()
    {
        RootCollider = transform.root.GetComponent<Collider2D>();
        CurrentCollider = GetComponent<Collider2D>();
    }

    private List<Collider2D> GetInteractees()
    {
        return Physics2D.OverlapCircleAll(interactPosition.position, interactRadius, selectObjectsToHit)
            .Where(x => x != CurrentCollider && x != RootCollider)
            .OrderBy(x => Vector2.Distance(x.transform.position, transform.position))
            .Where(x => x != null)
            .ToList();
    }

    private Collider2D GetClosestInteractee()
    {
        var interactees = GetInteractees();
        return interactees.Count() > 0 ? interactees[0] : null;
    }

    public void Interact(INPUT_TYPE originInputType)
    {
        // if (actionInProgress)... avoid doing anything further.
        var closestInteractee = GetClosestInteractee();
        var interactible = closestInteractee ? closestInteractee.GetComponent<IInteractible>() : null;

        if (interactible != null)
        {
            // Perform related actions on player side (trigger carrying, etc)
            actionResponder.Act(interactible.InteractibleType);

            // Perform related actions on interactible's side (trigger anims, data changes, etc)
            interactible.Use(RootCollider, originInputType);
        }
    }
}
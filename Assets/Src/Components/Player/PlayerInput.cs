using UnityEngine;

// See: https://bitbucket.org/drunkenoodle/rr-clone/src for original (and battle ideas).
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InteractionTrigger))]
public class PlayerInput : MonoBehaviour
{
    public float maxSpeed = 3f;
    public bool InteractionsEnabled { get; private set; }
    public bool MovementEnabled { get; private set; }
    public Vector2 Facing { get; set; }
    public Vector2 CurrentVelocity
    {
        get
        {
            return rbody.velocity;
        }
    }

    private Rigidbody2D rbody;
    private InteractionTrigger interactionTrigger;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        interactionTrigger = GetComponentInChildren<InteractionTrigger>();
        Facing = Vector2.right;

        InteractionsEnabled = true;
        MovementEnabled = true;
    }

    private void Update()
    {
        rbody.velocity = Vector2.zero;

        if (InteractionsEnabled)
            Interactions();

        if (MovementEnabled)
            Movement();
    }

    private void Interactions()
    {
        if (Input.GetKeyDown(KeyCodeConsts.USE))
        {
            interactionTrigger.Interact(INPUT_TYPE.USE);
        }

        if (Input.GetKeyDown(KeyCodeConsts.CANCEL))
        {
            interactionTrigger.Interact(INPUT_TYPE.CANCEL);
        }
    }

    private void Movement()
    {
        float xAxis = Input.GetAxisRaw(KeyCodeConsts.Horizontal);
        float yAxis = Input.GetAxisRaw(KeyCodeConsts.Vertical);

        if (xAxis != 0 || yAxis != 0)
        {
            Vector2 nm = new Vector2(xAxis * maxSpeed, yAxis * maxSpeed).normalized;
            rbody.velocity = nm * maxSpeed;
            Facing = CurrentVelocity.normalized;
        }
    }

    public void ToggleInteractions(bool state)
    {
        InteractionsEnabled = state;
    }

    public void ToggleMovement(bool state)
    {
        MovementEnabled = state;
    }
}
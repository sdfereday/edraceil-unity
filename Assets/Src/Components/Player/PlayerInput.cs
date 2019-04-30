using UnityEngine;

// See: https://bitbucket.org/drunkenoodle/rr-clone/src for original (and battle ideas).
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InteractionTrigger))]
public class PlayerInput : MonoBehaviour
{
    public float maxSpeed = 3f;
    public Vector2 facing { get; set; }
    public Vector2 currentVelocity
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
        facing = Vector2.right;
    }

    private void Update()
    {
        rbody.velocity = Vector2.zero;

        if (Input.GetKeyDown(KeyCodeConsts.USE))
        {
            interactionTrigger.Interact(INPUT_TYPE.USE);
        }

        if (Input.GetKeyDown(KeyCodeConsts.CANCEL))
        {
            interactionTrigger.Interact(INPUT_TYPE.CANCEL);
        }

        float xAxis = Input.GetAxisRaw(KeyCodeConsts.Horizontal);
        float yAxis = Input.GetAxisRaw(KeyCodeConsts.Vertical);

        if (xAxis != 0 || yAxis != 0)
        {
            Vector2 nm = new Vector2(xAxis * maxSpeed, yAxis * maxSpeed).normalized;
            rbody.velocity = nm * maxSpeed;
            facing = currentVelocity.normalized;
        }
    }
}
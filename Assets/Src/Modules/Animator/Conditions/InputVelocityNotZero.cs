public class InputVelocityNotZero : AnimCondition
{
    public PlayerInput PlayerInput;
    public override bool Truthy() => PlayerInput.CurrentVelocity.magnitude != 0;
}
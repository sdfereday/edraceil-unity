using UnityEngine;

/* Attach this to an entity that you wish to keep
 * recorded for its state permantly. Please
 * stick to naming conventions in directories
 * so you know which scene it belongs in. */
public class SaveState : MonoBehaviour
{
    [SerializeField]
    private BoolSaveState _BooleanObject;
    public bool IsTruthy => _BooleanObject.State;

    public void UpdateBoolState(bool _state)
    {
        _BooleanObject.State = _state;
    }
}

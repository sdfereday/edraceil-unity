using UnityEngine;

[CreateAssetMenu(fileName = "New Character Object", menuName = "Character Object", order = 51)]
public class CharacterObject : ScriptableObject
{
    public string Id;
    public string Name;

    [TextArea]
    public string _CharacterMeta;
    public string CharacterMeta => _CharacterMeta;
}

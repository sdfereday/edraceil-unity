using UnityEngine;

namespace RedPanda.Objects.Props
{
    [CreateAssetMenu(fileName = "New Scene Prop", menuName = "Scene Prop", order = 51)]
    public class SceneProp : ScriptableObject
    {
        public string Id;

        public GameObject _GraphicalPrefab;
        public GameObject GraphicalPrefab => _GraphicalPrefab;
    }
}
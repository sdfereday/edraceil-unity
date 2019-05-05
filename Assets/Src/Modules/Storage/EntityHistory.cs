using UnityEngine;
using System.Linq;
using System.Collections.Generic;

// TODO: Make use of a helper string thing as below.
using UnityEngine.SceneManagement;
// using BayatGames.SaveGameFree;

public class EntityHistory : MonoBehaviour
{
    [System.Serializable]
    public class RegisteredSpawner
    {
        public string Id;
        public string InScene;
    }
    public List<RegisteredSpawner> Entities { get; private set; }

    private void Awake()
    {
        // TODO: You'd usually be loading this from disk / cloud.
        Entities = new List<RegisteredSpawner>();
    }

    private void OnEnable()
    {
        SaveGame.OnSaveSignal += SaveToDisk;
    }


    private void OnDisable()
    {
        SaveGame.OnSaveSignal -= SaveToDisk;
    }

    public void SaveToDisk()
    {
        // Save spawn state to disk.
        Debug.Log(gameObject.name + " got save to disk signal!");
        Entities.ForEach(x => Debug.Log(x.Id));
    }

    public void LogUsed(string _id)
    {
        Entities.Add(new RegisteredSpawner()
        {
            Id = _id,
            InScene = SceneManager.GetActiveScene().name
        });
    }

    public bool WasUsed(string _id)
    {
        return Entities.Any(item => item.Id == _id);
    }
}

using UnityEngine;
using System.Linq;
using System.Collections.Generic;

// TODO: Make use of a helper string thing as below.
using UnityEngine.SceneManagement;
// using BayatGames.SaveGameFree;

public class KeyItemHistory : MonoBehaviour
{
    /*
     Things that need to be saved:
     - Items and qtys
     - Story / level progress (positions in, bosses defeated, etc)
     - Character custom name
     - Character location
     - Character current stats
     - Things picked up along the way / activated per scene
     */
    public List<RegisteredItem> UsedItems { get; private set; }

    private void Awake()
    {
        // TODO: You'd usually be loading this from disk / cloud.
        UsedItems = new List<RegisteredItem>();
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
        Debug.Log(gameObject.name + " got save to disk signal!");
        UsedItems.ForEach(x => Debug.Log(x.Id + " - at -> " + x.SceneName));
    }

    public void LogUsedItem(string _id)
    {
        if (HasUsedItem(_id))
        {
            throw new UnityException("It appears you have an item with that name already.");
        }

        UsedItems.Add(new RegisteredItem()
        {
            SceneName = SceneManager.GetActiveScene().name,
            Id = _id
        });
    }

    public bool HasUsedItem(string _id)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        return UsedItems.Any(item => item.Id == _id && item.SceneName == currentSceneName);
    }
}

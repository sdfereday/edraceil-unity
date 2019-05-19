using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class SceneContext : MonoBehaviour
{
    public List<BoolSaveState> MapEntityStates;

    [System.Serializable]
    public class BoolStateModel
    {
        public bool state;
        public string name;
    }

    [System.Serializable]
    public class SceneContextModel
    {
        public string sceneName;
        public List<BoolStateModel> boolStates;
    }

    private void Start()
    {
        // NOTES ON SAVING HERE
        // Why am I using a model? Because it means things won't try to be instantiated.
        // I just want values!
        // Then again, do I even care about saving these things? I mean, aren't they
        // data objects? I will need to think of something, because right now they're shared by all
        // save instances. But I'll figure it out. I might not even use SO's for this stuff and
        // just generate a JSON structure for each scene instead. Will decide later, could be less
        // complicated that way anyway.
        MapEntityStates.ForEach(x => x.State = false);

        var loadedSceneState = SaveDataManager.LoadData<List<SceneContextModel>>(DataConsts.LEVEL_DATA_FILE)
            .Where(x => x.sceneName == SceneManager.GetActiveScene().name)
            .FirstOrDefault();
        
        if (loadedSceneState != null)
        {
            loadedSceneState.boolStates.ForEach(x => {
                var boolState = MapEntityStates.FirstOrDefault(ent => ent.name == x.name);

                if (boolState != null)
                {
                    boolState.State = x.state;
                }
            });
        }
    }

    private void OnEnable()
    {
        SaveGame.OnSaveSignal += SaveToDisk;
    }

    private void OnDisable()
    {
        SaveGame.OnSaveSignal -= SaveToDisk;
    }

    private void SaveToDisk()
    {
        var payload = new SceneContextModel()
        {
            sceneName = SceneManager.GetActiveScene().name,
            boolStates = MapEntityStates.Select(x => {
                return new BoolStateModel()
                {
                    name = x.name,
                    state = x.State
                };
            }).ToList()
        };

        // TODO: This will overwrite any existing that exist. This needs fixing. Perhaps before you go ahead and
        // send a new list, find the 'node' instead, and edit that. Just check the docs, see what the craic is.
        SaveDataManager.SaveData(new List<SceneContextModel>() { payload }, DataConsts.LEVEL_DATA_FILE);

        // Test payload
        var payload2 = new SceneContextModel()
        {
            sceneName = SceneManager.GetActiveScene().name,
            boolStates = MapEntityStates.Select(x => {
                return new BoolStateModel()
                {
                    name = x.name + "rammbooooo",
                    state = x.State
                };
            }).ToList()
        };

        // Tests see that this overwrites. We need to compile it all together.
        SaveDataManager.SaveData(new List<SceneContextModel>() { payload2 }, DataConsts.LEVEL_DATA_FILE);

        // Like so (somehow, either write to a new file, or, get what's already there and stick it on the end):
        SaveDataManager.SaveData(new List<SceneContextModel>() { payload, payload2 }, DataConsts.LEVEL_DATA_FILE);
    }
}

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SceneContext : MonoBehaviour
{
    // Drag everything to this list that you want to keep track of state-wise.
    public string SceneName;
    public List<BoolSaveState> MapEntityStates;

    [System.Serializable]
    public class BooStateModel
    {
        public bool State;
        public string name;
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

        var loadedBoolStates = SaveDataManager.LoadData<List<BooStateModel>>(DataConsts.LEVEL_DATA_FILE);
        
        if (loadedBoolStates != null)
        {
            loadedBoolStates.ForEach(x => {
                var boolState = MapEntityStates.FirstOrDefault(ent => ent.name == x.name);

                if (boolState != null)
                {
                    boolState.State = x.State;
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
        SaveDataManager.SaveData(MapEntityStates, DataConsts.LEVEL_DATA_FILE);
    }
}

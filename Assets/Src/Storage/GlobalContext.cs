using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GlobalContext : MonoBehaviour
{
    public List<SceneContextModel> LoadedScenes;

    private void Awake()
    {
        List<SceneContextModel> loadedData = SaveDataManager.LoadData<List<SceneContextModel>>(DataConsts.SCENE_DATA_FILE);
        bool hasDuplicates = loadedData.GroupBy(x => x.sceneName)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .Count() > 0;

        if (hasDuplicates)
        {
            throw new System.Exception(ErrorConsts.DUPLICATE_SCENE_MODEL);
        }

        LoadedScenes = loadedData;
    }

    public SceneContextModel LoadSceneData(string sceneName)
    {
        return LoadedScenes
            .Where(x => x.sceneName == sceneName)
            .FirstOrDefault();
    }

    public void UpdateSceneData(SceneContextModel sceneModel)
    {
        var existing = LoadedScenes.Where(x => x.sceneName == sceneModel.sceneName)
            .FirstOrDefault();

        if (existing == null)
        {
            LoadedScenes.Add(existing);
        } else
        {
            existing.boolStates = sceneModel.boolStates;
        }

        SaveDataManager.SaveData(LoadedScenes, DataConsts.SCENE_DATA_FILE);
    }
}

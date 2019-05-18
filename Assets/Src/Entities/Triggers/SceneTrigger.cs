using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public string SceneName;
    public ChangeScene SceneChanger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO: Const this tag.
        if (collision.transform.CompareTag("PlayerInteractor"))
        {
            SceneChanger.DoTransition(ChangeScene.FadeDirection.In, SceneName);
        }
    }
}

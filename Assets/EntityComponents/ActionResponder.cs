using UnityEngine;
using System.Collections.Generic;

public class ActionResponder : MonoBehaviour
{
    [System.Serializable]
    public class Action
    {
        public string id;
        public INTERACTIBLE_TYPE type;
        public Carry carryComponent;
        public Talk talkComponent;

        public void Activate()
        {
            if (carryComponent != null)
                carryComponent.StartCarrying();

            if (talkComponent != null)
                talkComponent.StartTalking();
        }
    }

    public List<Action> ActionList;

    public void Act(INTERACTIBLE_TYPE type, string id = null)
    {
        ActionList.Find(x => id != null ? x.id == id : x.type == type)
            .Activate();
    }
}

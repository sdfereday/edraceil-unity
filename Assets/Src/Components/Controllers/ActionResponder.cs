using UnityEngine;
using System.Collections.Generic;

public class ActionResponder : MonoBehaviour
{
    [System.Serializable]
    public class Action
    {
        public string id;
        public INTERACTIBLE_TYPE type;
        public TestAction testAction;
        public Carry carryAction;
        public Talk talkAction;

        public void Activate(INTERACTIBLE_TYPE originType, Transform originTransform)
        {
            if (testAction != null)
                testAction.StartTesting(originType, originTransform);

            if (carryAction != null)
                carryAction.StartCarrying(originType, originTransform);

            if (talkAction != null)
                talkAction.StartTalking(originType, originTransform);
        }
    }

    public List<Action> ActionList;

    public void Act(INTERACTIBLE_TYPE originType, Transform originTransform)
    {
        var matchedAction = ActionList.Find(x => x.type == originType);

        if (matchedAction != null)
            matchedAction.Activate(originType, originTransform);
    }
}

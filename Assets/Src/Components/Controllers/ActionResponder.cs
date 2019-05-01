using UnityEngine;
using System;
using System.Collections.Generic;
public class ActionResponder : MonoBehaviour
{
    [System.Serializable]
    public class Response
    {
        public string id;
        public INTERACTIBLE_TYPE type;
        public TestAction testResponse;
        public Carry carryResponse;
        public Throw throwResponse;
        public Talk talkResponse;

        public void Activate(INTERACTIBLE_TYPE originType, Transform originTransform, Action onActivated = null)
        {
            // TODO: It might actually be best to only allow for one type of response to happen, things
            // could get messy with multiple callbacks for example. I'd also use an interface / component.
            // TODO: Rather than a deactivate, consider using the interactible type to decide what to do, or
            // perhaps even the input type.
            if (testResponse != null) {
                testResponse.StartTesting(originType, originTransform);
            }

            if (talkResponse != null) {
                talkResponse.StartTalking(originType, originTransform);
            }

            if (carryResponse != null) {
                carryResponse.StartCarrying(originTransform, onActivated); // hmm... not a fan of this idea.
            }
        }

        public void Deactivate(INTERACTIBLE_TYPE originType, Transform originTransform)
        {
            if (carryResponse != null)
                carryResponse.StopCarrying();

            if (throwResponse != null)
                throwResponse.StartThrow(originTransform);
        }

        public void Update()
        {
            if (throwResponse != null)
                throwResponse.Update();
        }
    }

    private bool ActionsInProgress { get; set; }

    private void Update()
    {
        ResponseList.ForEach(x => x.Update());
    }

    public List<Response> ResponseList;

    public void Act(INTERACTIBLE_TYPE originType, Transform originTransform)
    {
        var matchedResponse = ResponseList.Find(x => x.type == originType);

        if (ActionsInProgress)
        {
            if (matchedResponse != null)
            {
                matchedResponse.Deactivate(originType, originTransform);
            }

            ActionsInProgress = false;
            return;
        }

        if (matchedResponse != null)
            matchedResponse.Activate(originType, originTransform, () =>
            {
                ActionsInProgress = true;
            });
    }
}

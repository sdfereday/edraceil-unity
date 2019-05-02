using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ActionResponder : MonoBehaviour
{
    [System.Serializable]
    public class Response
    {
        public string id;
        public INTERACTIBLE_TYPE type;
        public TestAction testResponse;
        public Carry carryResponse;
        public Talk talkResponse;
        public bool blockUntilDone = false;

        public bool isBusy = false;

        public void Activate(INTERACTIBLE_TYPE originType, Transform originTransform)
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
                isBusy = true; // Hmmmmmm
                carryResponse.StartCarrying(originType, originTransform); // hmm... onActivated
            }
        }

        public void Deactivate(INTERACTIBLE_TYPE originType, Transform originTransform)
        {
            if (carryResponse != null)
            {
                carryResponse.StopCarrying();
                isBusy = false;
            }
        }
    }

    public List<Response> ResponseList;

    public void Act(INTERACTIBLE_TYPE originType, Transform originTransform)
    {
        Debug.Log(originType);
        var matchedResponse = ResponseList.Find(x => x.type == originType);
        var responseMustFinish = ResponseList.Any(x => x.blockUntilDone && x.isBusy);

        if (matchedResponse == null)
            throw new UnityException("No matched response found.");

        if (responseMustFinish)
        {
            // matchedResponse.doSomethingToCheckIfItsFinished
            // Is a response still in progress or waiting to finish? If so, we don't fire any others yet.
            return;
        }
        
        matchedResponse.Activate(originType, originTransform);
    }
}

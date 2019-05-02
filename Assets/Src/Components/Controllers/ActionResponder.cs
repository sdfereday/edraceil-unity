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

        public bool BlockUntilDone { get; set; }

        public void Activate(INTERACTIBLE_TYPE originType, Transform originTransform)
        {
            // TODO: It might actually be best to only allow for one type of response to happen, things
            // could get messy with multiple callbacks for example. I'd also use an interface / component.
            /* Block until done will only be set here if the 'type' of response allows it to do so. This
             could be done with an interface for example to make things a little more generic and not specific
             to the different items in this list. */
            if (testResponse != null) {
                testResponse.StartTesting(originType, originTransform);
            }

            if (talkResponse != null) {
                talkResponse.StartTalking(originType, originTransform);
            }

            if (carryResponse != null) {
                BlockUntilDone = true;
                carryResponse.StartCarrying(originType, originTransform);
            }
        }

        public void Nudge(INTERACTIBLE_TYPE originType, Transform originTransform)
        {
            if (carryResponse != null)
            {
                BlockUntilDone = false;
                carryResponse.StopCarrying();
            }
        }
    }

    public List<Response> ResponseList;

    public void Act(INTERACTIBLE_TYPE originType, Transform originTransform)
    {
        var matchedResponse = ResponseList.Find(x => x.type == originType);
        var responseMustFinish = ResponseList.Any(x => x.BlockUntilDone);

        if (matchedResponse == null)
            throw new UnityException("No matched response found.");

        if (responseMustFinish)
        {
            // Is a response still in progress or waiting to finish? If so, we don't fire any others yet.
            // Response can mark self as done or it can just cancel on input again if it's a toggle sort,
            // we manually nudge the response just in case it requires a secondary input to cancel.
            matchedResponse.Nudge(originType, originTransform);
            return;
        }
        
        matchedResponse.Activate(originType, originTransform);
    }
}

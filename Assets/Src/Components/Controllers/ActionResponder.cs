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
        public Collect collectResponse;

        public List<IResponseTask> ResponseTasks { get; set; }
        
        public void Init()
        {
            ResponseTasks = new List<IResponseTask>();

            if (testResponse != null)
            {
                ResponseTasks.Add(testResponse.GetComponent<IResponseTask>());
            }

            if (talkResponse != null)
            {
                ResponseTasks.Add(talkResponse.GetComponent<IResponseTask>());
            }

            if (carryResponse != null)
            {
                ResponseTasks.Add(carryResponse.GetComponent<IResponseTask>());
            }

            if (collectResponse != null)
            {
                ResponseTasks.Add(collectResponse.GetComponent<IResponseTask>());
            }
        }

        public void Activate(INTERACTIBLE_TYPE originType, Transform originTransform)
        {
            ResponseTasks.ForEach(x => x.Run(originType, originTransform));
        }

        public void Update(INTERACTIBLE_TYPE originType, Transform originTransform)
        {
            ResponseTasks
                .Where(x => x.IsActive && x.ResponseType == RESPONSE_TYPE.CONTINUOUS)
                .ToList()
                .ForEach(x => x.Next());

            ResponseTasks
                .Where(x => x.IsActive && x.ResponseType == RESPONSE_TYPE.TOGGLE)
                .ToList()
                .ForEach(x => x.Complete());
        }

        public bool ContainsUnfinished()
        {
            return ResponseTasks.Any(x => x.IsActive);
        }
    }

    public List<Response> ResponseList;
    public bool ResponseMustFinish { get; private set; }

    private void Start()
    {
        ResponseList.ForEach(r => r.Init());
    }

    public void Act(INTERACTIBLE_TYPE originType, Transform originTransform)
    {
        var matchedResponse = ResponseList.Find(r => r.type == originType);
        ResponseMustFinish = ResponseList.Any(r => r.ContainsUnfinished());

        if (matchedResponse == null)
            return;

        if (ResponseMustFinish)
        {
            matchedResponse.Update(originType, originTransform);
        } else
        {
            matchedResponse.Activate(originType, originTransform);
        }
    }
}

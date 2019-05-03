﻿using UnityEngine;

public interface IResponseTask
{
    RESPONSE_TYPE ResponseType { get; }
    bool IsActive { get; }
    void Run(INTERACTIBLE_TYPE originType, Transform originTransform);
    void Next();
    void Complete();
}
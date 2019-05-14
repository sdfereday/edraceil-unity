﻿using UnityEngine;

/*
 Create a new one of these for each entity you'd like to save
 the state for. This should in turn save this permanantely. If you
 want even more, just gather up all the states on save game
 and store them via a glbal state. You can always re-apply things later on
 upon load. */
[CreateAssetMenu(fileName = "New Boolean Save Object", menuName = "Boolean Save Object", order = 51)]
public class BoolSaveState : ScriptableObject
{
    public bool State;
}
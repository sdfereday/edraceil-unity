﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(SpriteAnimator))]
public class AnimatorLogicManager : MonoBehaviour
{
    public CharacterObject CharacterData;
    public SpriteAnimator SpriteAnimator;
    private List<AnimationGate> AnimationGateData;

    private void Awake()
    {
        // TODO: Move this out of here eventually (worth splitting json files out also?)
        // PLUS, don't store this in the user directory. It belongs to the games files, it
        // might even be worth making it in to a binary eventually.
        AnimationGateData = new List<AnimationGate>();

        var loadedGates = SaveDataManager.LoadAssetData<List<GateModelCollection>>(DataConsts.ANIMATION_LOGIC_FILE)
            .Where(x => x.targetEntity == CharacterData.Id)
            .FirstOrDefault().gates;
            
        // Attempt to reconstruct gate data using this new instanced data
        AnimationGateData = loadedGates.Select(gate =>
        {
            // This is where it gets rather nasty. Compare via switch to see what we need to bind. It's also nasty
            // IMO because I have to cast it as a condition object, then convert it all to a list.
            List<ConditionObject<float>> floatConditions = gate.floatConditions != null ? gate.floatConditions.Select(f =>
            {
                return new FloatCondition(f.Id, f.Expected, f.Value, f.LogicMethod) as ConditionObject<float>;
            }).ToList() : new List<ConditionObject<float>>();

            List<ConditionObject<bool>> boolConditions = gate.boolConditions != null ? gate.boolConditions.Select(b =>
            {
                return new BoolCondition(b.Id, b.Expected, b.Value, b.LogicMethod) as ConditionObject<bool>;
            }).ToList() : new List<ConditionObject<bool>>();

            // Nasty stuff ended, we just return a new animation gate.
            return new AnimationGate()
            {
                playAnimation = gate.playAnimation,
                floatConditions = floatConditions,
                boolConditions = boolConditions
            };
        }).ToList();
    }

    public void SetFloat(string id, float value)
    {
        if (AnimationGateData != null)
            AnimationGateData.ForEach(x => x.SetFloat(id, value));
    }

    public void SetBool(string id, bool value)
    {
        if (AnimationGateData != null)
            AnimationGateData.ForEach(x => x.SetBool(id, value));
    }

    private void Update()
    {
        if (AnimationGateData == null)
            return;

        AnimationGate firstTruthyGate = AnimationGateData
            .Where(x => x.IsTruthy())
            .FirstOrDefault();

        if (firstTruthyGate != null)
            SpriteAnimator.PlayAnimation(firstTruthyGate.playAnimation);
    }
}
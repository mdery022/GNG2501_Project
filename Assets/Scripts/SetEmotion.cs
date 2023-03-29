using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEmotion : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer meshRenderer;

    [SerializeField]
    private Emotion emotion;

    float gradiantStep = 1f;
    int eyebrowBrowUpIndex, eyebrowDownIndex, eyesClosedIndex, mouthClenchIndex, mouthArchDown, eyebrowArchDown, mouthPucker;

    // Start is called before the first frame update
    void Start()
    {
        eyebrowBrowUpIndex = meshRenderer.sharedMesh.GetBlendShapeIndex("Eyebrow_Arch_Up");
        eyebrowDownIndex = meshRenderer.sharedMesh.GetBlendShapeIndex("Eyebrow_Down");
        eyebrowArchDown = meshRenderer.sharedMesh.GetBlendShapeIndex("Eyebrow.Arch_Down.B");
        eyesClosedIndex = meshRenderer.sharedMesh.GetBlendShapeIndex("Eyes_Closed.B");
        mouthClenchIndex = meshRenderer.sharedMesh.GetBlendShapeIndex("Mouth_Clench");
        mouthArchDown = meshRenderer.sharedMesh.GetBlendShapeIndex("Mouth_Arch.Down");
        mouthPucker = meshRenderer.sharedMesh.GetBlendShapeIndex("Mouth_Pucker");
    }

    // Update is called once per frame
    void Update()
    {
        switch (emotion)
        {
            case Emotion.Angry:
                setBlendShapeWeightGradual(eyebrowArchDown, 0);
                setBlendShapeWeightGradual(mouthPucker, 0);
                setBlendShapeWeightGradual(eyebrowBrowUpIndex, 85);
                setBlendShapeWeightGradual(eyebrowDownIndex, 85);
                setBlendShapeWeightGradual(eyesClosedIndex, 10);
                setBlendShapeWeightGradual(mouthClenchIndex, 80);
                setBlendShapeWeightGradual(mouthArchDown, 45);
                break;
            case Emotion.Displeased:
                setBlendShapeWeightGradual(mouthClenchIndex, 0);
                setBlendShapeWeightGradual(eyebrowArchDown, 0);
                setBlendShapeWeightGradual(mouthPucker, 0);
                setBlendShapeWeightGradual(eyebrowBrowUpIndex, 85);
                setBlendShapeWeightGradual(eyebrowDownIndex, 65);
                setBlendShapeWeightGradual(eyesClosedIndex, 10);
                setBlendShapeWeightGradual(mouthArchDown, 50);
                break;
            case Emotion.Worry:
                setBlendShapeWeightGradual(eyebrowBrowUpIndex, 0);
                setBlendShapeWeightGradual(eyesClosedIndex, 0);
                setBlendShapeWeightGradual(mouthClenchIndex, 0);
                setBlendShapeWeightGradual(mouthPucker, 10);
                setBlendShapeWeightGradual(eyebrowArchDown, 35);
                setBlendShapeWeightGradual(eyebrowDownIndex, 10);
                setBlendShapeWeightGradual(mouthArchDown, 50);
                break;
            default:
                setBlendShapeWeightGradual(eyebrowBrowUpIndex, 0);
                setBlendShapeWeightGradual(eyebrowDownIndex, 0);
                setBlendShapeWeightGradual(eyesClosedIndex, 0);
                setBlendShapeWeightGradual(mouthClenchIndex, 0);
                setBlendShapeWeightGradual(mouthArchDown, 0);
                setBlendShapeWeightGradual(eyebrowArchDown, 0);
                setBlendShapeWeightGradual(mouthPucker, 0);
                break;
        }
    }
    private void setBlendShapeWeightGradual(int blendShapeIndex, float targetValue)
    {
        float currentValue = meshRenderer.GetBlendShapeWeight(blendShapeIndex);

        if (Math.Abs(currentValue - targetValue) > gradiantStep)
        {
            if (currentValue < targetValue)
                currentValue += gradiantStep;
            else
                currentValue -= gradiantStep;

            meshRenderer.SetBlendShapeWeight(blendShapeIndex, currentValue);
        }
    }

    private enum Emotion
    {
        Angry,
        Displeased,
        Worry,
        None
    }
}
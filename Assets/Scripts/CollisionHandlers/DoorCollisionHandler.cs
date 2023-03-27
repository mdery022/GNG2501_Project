using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject mother;
    private Animator motherAnimator;

    [SerializeField]
    private GameObject father;
    private Animator fatherAnimator;

    [SerializeField]
    private TextMeshProUGUI subtitle;

    [SerializeField]
    private string colliderName;

    private EnterDiningRoomAnimationState state;
    private float timer;

    private void Start()
    {
        motherAnimator = mother.GetComponent<Animator>();
        fatherAnimator = father.GetComponent<Animator>();

        state = EnterDiningRoomAnimationState.NotEntered;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("DOOR INTERACTABLE TRIGGERED");

        if (other.name == colliderName && state == EnterDiningRoomAnimationState.NotEntered)
        {
            timer = 0f;
            state = EnterDiningRoomAnimationState.MotherTalking;
            subtitle.text = "[Mère] Te voilà, Zoé!";
            motherAnimator.SetInteger("State", 1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("DOOR INTERACTABLE TRIGGER STAY");

        if (other.name == colliderName && state == EnterDiningRoomAnimationState.NotEntered)
        {
            timer = 0f;
            state = EnterDiningRoomAnimationState.MotherTalking;
            subtitle.text = "[Mère] Te voilà, Zoé!";
            motherAnimator.SetInteger("State", 1);
        }
    }

    private void Update()
    {
        if (state == EnterDiningRoomAnimationState.MotherTalking)
        {
            timer += Time.deltaTime;
            if (timer >= 3f)
            {
                timer = 0;
                state = EnterDiningRoomAnimationState.FatherTalking;
                subtitle.text = "[Père] Assieds-toi.";
                motherAnimator.SetInteger("State", 0);
                fatherAnimator.SetInteger("State", 1);
            }
        }
        else if (state == EnterDiningRoomAnimationState.FatherTalking)
        {
            timer += Time.deltaTime;
            if (timer >= 3f)
            {
                state = EnterDiningRoomAnimationState.AnimationDone;
                subtitle.text = "";
                fatherAnimator.SetInteger("State", 0);
            }
        }
    }

    private enum EnterDiningRoomAnimationState
    {
        NotEntered,
        MotherTalking,
        FatherTalking,
        AnimationDone
    }
}

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

    [SerializeField]
    private AudioSource audioSource;

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
        if (other.name == colliderName && state == EnterDiningRoomAnimationState.NotEntered)
        {
            timer = 0f;
            state = EnterDiningRoomAnimationState.MotherTalking;
            subtitle.text = "[M�re] Tu es rentr�!";
            motherAnimator.SetInteger("State", 1);
            audioSource.Play();
        }
    }

    private void Update()
    {
        if (state == EnterDiningRoomAnimationState.MotherTalking)
        {
            timer += Time.deltaTime;
            if (timer >= 1.5f)
            {
                timer = 0;
                state = EnterDiningRoomAnimationState.FatherTalking;
                subtitle.text = "[P�re] Assieds-toi.";
                motherAnimator.SetInteger("State", 0);
                fatherAnimator.SetInteger("State", 1);
            }
        }
        else if (state == EnterDiningRoomAnimationState.FatherTalking)
        {
            timer += Time.deltaTime;
            if (timer >= 1.1f)
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

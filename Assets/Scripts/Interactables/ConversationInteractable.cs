using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConversationInteractable : Interactable
{
    [SerializeField]
    TextMeshProUGUI subtitle;

    [SerializeField]
    List<ConversationDetails> conversations;

    [SerializeField]
    AudioSource audioSource;

    Animator animator;

    float timer = 0;
    float end = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnInteract()
    {
        if (isInteractable)
        {
            ConversationDetails conversation = conversations[UnityEngine.Random.Range(0, conversations.Count)];

            isInteractable = false;
            timer = 0;
            end = conversation.Audio.length; ;
            subtitle.text = conversation.Conversation;
            animator.SetInteger("State", 1);
            audioSource.PlayOneShot(conversation.Audio);
        }
    }

    private void Update()
    {
        if (!isInteractable)
        {
            timer += Time.deltaTime;

            if (timer >= end)
            {
                isInteractable = true;
                subtitle.text = "";
                animator.SetInteger("State", 0);
            }
        }
    }

    [Serializable]
    public class ConversationDetails
    {
        public string Conversation;
        public AudioClip Audio;
    }
}

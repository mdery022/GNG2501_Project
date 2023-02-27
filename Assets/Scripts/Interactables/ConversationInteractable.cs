using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConversationInteractable : Interactable
{
    [SerializeField]
    TextMeshProUGUI subtitle;

    [SerializeField]
    List<string> conversations;

    Animator animator;

    float timer = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnInteract()
    {
        if (isInteractable)
        {
            isInteractable = false;
            timer = 0;
            subtitle.text = conversations[Random.Range(0, conversations.Count)];
            animator.SetInteger("State", 1);
        }
    }

    private void Update()
    {
        if (!isInteractable)
        {
            timer += Time.deltaTime;

            if (timer >= 3)
            {
                isInteractable = true;
                subtitle.text = "";
                animator.SetInteger("State", 0);
            }
        }
    }
}

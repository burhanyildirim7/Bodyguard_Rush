using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paparazi : MonoBehaviour
{
    public Animator animator;
    public GameObject emojiSad;
    void FixedUpdate()
    {
        if (this.tag == "SadPaparazi")
        {
            emojiSad.SetActive(true);

            animator.SetBool("isIdle", false);
            animator.SetBool("isHit", true);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hayran : MonoBehaviour
{
    public bool _unluyeKosma = false;
    public Animator animator;
    public GameObject emojiHappy, emojiSad;

    [SerializeField] private float _runSpeed = 5f;
    void FixedUpdate()
    {
        if (_unluyeKosma && this.tag == "ObstacleKosan")
        {
            emojiSad.SetActive(false);
            emojiHappy.SetActive(true);

            animator.SetBool("isIdle", false);
            animator.SetBool("isHit", false);
            animator.SetBool("isRun", true);

            transform.LookAt(GameObject.Find("MainCharacter").transform);
            transform.Translate(Vector3.forward * Time.deltaTime * _runSpeed);
        }
        if (this.tag == "SadHayran")
        {
            emojiHappy.SetActive(false);
            emojiSad.SetActive(true);

            animator.SetBool("isIdle", false);
            animator.SetBool("isRun", false);
            animator.SetBool("isHit", true);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BizimKarakter")
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isHit", false);
            animator.SetBool("isIdle", true);
        }
    }
}

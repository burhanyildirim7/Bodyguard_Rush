using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyguardManager : MonoBehaviour
{
    public Animator animator;
    public GameObject angryEmoji, coolEmoji;

    // Update is called once per frame
    void Update()
    {
        if (tag == "CalmBodyguard" && !coolEmoji.activeSelf)
        {
            angryEmoji.SetActive(false);
            coolEmoji.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ObstacleDuran"&&tag=="Bodyguard")
        {
            other.tag = "SadPaparazi";
            this.transform.parent = null;
            transform.LookAt(other.gameObject.transform);

            GetComponent<Animator>().SetBool("isIdle", false);
            GetComponent<Animator>().SetBool("isRun", false);
            GetComponent<Animator>().SetBool("isFall", false);
            GetComponent<Animator>().SetBool("isPunch", true);

            PlayerManager.Bodyguards[PlayerManager.Bodyguards.IndexOf(this.gameObject)] = null;
            this.tag = "CalmBodyguard";
        }

        if(other.tag== "ObstacleKosan" && tag == "Bodyguard")
        {
            other.tag = "SadHayran";
            this.transform.parent = null;
            transform.LookAt(other.gameObject.transform);

            GetComponent<Animator>().SetBool("isIdle", false);
            GetComponent<Animator>().SetBool("isRun", false);
            GetComponent<Animator>().SetBool("isFall", false);
            GetComponent<Animator>().SetBool("isPunch", true);

            PlayerManager.Bodyguards[PlayerManager.Bodyguards.IndexOf(this.gameObject)] = null;
            this.tag = "CalmBodyguard";
        }
        if(other.tag=="Obstacle" && tag == "Bodyguard")
        {
            this.transform.parent = null;
            PlayerManager.Bodyguards[PlayerManager.Bodyguards.IndexOf(this.gameObject)] = null;
            GetComponent<Animator>().SetBool("isIdle", false);
            GetComponent<Animator>().SetBool("isRun", false);
            GetComponent<Animator>().SetBool("isPunch", false);
            GetComponent<Animator>().SetBool("isFall", true);
        }
    }


}

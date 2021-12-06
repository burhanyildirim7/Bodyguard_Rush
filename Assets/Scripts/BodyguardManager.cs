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

        /*if (tag == "Attacker" && !angryEmoji.activeSelf)
        {
            coolEmoji.SetActive(false);
            angryEmoji.SetActive(true);
        }*/
        if (tag == "CalmBodyguard" && !coolEmoji.activeSelf)
        {
            angryEmoji.SetActive(false);
            coolEmoji.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ObstacleDuran")
        {
            other.tag = "SadPaparazi";
            this.transform.parent = null;
            transform.LookAt(other.gameObject.transform);
            PlayerManager.Bodyguards[PlayerManager.Bodyguards.IndexOf(this.gameObject)] = null;
            this.tag = "CalmBodyguard";
        }

        if(other.tag== "ObstacleKosan")
        {
            other.tag = "SadHayran";
            this.transform.parent = null;
            transform.LookAt(other.gameObject.transform);
            PlayerManager.Bodyguards[PlayerManager.Bodyguards.IndexOf(this.gameObject)] = null;
            this.tag = "CalmBodyguard";
        }



        if (other.tag == "Obstacle")
        {
            PlayerManager.Bodyguards[PlayerManager.Bodyguards.IndexOf(this.gameObject)] = null ;
            Destroy(this.gameObject);
        }
    }


}

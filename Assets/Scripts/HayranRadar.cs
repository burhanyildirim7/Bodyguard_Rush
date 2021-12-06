using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayranRadar : MonoBehaviour
{
    public GameObject hayran;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="BizimKarakter")
        {
            hayran.GetComponent<Hayran>()._unluyeKosma = true;
        }
    }
}

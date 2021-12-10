using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationButtons : MonoBehaviour
{
    PlayerManager playerManager;
    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("BizimKarakter").GetComponent<PlayerManager>();
    }
    public void FormationButtons_Ucgen()
    {
        playerManager.isTriangle = false;
        playerManager.isDikeyDikdortgen = false;
        playerManager.isHilal = false;
        playerManager.isDikdortgen = false;
        playerManager.isWifi = false;
        playerManager.isUcgen = true;

        playerManager.KapiUcgenFormasyonu();
    }
    public void FormationButton_DikeyDikdortgen()
    {
        playerManager.isTriangle = false;
        playerManager.isHilal = false;
        playerManager.isWifi = false;
        playerManager.isDikdortgen = false;
        playerManager.isUcgen = false;
        playerManager.isDikeyDikdortgen = true;

        playerManager.KapiDikeyDikdortgenFormasyonu();
    }

    public void FormationButton_Wifi()
    {
        playerManager.isTriangle = false;
        playerManager.isDikeyDikdortgen = false;
        playerManager.isHilal = false;
        playerManager.isDikdortgen = false;
        playerManager.isUcgen = false;
        playerManager.isWifi = true;

        playerManager.KapiWifiFormasyonu();
    }
   






    //Kullanýlmayan formasyonlar
    public void FormationButton_Dikdortgen()
    {
        playerManager.isTriangle = false;
        playerManager.isDikeyDikdortgen = false;
        playerManager.isHilal = false;
        playerManager.isWifi = false;
        playerManager.isUcgen = false;
        playerManager.isDikdortgen = true;

        playerManager.KapiDikdortgenFormasyonu();
    }
    public void FormationButton_Triangle()
    {
        playerManager.isDikeyDikdortgen = false;
        playerManager.isHilal = false;
        playerManager.isWifi = false;
        playerManager.isDikdortgen = false;
        playerManager.isUcgen = false;
        playerManager.isTriangle = true;

        playerManager.KapiTriangleFormasyonu();
    }

    public void FormationButton_Hilal()
    {
        playerManager.isTriangle = false;
        playerManager.isDikeyDikdortgen = false;
        playerManager.isWifi = false;
        playerManager.isDikdortgen = false;
        playerManager.isUcgen = false;
        playerManager.isHilal = true;

        playerManager.KapiHilalFormasyonu();
    }


}

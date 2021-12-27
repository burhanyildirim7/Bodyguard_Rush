using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static List<GameObject> Bodyguards;
    public static List<Vector3> BodyguardPositionList;
    public static int rowChecker_TriggerEnter = 0, rowCounter_TriggerEnter = 1;

    public GameObject dikeyDikdortgenFormation, wifiFormation, ucgenFormation;//Oyunda kullan?lan formasyonlar

    public GameObject rectangleFormation, triangleFormation, hilalFormation; //(!)Kullan?lmayan formasyonlar.

    public GameObject finishCameraTarget;

    public GameObject fxHavaiFisek1, fxHavaiFisek2;

    public bool isDikeyDikdortgen, isWifi, isUcgen; //Oyunda kullan?lan formasyonlar

    public bool isDikdortgen, isHilal, isTriangle; //(!)Kullan?lmayan formasyonlar.

    bool cameraDegis;

    bool nullControl;

    public UIController uiController;


    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("BizimKarakter");
        isDikeyDikdortgen = true;
        Bodyguards = new List<GameObject>();
        BodyguardPositionList = new List<Vector3>();
        nullControl = false;
        cameraDegis = false;
    }
    private void Update()
    {
        if (cameraDegis == true)
        {
            finishCameraTarget.transform.Translate(Vector3.up * Time.deltaTime * 8f);
        }
        if (GameController._oyunBekletFinish == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 5f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Unclaimed Bodyguard")
        {
            if (isDikeyDikdortgen)
            {
                BodyguardDikeyDikdortgenFormasyonu(other);
            }
            else if (isWifi)
            {
                BodyguardWifiFormasyonu(other);
            }
            else if (isUcgen)
            {
                BodyguardUcgenFormasyonu(other);
            }

            //Kullan?lmayanlar
            else if (isDikdortgen)
            {
                BodyguardDikdortgenFormasyonu(other);
            }
            else if (isTriangle)
            {
                BodyguardTriangleFormasyonu(other);
            }
            else if (isHilal)
            {
                BodyguardHilalFormasyonu(other);
            }



        }
        if (other.tag == "ObstacleKosan" || other.tag == "ObstacleDuran")
        {
            GameController._oyunAktif = false;

            player.GetComponent<Animator>().SetBool("isDance", false);
            player.GetComponent<Animator>().SetBool("isRun", false);
            player.GetComponent<Animator>().SetBool("isIdle", false);
            player.GetComponent<Animator>().SetBool("isCry", true);

            uiController.LevelSonuElmasSayisi(0);
            uiController.LoseScreenPanelOpen();
        }

        if (other.tag == "Obstacle")
        {
            GameController._oyunAktif = false;

            player.GetComponent<Animator>().SetBool("isDance", false);
            player.GetComponent<Animator>().SetBool("isRun", false);
            player.GetComponent<Animator>().SetBool("isIdle", false);
            player.GetComponent<Animator>().SetBool("isFall", true);

            uiController.LevelSonuElmasSayisi(0);
            uiController.LoseScreenPanelOpen();
        }


        if (other.tag == "DikeyDikdortgenKapisi")
        {
            isTriangle = false;
            isDikdortgen = false;
            isHilal = false;
            isWifi = false;
            isDikeyDikdortgen = true;
            KapiDikeyDikdortgenFormasyonu();
        }
        if (other.tag == "UcgenKapisi")
        {
            isDikdortgen = false;
            isDikeyDikdortgen = false;
            isHilal = false;
            isWifi = false;
            isTriangle = true;
            KapiTriangleFormasyonu();
        }
        if (other.tag == "DikdortgenKapisi")
        {
            isTriangle = false;
            isDikeyDikdortgen = false;
            isHilal = false;
            isWifi = false;
            isDikdortgen = true;
            KapiDikdortgenFormasyonu();
        }
        if (other.tag == "UKapisi")
        {
            isTriangle = false;
            isDikeyDikdortgen = false;
            isDikdortgen = false;
            isWifi = false;
            isHilal = true;
            KapiHilalFormasyonu();
        }
        if (other.tag == "TersUKapisi")
        {
            isTriangle = false;
            isDikeyDikdortgen = false;
            isDikdortgen = false;
            isHilal = false;
            isWifi = true;
            KapiWifiFormasyonu();
        }

        if (other.tag == "FinishLine")
        {
            GameController._oyunAktif = false;
            player.GetComponent<Animator>().SetBool("isRun", false);
            player.GetComponent<Animator>().SetBool("isIdle", true);
            StartCoroutine(bodyguardsFinish());
        }
        if (other.tag == "FinishDancePoint")
        {
            GameController._oyunBekletFinish = false;
            player.GetComponent<Animator>().SetBool("isIdle", false);
            player.GetComponent<Animator>().SetBool("isRun", false);
            player.GetComponent<Animator>().SetBool("isDance", true);
            fxHavaiFisek1.SetActive(true);
            fxHavaiFisek2.SetActive(true);
            uiController.LevelSonuElmasSayisi((int)(Bodyguards.Count * (Bodyguards.Count / 2.5f)));
            uiController.WinScreenPanelOpen();
        }
    }

    IEnumerator bodyguardsFinish()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0f, 1f, GameObject.FindGameObjectWithTag("Player").transform.position.z);
        Vector3 bodyguardsFinishLocation = new Vector3(0f, 0f, 10f);

        Bodyguards.RemoveAll(x => x == null);
        BodyguardPositionList.Clear();

        for (int i = 0; i < Bodyguards.Count; i++)
        {
            Bodyguards[i].GetComponent<Animator>().SetBool("isRun", false);
            Bodyguards[i].GetComponent<Animator>().SetBool("isIdle", true);
            if (i >= 25)
            {
                Destroy(Bodyguards[i].gameObject);
                Bodyguards[i] = null;
            }
        }
        Bodyguards.RemoveAll(x => x == null);


        cameraDegis = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>().Player = finishCameraTarget;

        for (int i = 0; i < Bodyguards.Count; i++)
        {
            Bodyguards[i].transform.localPosition = new Vector3(bodyguardsFinishLocation.x, (bodyguardsFinishLocation.y + (float)(i * 1.75)) - 1, bodyguardsFinishLocation.z);
            yield return new WaitForSeconds(0.2f);
        }
        Vector3 lastBodyguardPosition = Bodyguards[Bodyguards.Count - 1].transform.position;

        player.transform.position = new Vector3(lastBodyguardPosition.x, lastBodyguardPosition.y + 1.75f, lastBodyguardPosition.z);
        cameraDegis = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>().Player = player;

        if (Bodyguards.Count == 25) //Level sonu max bodyguard say?s?
        {
            player.GetComponent<Animator>().SetBool("isIdle", false);
            player.GetComponent<Animator>().SetBool("isDance", false);
            player.GetComponent<Animator>().SetBool("isRun", false);
            player.GetComponent<Animator>().SetBool("isHappy", true);

            player.transform.localRotation *= Quaternion.Euler(0, 180, 0);

            yield return new WaitForSeconds(1.85f); //Happy animasyonunun s?resi kadar bekletiliyor. Animasyon de?i?irse buras? da de?i?cek

            GameController._oyunBekletFinish = true;

            player.transform.localRotation *= Quaternion.Euler(0, 180, 0);


            player.GetComponent<Animator>().SetBool("isIdle", false);
            player.GetComponent<Animator>().SetBool("isDance", false);
            player.GetComponent<Animator>().SetBool("isHappy", false);
            player.GetComponent<Animator>().SetBool("isRun", true);
        }

        else
        {
            GameController._oyunBekletFinish = false;

            player.GetComponent<Animator>().SetBool("isIdle", false);
            player.GetComponent<Animator>().SetBool("isRun", false);
            player.GetComponent<Animator>().SetBool("isHappy", false);
            player.GetComponent<Animator>().SetBool("isDance", true);
            fxHavaiFisek1.SetActive(true);
            fxHavaiFisek2.SetActive(true);
            uiController.LevelSonuElmasSayisi((int)(Bodyguards.Count * (Bodyguards.Count / 2.5f)));
            uiController.WinScreenPanelOpen();
        }


    }

    public void KapiDikeyDikdortgenFormasyonu()
    {
        Bodyguards.RemoveAll(x => x == null);
        BodyguardPositionList.Clear();

        for (int i = 0; i < Bodyguards.Count; i++)
        {


            Bodyguards[i].transform.localPosition = new Vector3(dikeyDikdortgenFormation.GetComponent<FormationPoints>().formationPoints[i % 7].localPosition.x,
                            dikeyDikdortgenFormation.GetComponent<FormationPoints>().formationPoints[i % 7].localPosition.y,
                            dikeyDikdortgenFormation.GetComponent<FormationPoints>().formationPoints[i % 7].localPosition.z + i / 7);

            BodyguardPositionList.Add(Bodyguards[i].transform.localPosition);
        }
    }
    void BodyguardDikeyDikdortgenFormasyonu(Collider bodyguard)
    {
        int rowCount = 0;

        bodyguard.tag = "Bodyguard";
        bodyguard.GetComponent<Animator>().SetBool("isIdle", false);
        bodyguard.GetComponent<Animator>().SetBool("isRun", true);

        rowCount = Bodyguards.Count / 7;


        bodyguard.gameObject.transform.parent = GameObject.Find("Bodyguards").transform;

        bodyguard.gameObject.transform.localPosition = new Vector3(dikeyDikdortgenFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 7].localPosition.x,
                        dikeyDikdortgenFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 7].localPosition.y,
                        dikeyDikdortgenFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 7].localPosition.z + rowCount);

        for (int i = 0; i < Bodyguards.Count; i++)
        {
            if (Bodyguards[i] == null)
            {
                Bodyguards[i] = bodyguard.gameObject;
                bodyguard.gameObject.transform.localPosition = BodyguardPositionList[i];
                nullControl = true;
                break;
            }
        }

        if (nullControl == false)
        {
            Bodyguards.Add(bodyguard.gameObject);
            BodyguardPositionList.Add(bodyguard.gameObject.transform.localPosition);
        }
        nullControl = false;
    }
    public void KapiUcgenFormasyonu()
    {
        Bodyguards.RemoveAll(x => x == null);
        BodyguardPositionList.Clear();

        int rowChecker = 0;
        int rowCounter = 1;

        for (int i = 0; i < Bodyguards.Count; i++)
        {

            Bodyguards[i].transform.localPosition = new Vector3(ucgenFormation.GetComponent<FormationPoints>().formationPoints[i].localPosition.x,
                            ucgenFormation.GetComponent<FormationPoints>().formationPoints[i].localPosition.y,
                            ucgenFormation.GetComponent<FormationPoints>().formationPoints[i].localPosition.z);

            BodyguardPositionList.Add(Bodyguards[i].transform.localPosition);
        }

        for (int i = 0; i < Bodyguards.Count; i++)
        {
            if (i % (rowChecker + rowCounter) == 0 && i != 0)
            {
                for (int j = 0; j < i; j++)
                {
                    Bodyguards[j].transform.localPosition = new Vector3(Bodyguards[j].transform.localPosition.x,
                    Bodyguards[j].transform.localPosition.y,
                    Bodyguards[j].transform.localPosition.z + 1);
                }
                rowChecker += rowCounter;
                rowCounter += 2;
            }
        }
    }
    void BodyguardUcgenFormasyonu(Collider bodyguard)
    {
        bodyguard.tag = "Bodyguard";
        bodyguard.GetComponent<Animator>().SetBool("isIdle", false);
        bodyguard.GetComponent<Animator>().SetBool("isRun", true);



        bodyguard.gameObject.transform.parent = GameObject.Find("Bodyguards").transform;

        bodyguard.gameObject.transform.localPosition = new Vector3(ucgenFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count].localPosition.x,
                        ucgenFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count].localPosition.y,
                        ucgenFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count].localPosition.z);



        for (int i = 0; i < Bodyguards.Count; i++)
        {
            if (Bodyguards[i] == null)
            {
                Bodyguards[i] = bodyguard.gameObject;
                bodyguard.gameObject.transform.localPosition = BodyguardPositionList[i];
                nullControl = true;
                break;
            }
        }

        if (nullControl == false)
        {
            Bodyguards.Add(bodyguard.gameObject);
            BodyguardPositionList.Add(bodyguard.gameObject.transform.localPosition);

            if ((Bodyguards.Count - 1) % (rowChecker_TriggerEnter + rowCounter_TriggerEnter) == 0 && Bodyguards.Count > 1)
            {
                for (int i = 0; i < Bodyguards.Count - 1; i++)
                {
                    Bodyguards[i].transform.localPosition = new Vector3(Bodyguards[i].transform.localPosition.x,
                        Bodyguards[i].transform.localPosition.y,
                        Bodyguards[i].transform.localPosition.z + 1);
                }
                rowChecker_TriggerEnter += rowCounter_TriggerEnter;
                rowCounter_TriggerEnter += 2;
            }
        }
        nullControl = false;
    }
    public void KapiWifiFormasyonu()
    {

        Bodyguards.RemoveAll(x => x == null);
        BodyguardPositionList.Clear();

        for (int i = 0; i < Bodyguards.Count; i++)
        {
            Bodyguards[i].transform.localPosition = new Vector3(wifiFormation.GetComponent<FormationPoints>().formationPoints[i % 9].localPosition.x,
                            wifiFormation.GetComponent<FormationPoints>().formationPoints[i % 9].localPosition.y,
                            wifiFormation.GetComponent<FormationPoints>().formationPoints[i % 9].localPosition.z + i / 9);

            BodyguardPositionList.Add(Bodyguards[i].transform.localPosition);
        }
    }
    void BodyguardWifiFormasyonu(Collider bodyguard)
    {
        int rowCount = 0;

        bodyguard.tag = "Bodyguard";
        bodyguard.GetComponent<Animator>().SetBool("isIdle", false);
        bodyguard.GetComponent<Animator>().SetBool("isRun", true);

        bodyguard.gameObject.transform.parent = GameObject.Find("Bodyguards").transform;

        rowCount = Bodyguards.Count / 9;

        bodyguard.gameObject.transform.localPosition = new Vector3(wifiFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 9].localPosition.x,
                        wifiFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 9].localPosition.y,
                        wifiFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 9].localPosition.z + rowCount);

        for (int i = 0; i < Bodyguards.Count; i++)
        {
            if (Bodyguards[i] == null)
            {
                Bodyguards[i] = bodyguard.gameObject;
                bodyguard.gameObject.transform.localPosition = BodyguardPositionList[i];
                nullControl = true;
                break;
            }
        }

        if (nullControl == false)
        {
            Bodyguards.Add(bodyguard.gameObject);
            BodyguardPositionList.Add(bodyguard.gameObject.transform.localPosition);
        }
        nullControl = false;
    }





    //Kullan?lmayan formasyonlar
    public void KapiHilalFormasyonu()
    {

        Bodyguards.RemoveAll(x => x == null);
        BodyguardPositionList.Clear();

        for (int i = 0; i < Bodyguards.Count; i++)
        {
            Bodyguards[i].transform.localPosition = new Vector3(hilalFormation.GetComponent<FormationPoints>().formationPoints[i % 10].localPosition.x,
                            hilalFormation.GetComponent<FormationPoints>().formationPoints[i % 10].localPosition.y,
                            hilalFormation.GetComponent<FormationPoints>().formationPoints[i % 10].localPosition.z + i / 10);

            BodyguardPositionList.Add(Bodyguards[i].transform.localPosition);
        }
    }
    void BodyguardHilalFormasyonu(Collider bodyguard)
    {
        int rowCount = 0;

        bodyguard.tag = "Bodyguard";
        bodyguard.GetComponent<Animator>().SetBool("isIdle", false);
        bodyguard.GetComponent<Animator>().SetBool("isRun", true);

        bodyguard.gameObject.transform.parent = GameObject.Find("Bodyguards").transform;

        rowCount = Bodyguards.Count / 10;

        bodyguard.gameObject.transform.localPosition = new Vector3(hilalFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 10].localPosition.x,
                        hilalFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 10].localPosition.y,
                        hilalFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 10].localPosition.z + rowCount);

        for (int i = 0; i < Bodyguards.Count; i++)
        {
            if (Bodyguards[i] == null)
            {
                Bodyguards[i] = bodyguard.gameObject;
                bodyguard.gameObject.transform.localPosition = BodyguardPositionList[i];
                nullControl = true;
                break;
            }
        }

        if (nullControl == false)
        {
            Bodyguards.Add(bodyguard.gameObject);
            BodyguardPositionList.Add(bodyguard.gameObject.transform.localPosition);
        }
        nullControl = false;
    }

    public void KapiDikdortgenFormasyonu()
    {
        Bodyguards.RemoveAll(x => x == null);
        BodyguardPositionList.Clear();

        for (int i = 0; i < Bodyguards.Count; i++)
        {
            Bodyguards[i].transform.localPosition = new Vector3(rectangleFormation.GetComponent<FormationPoints>().formationPoints[i % 13].localPosition.x,
                            rectangleFormation.GetComponent<FormationPoints>().formationPoints[i % 13].localPosition.y,
                            rectangleFormation.GetComponent<FormationPoints>().formationPoints[i % 13].localPosition.z + i / 13);

            BodyguardPositionList.Add(Bodyguards[i].transform.localPosition);
        }
    }
    void BodyguardDikdortgenFormasyonu(Collider bodyguard)
    {
        int rowCount = 0;

        bodyguard.tag = "Bodyguard";
        bodyguard.GetComponent<Animator>().SetBool("isIdle", false);
        bodyguard.GetComponent<Animator>().SetBool("isRun", true);

        bodyguard.gameObject.transform.parent = GameObject.Find("Bodyguards").transform;

        rowCount = Bodyguards.Count / 13;

        bodyguard.gameObject.transform.localPosition = new Vector3(rectangleFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 13].localPosition.x,
                        rectangleFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 13].localPosition.y,
                        rectangleFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 13].localPosition.z + rowCount);

        for (int i = 0; i < Bodyguards.Count; i++)
        {
            if (Bodyguards[i] == null)
            {
                Bodyguards[i] = bodyguard.gameObject;
                bodyguard.gameObject.transform.localPosition = BodyguardPositionList[i];
                nullControl = true;
                break;
            }
        }

        if (nullControl == false)
        {
            Bodyguards.Add(bodyguard.gameObject);
            BodyguardPositionList.Add(bodyguard.gameObject.transform.localPosition);
        }
        nullControl = false;
    }
    public void KapiTriangleFormasyonu()
    {
        Bodyguards.RemoveAll(x => x == null);
        BodyguardPositionList.Clear();

        for (int i = 0; i < Bodyguards.Count; i++)
        {


            Bodyguards[i].transform.localPosition = new Vector3(triangleFormation.GetComponent<FormationPoints>().formationPoints[i % 36].localPosition.x,
                            triangleFormation.GetComponent<FormationPoints>().formationPoints[i % 36].localPosition.y,
                            triangleFormation.GetComponent<FormationPoints>().formationPoints[i % 36].localPosition.z);

            BodyguardPositionList.Add(Bodyguards[i % 36].transform.localPosition);
        }
    }
    void BodyguardTriangleFormasyonu(Collider bodyguard)
    {
        bodyguard.tag = "Bodyguard";
        bodyguard.GetComponent<Animator>().SetBool("isIdle", false);
        bodyguard.GetComponent<Animator>().SetBool("isRun", true);

        bodyguard.gameObject.transform.parent = GameObject.Find("Bodyguards").transform;

        bodyguard.gameObject.transform.localPosition = new Vector3(triangleFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 36].localPosition.x,
                        triangleFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 36].localPosition.y,
                        triangleFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 36].localPosition.z);

        for (int i = 0; i < Bodyguards.Count; i++)
        {
            if (Bodyguards[i] == null)
            {
                Bodyguards[i] = bodyguard.gameObject;
                bodyguard.gameObject.transform.localPosition = BodyguardPositionList[i];
                nullControl = true;
                break;
            }
        }

        if (nullControl == false)
        {
            Bodyguards.Add(bodyguard.gameObject);
            BodyguardPositionList.Add(bodyguard.gameObject.transform.localPosition);
        }
        nullControl = false;
    }










}

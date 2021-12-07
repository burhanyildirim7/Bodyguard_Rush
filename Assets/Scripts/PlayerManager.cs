using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static List<GameObject> Bodyguards;
    public static List<Vector3> BodyguardPositionList;

    public GameObject rectangleFormation, dikeyDikdortgenFormation, triangleFormation, uFormation, tersUFormation;

    public GameObject fx1, fx2;

    private bool isDikeyDikdortgen, isDikdortgen, isUcgen, isU, isTersU;

    bool nullControl;

    public UIController uiController;

    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("BizimKarakter");
        isTersU = true;
        Bodyguards = new List<GameObject>();
        BodyguardPositionList = new List<Vector3>();
        nullControl = false;
    }
    private void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Unclaimed Bodyguard")
        {
            if (isDikeyDikdortgen)
            {
                BodyguardDikeyDikdortgenFormasyonu(other);
            }
            else if (isDikdortgen)
            {
                BodyguardDikdortgenFormasyonu(other);
            }
            else if (isUcgen)
            {
                BodyguardUcgenFormasyonu(other);
            }
            else if (isU)
            {
                BodyguardUFormasyonu(other);
            }
            else if (isTersU)
            {
                BodyguardTersUFormasyonu(other);
            }

        }
        if (other.tag == "ObstacleKosan" || other.tag == "ObstacleDuran")
        {
            GameController._oyunAktif = false;

            player.GetComponent<Animator>().SetBool("isDance", false);
            player.GetComponent<Animator>().SetBool("isRun", false);
            player.GetComponent<Animator>().SetBool("isIdle", false);
            player.GetComponent<Animator>().SetBool("isCry", true);


            uiController.LoseScreenPanelOpen();
        }

        if (other.tag == "Obstacle")
        {
            GameController._oyunAktif = false;

            player.GetComponent<Animator>().SetBool("isDance", false);
            player.GetComponent<Animator>().SetBool("isRun", false);
            player.GetComponent<Animator>().SetBool("isIdle", false);
            player.GetComponent<Animator>().SetBool("isFall", true);

            uiController.LoseScreenPanelOpen();
        }

        if (other.tag == "DikeyDikdortgenKapisi")
        {
            isUcgen = false;
            isDikdortgen = false;
            isU = false;
            isTersU = false;
            isDikeyDikdortgen = true;
            KapiDikeyDikdortgenFormasyonu();
        }
        if (other.tag == "UcgenKapisi")
        {
            isDikdortgen = false;
            isDikeyDikdortgen = false;
            isU = false;
            isTersU = false;
            isUcgen = true;
            KapiUcgenFormasyonu();
        }
        if (other.tag == "DikdortgenKapisi")
        {
            isUcgen = false;
            isDikeyDikdortgen = false;
            isU = false;
            isTersU = false;
            isDikdortgen = true;
            KapiDikdortgenFormasyonu();
        }
        if (other.tag == "UKapisi")
        {
            isUcgen = false;
            isDikeyDikdortgen = false;
            isDikdortgen = false;
            isTersU = false;
            isU = true;
            KapiUFormasyonu();
        }
        if (other.tag == "TersUKapisi")
        {
            isUcgen = false;
            isDikeyDikdortgen = false;
            isDikdortgen = false;
            isU = false;
            isTersU = true;
            KapiTersUFormasyonu();
        }
        if (other.tag == "FinishLine")
        {
            GameController._oyunAktif = false;
            player.GetComponent<Animator>().SetBool("isRun", false);
            player.GetComponent<Animator>().SetBool("isIdle", true);
            StartCoroutine(bodyguardsFinish());
        }
    }

    IEnumerator bodyguardsFinish()
    {
        Vector3 bodyguardsFinishLocation = new Vector3(0f, 0f, 5f);

        Bodyguards.RemoveAll(x => x == null);
        BodyguardPositionList.Clear();

        for (int i = 0; i < Bodyguards.Count; i++)
        {
            Bodyguards[i].GetComponent<Animator>().SetBool("isRun", false);
            Bodyguards[i].GetComponent<Animator>().SetBool("isIdle", true);
        }

        for (int i = 0; i < Bodyguards.Count; i++)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>().Player = Bodyguards[i];
            Bodyguards[i].transform.localPosition = new Vector3(bodyguardsFinishLocation.x, bodyguardsFinishLocation.y + (float)(i * 1.75), bodyguardsFinishLocation.z);
            yield return new WaitForSeconds(0.2f);
        }
        Vector3 lastBodyguardPosition = Bodyguards[Bodyguards.Count - 1].transform.position;

        player.transform.position = new Vector3(lastBodyguardPosition.x, lastBodyguardPosition.y + 1.75f, lastBodyguardPosition.z);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>().Player = player;

        player.GetComponent<Animator>().SetBool("isIdle", false);
        player.GetComponent<Animator>().SetBool("isRun", false);
        player.GetComponent<Animator>().SetBool("isDance", true);

        fx1.SetActive(true);
        fx2.SetActive(true);

        uiController.WinScreenPanelOpen();
    }

    void KapiDikeyDikdortgenFormasyonu()
    {
        Bodyguards.RemoveAll(x => x == null);
        BodyguardPositionList.Clear();

        for (int i = 0; i < Bodyguards.Count; i++)
        {


            Bodyguards[i].transform.localPosition = new Vector3(dikeyDikdortgenFormation.GetComponent<FormationPoints>().formationPoints[i % 5].localPosition.x,
                            dikeyDikdortgenFormation.GetComponent<FormationPoints>().formationPoints[i % 5].localPosition.y,
                            dikeyDikdortgenFormation.GetComponent<FormationPoints>().formationPoints[i % 5].localPosition.z + i / 5);

            BodyguardPositionList.Add(Bodyguards[i].transform.localPosition);
        }
    }
    void BodyguardDikeyDikdortgenFormasyonu(Collider bodyguard)
    {
        int rowCount = 0;

        bodyguard.tag = "Bodyguard";
        bodyguard.GetComponent<Animator>().SetBool("isIdle", false);
        bodyguard.GetComponent<Animator>().SetBool("isRun", true);

        rowCount = Bodyguards.Count / 5;


        bodyguard.gameObject.transform.parent = GameObject.Find("Bodyguards").transform;

        bodyguard.gameObject.transform.localPosition = new Vector3(dikeyDikdortgenFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 5].localPosition.x,
                        dikeyDikdortgenFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 5].localPosition.y,
                        dikeyDikdortgenFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 5].localPosition.z + rowCount);

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

    void KapiDikdortgenFormasyonu()
    {
        Bodyguards.RemoveAll(x => x == null);
        BodyguardPositionList.Clear();

        for (int i = 0; i < Bodyguards.Count; i++)
        {
            Bodyguards[i].transform.localPosition = new Vector3(rectangleFormation.GetComponent<FormationPoints>().formationPoints[i % 9].localPosition.x,
                            rectangleFormation.GetComponent<FormationPoints>().formationPoints[i % 9].localPosition.y,
                            rectangleFormation.GetComponent<FormationPoints>().formationPoints[i % 9].localPosition.z + i / 9);

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

        rowCount = Bodyguards.Count / 7;

        bodyguard.gameObject.transform.localPosition = new Vector3(rectangleFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 9].localPosition.x,
                        rectangleFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 9].localPosition.y,
                        rectangleFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 9].localPosition.z + rowCount);

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

    void KapiUcgenFormasyonu()
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
    void BodyguardUcgenFormasyonu(Collider bodyguard)
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


    void KapiTersUFormasyonu()
    {

        Bodyguards.RemoveAll(x => x == null);
        BodyguardPositionList.Clear();

        for (int i = 0; i < Bodyguards.Count; i++)
        {
            Bodyguards[i].transform.localPosition = new Vector3(tersUFormation.GetComponent<FormationPoints>().formationPoints[i % 9].localPosition.x,
                            tersUFormation.GetComponent<FormationPoints>().formationPoints[i % 9].localPosition.y,
                            tersUFormation.GetComponent<FormationPoints>().formationPoints[i % 9].localPosition.z + i / 9);

            BodyguardPositionList.Add(Bodyguards[i].transform.localPosition);
        }
    }
    void BodyguardTersUFormasyonu(Collider bodyguard)
    {
        int rowCount = 0;

        bodyguard.tag = "Bodyguard";
        bodyguard.GetComponent<Animator>().SetBool("isIdle", false);
        bodyguard.GetComponent<Animator>().SetBool("isRun", true);

        bodyguard.gameObject.transform.parent = GameObject.Find("Bodyguards").transform;

        rowCount = Bodyguards.Count / 9;

        bodyguard.gameObject.transform.localPosition = new Vector3(tersUFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 9].localPosition.x,
                        tersUFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 9].localPosition.y,
                        tersUFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 9].localPosition.z + rowCount);

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



    void KapiUFormasyonu()
    {

        Bodyguards.RemoveAll(x => x == null);
        BodyguardPositionList.Clear();

        for (int i = 0; i < Bodyguards.Count; i++)
        {
            Bodyguards[i].transform.localPosition = new Vector3(uFormation.GetComponent<FormationPoints>().formationPoints[i % 9].localPosition.x,
                            uFormation.GetComponent<FormationPoints>().formationPoints[i % 9].localPosition.y,
                            uFormation.GetComponent<FormationPoints>().formationPoints[i % 9].localPosition.z + i / 9);

            BodyguardPositionList.Add(Bodyguards[i].transform.localPosition);
        }
    }
    void BodyguardUFormasyonu(Collider bodyguard)
    {
        int rowCount = 0;

        bodyguard.tag = "Bodyguard";
        bodyguard.GetComponent<Animator>().SetBool("isIdle", false);
        bodyguard.GetComponent<Animator>().SetBool("isRun", true);

        bodyguard.gameObject.transform.parent = GameObject.Find("Bodyguards").transform;

        rowCount = Bodyguards.Count / 9;

        bodyguard.gameObject.transform.localPosition = new Vector3(uFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 9].localPosition.x,
                        uFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 9].localPosition.y,
                        uFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 9].localPosition.z + rowCount);

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


















    void KareFormation()
    {
        int columns = 4;

        int space = 1;
        for (int i = 0; i < Bodyguards.Count; i++)
        {
            float posX = (i % columns) * space;
            float posZ = (i / columns) * space;
            Bodyguards[i].transform.localPosition = new Vector3(transform.position.x + posX, 0, transform.position.z + posZ);
        }
    }






    void DikdortgenFormation()
    {
        int rowNumber = 0;

        for (int i = 0; i < Bodyguards.Count; i++)
        {
            if (i % 7 == 0)
                rowNumber++;

            if (Bodyguards[i] != null)
                Bodyguards[i].transform.position = new Vector3(rectangleFormation.transform.position.x + (i % 7 - 3), rectangleFormation.transform.position.y, rectangleFormation.transform.position.z + (rowNumber));
        }
    }
    private void CircularRegroup()
    {
        var policeHolderPosition = GameObject.Find("BizimKarakter").transform.position;
        int numerator = 0;
        int denominator = 6;
        float multiplier = 0.7f;

        for (int i = 1; i < Bodyguards.Count; i++)
            if (Bodyguards[i] != null)
            {
                if (numerator / denominator >= 1)
                {
                    numerator = 0;
                    denominator += 6;
                    multiplier += 0.7f;
                }
                numerator++;
                float angle = numerator * (2 * Mathf.PI / denominator);

                float x = Mathf.Cos(angle) * multiplier;
                float y = Mathf.Sin(angle) * multiplier;

                var targetPosition = new Vector3(policeHolderPosition.x + x, policeHolderPosition.y, policeHolderPosition.z + y);
                Bodyguards[i].transform.position = Vector3.Lerp(Bodyguards[i].transform.position, targetPosition, 0.1f);
            }
    }
}

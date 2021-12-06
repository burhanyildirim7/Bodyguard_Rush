using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static List<GameObject> Bodyguards;
    public static List<Vector3> BodyguardPositionList;

    public GameObject rectangleFormation, squareFormation, triangleFormation;

    private bool isKare, isDikdortgen, isUcgen;

    bool nullControl;

    public UIController uiController;

    private void Start()
    {
        isDikdortgen = true;
        Bodyguards = new List<GameObject>();
        BodyguardPositionList = new List<Vector3>();
        nullControl = false;
    }
    private void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);

        if (other.tag == "Unclaimed Bodyguard")
        {
            if (isKare)
            {
                BodyguardKareFormasyonu(other);
            }
            else if (isDikdortgen)
            {
                BodyguardDikdortgenFormasyonu(other);
            }
            else if (isUcgen)
            {
                BodyguardUcgenFormasyonu(other);
            }

        }
        if (other.tag == "ObstacleKosan" || other.tag == "ObstacleDuran" || other.tag == "Obstacle")
        {
            GameController._oyunAktif = false;
            uiController.LoseScreenPanelOpen();
        }

        if (other.tag == "KareKapisi")
        {
            isUcgen = false;
            isDikdortgen = false;
            isKare = true;
            KapiKareFormasyonu();
        }
        if (other.tag == "UcgenKapisi")
        {
            isDikdortgen = false;
            isKare = false;
            isUcgen = true;
            KapiUcgenFormasyonu();
        }
        if (other.tag == "DikdortgenKapisi")
        {
            isUcgen = false;
            isKare = false;
            isDikdortgen = true;
            KapiDikdortgenFormasyonu();
        }
        if (other.tag == "FinishLine")
        {
            Vector3 bodyguardsFinishLocation = new Vector3(0f, 1f, 5f);
            GameController._oyunAktif = false;
            for (int i = 0; i < Bodyguards.Count; i++)
            {
                Bodyguards[i].GetComponent<Animator>().SetBool("isRun", false);
                Bodyguards[i].GetComponent<Animator>().SetBool("isIdle", true);

                Bodyguards[i].transform.localPosition = new Vector3(bodyguardsFinishLocation.x, bodyguardsFinishLocation.y + (float)(i * 1.75), bodyguardsFinishLocation.z);
            }
        }
    }

    void KapiKareFormasyonu()
    {
        Bodyguards.RemoveAll(x => x == null);
        BodyguardPositionList.Clear();

        for (int i = 0; i < Bodyguards.Count; i++)
        {


            Bodyguards[i].transform.localPosition = new Vector3(squareFormation.GetComponent<FormationPoints>().formationPoints[i].localPosition.x,
                            squareFormation.GetComponent<FormationPoints>().formationPoints[i].localPosition.y,
                            squareFormation.GetComponent<FormationPoints>().formationPoints[i].localPosition.z);

            BodyguardPositionList.Add(Bodyguards[i].transform.localPosition);
        }
    }
    void BodyguardKareFormasyonu(Collider bodyguard)
    {
        bodyguard.tag = "Bodyguard";
        bodyguard.GetComponent<Animator>().SetBool("isIdle", false);
        bodyguard.GetComponent<Animator>().SetBool("isRun", true);

        bodyguard.gameObject.transform.parent = GameObject.Find("Bodyguards").transform;

        bodyguard.gameObject.transform.localPosition = new Vector3(squareFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count].localPosition.x,
                        squareFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count].localPosition.y,
                        squareFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count].localPosition.z);

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
            Bodyguards[i].transform.localPosition = new Vector3(rectangleFormation.GetComponent<FormationPoints>().formationPoints[i % 7].localPosition.x,
                            rectangleFormation.GetComponent<FormationPoints>().formationPoints[i % 7].localPosition.y,
                            rectangleFormation.GetComponent<FormationPoints>().formationPoints[i % 7].localPosition.z);

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

        bodyguard.gameObject.transform.localPosition = new Vector3(rectangleFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 7].localPosition.x,
                        rectangleFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 7].localPosition.y,
                        rectangleFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count % 7].localPosition.z + rowCount);

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


            Bodyguards[i].transform.localPosition = new Vector3(triangleFormation.GetComponent<FormationPoints>().formationPoints[i].localPosition.x,
                            triangleFormation.GetComponent<FormationPoints>().formationPoints[i].localPosition.y,
                            triangleFormation.GetComponent<FormationPoints>().formationPoints[i].localPosition.z);

            BodyguardPositionList.Add(Bodyguards[i].transform.localPosition);
        }
    }
    void BodyguardUcgenFormasyonu(Collider bodyguard)
    {
        bodyguard.tag = "Bodyguard";
        bodyguard.GetComponent<Animator>().SetBool("isIdle", false);
        bodyguard.GetComponent<Animator>().SetBool("isRun", true);

        bodyguard.gameObject.transform.parent = GameObject.Find("Bodyguards").transform;

        bodyguard.gameObject.transform.localPosition = new Vector3(triangleFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count].localPosition.x,
                        triangleFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count].localPosition.y,
                        triangleFormation.GetComponent<FormationPoints>().formationPoints[Bodyguards.Count].localPosition.z);

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
        var policeHolderPosition = GameObject.Find("Player").transform.position;
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

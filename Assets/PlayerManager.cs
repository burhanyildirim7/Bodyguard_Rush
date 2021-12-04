using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<GameObject> Bodyguards;
    private void Start()
    {
        Bodyguards = new List<GameObject>();            
    }
    private void Update()
    {
        CircularRegroup();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Unclaimed Bodyguard")
        {
            other.tag = "Bodyguard";
            Bodyguards.Add(other.gameObject);
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

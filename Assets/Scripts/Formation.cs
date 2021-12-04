using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{
    public GameObject prefab;
    void TriangleFormation()
    {
        Vector3 targetPosition = Vector3.left;

        int rows = 3;
        float rowOffset = 0.5f;
        float yOffset = -1.0f;
        float xOffset = 1.0f;

        for (int i = 1; i <=rows; i++)
        {
            for (int j = 0; j < i; j++)
            {
                GameObject instance = Instantiate(prefab);
                targetPosition = new Vector3(targetPosition.x+xOffset, targetPosition.y, 0);
                instance.transform.position = targetPosition;
            }
            targetPosition = new Vector3((rowOffset * i), targetPosition.y + yOffset, 0f);
        }
    }

    private void SquareFormation()
    {
        Vector3 targetpostion = Vector3.zero;

        int counter = -1;
        int xoffset = -1;

        float sqrt = Mathf.Sqrt(10);
        float startx = targetpostion.x;

        for (int i = 0; i < 10; i++)
        {
            GameObject instance = Instantiate(prefab);

            counter++;
            xoffset++;

            if (xoffset > 1)
            {
                xoffset = 1;
            }

            targetpostion = new Vector3(targetpostion.x + (xoffset * 2.0f), targetpostion.y, 0f);

            if (counter == Mathf.Floor(sqrt))
            {
                counter = 0;
                targetpostion.x = startx;
                targetpostion.y += 1 + 0.25f;
            }
            instance.transform.position = targetpostion;
        }
    }
}

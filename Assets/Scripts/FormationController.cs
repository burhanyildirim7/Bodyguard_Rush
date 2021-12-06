using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The FormationController.
/// Handles the Formation functionalit.
/// </summary>
public class FormationController : MonoBehaviour
{
    // Reference to the prefab to spawn.
    public GameObject prefab;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //this.SphereFormation(10, 3, 7);
        //this.TriangleFormation();
        //this.CircleFormation();
        //this.SquareFormation();
    }

    private void Update()
    {
        //CircleFormation();
    }

    /// <summary>
    /// The CircleFormation function.
    /// Creates a Circle Formation.
    /// </summary>
    private void CircleFormation()
    {
        Vector3 targetPosition = player.transform.position;

        for (int i = 0; i < PlayerManager.Bodyguards.Count; i++)
        {
            float angle = i * (2 * 3.14159f / PlayerManager.Bodyguards.Count);

            float x = Mathf.Cos(angle) * 1.5f;
            float z = Mathf.Sin(angle) * 1.5f;

            targetPosition = new Vector3(targetPosition.x + x, targetPosition.y, targetPosition.z +z);

            PlayerManager.Bodyguards[i].transform.position = Vector3.Lerp(PlayerManager.Bodyguards[i].transform.position, targetPosition, 0.1f);
        }
    }

    /// <summary>
    /// The SquareFormation function.
    /// Creates a Square Formation.
    /// </summary>
    private void SquareFormation()
    {
        // Set a targetposition variable of where to spawn objects.
        Vector3 targetpostion = Vector3.zero;

        // Counter used for indexing when to start a new row.
        int counter = -1;
        // The offset of each object from one another on the X axis.
        int xoffset = -1;

        // Get the square root
        float sqrt = Mathf.Sqrt(10);

        // Get the reference to the starting target positions x.
        float startx = targetpostion.x;

        // Loop through the number of objects to spawn for the square.
        for (int i = 0; i < 10; i++)
        {
            // Instantiate the prefab.
            GameObject instance = Instantiate(prefab);

            // Increment the counter by 1.
            counter++;
            // Increment the xoffset by 1.
            xoffset++;

            /// We do this check because we do not want the offset being 1 on the 
            /// first iteration of the loop. We want the first index to be placed at 0.
            // If the xoffset > 1.
            if (xoffset > 1)
            {// Set the xoffset to 1.
                xoffset = 1;
            }

            // Set the targetposition to a new Vector 3 with the new variables and offset applied.
            targetpostion = new Vector3(targetpostion.x + (xoffset * 2.0f), targetpostion.y, 0f);

            // If the counter is equal to the sqrt variable rounded down.
            if (counter == Mathf.Floor(sqrt))
            {// Reset counter to 0.
                counter = 0;
                // Set the targetposition x to the referenced start x.
                targetpostion.x = startx;
                // Set the targetposition y to 1 + 0.25f.
                // The 1 is to increment in the y axis, giving another row.
                // The 0.25f is to offset each sphere is the y axis so they do not overlap.
                targetpostion.y += 1 + 0.25f;
            }
            // x
            // xxx
            // xxx
            // xxx
            ////
            // Set the position of the instantiated object to the targetposition.
            instance.transform.position = targetpostion;
        }
    }

    /// <summary>
    /// The SpiralFormation function.
    /// Creates a Spiral Formation.
    /// </summary>
    private void SpiralFormation()
    {
        // Set a targetPosition variable of where to spawn objects.
        Vector3 targetPosition = player.transform.position;

        // Set a container variable that is used to hold child objects in the inspector.
        GameObject container = new GameObject("Container");
        // Set the position to 0.
        container.transform.position = Vector3.zero;

        // Reference to the offset in the y axis.
        float yOffset = 0.5f;

        // Loop through the number of spheres to spawn in the spiral.
        for (int i = 0; i < 25; i++)
        {
            // Instantiate the prefab.
            GameObject instance = Instantiate(prefab);

            // Get the angle of the current index being instantiated
            // from the center.
            float angle = i * (2 * 3.14159f / 10);

            // Get the X Position of the angle times 1.5f. 1.5f is the radius.
            float x = Mathf.Cos(angle) * 1.5f;
            // Get the Z Position of the angle times 1.5f. 1.5f is the radius.
            float z = Mathf.Sin(angle) * 1.5f;

            // Set the targetPosition to a new Vector3 with the new variables.
            // The Y offset is what gives the spiral effect going up.
            targetPosition = new Vector3(targetPosition.x + x, targetPosition.y + yOffset, targetPosition.z + z);

            // Set the position of the instantiated object to the targetPosition.
            instance.transform.position = targetPosition;
            // Set the instantiated object as a child of the container gameObject.
            instance.transform.SetParent(container.transform);
        }

        // Add the ContainerComponent to the container.
        // container.AddComponent<ContainerComponent>();
    }

    /// <summary>
    /// The TriangleFormation function.
    /// Creates a Triangle Formation.
    /// </summary>
    private void TriangleFormation()
    {
        // Set a container variable that is used to hold child objects in the inspector.
        GameObject container = new GameObject("Container");
        // Set the position to 0.
        container.transform.position = Vector3.zero;

        // Set a targetPosition variable of where to spawn objects.
        // This is set to (-1,0,0) to allow proper setting of the first instantiated object.
        Vector3 targetPosition = Vector3.left;

        // Number of rows in the triangle.
        int rows = 10;
        // The offset for each row in the x axis.
        float rowOffset = -0.5f;
        // The y offset of each row.
        float yOffset = -1.0f;
        // The x offset for each object spawned in a row.
        float xOffset = 1.0f;

        // Loop through the number of rows in the triangle.
        for (int i = 1; i <= rows; i++)
        {
            // Loop through the number of objects in each row of the triangle.
            for (int j = 0; j < i; j++)
            {
                // Instantiate the prefab.
                GameObject instance = Instantiate(prefab);
                // Set the targetPosition to a new Vector3 with the new variables.
                // The X offset is what gives the space between each object spwaned in a row.
                targetPosition = new Vector3(targetPosition.x + xOffset, targetPosition.y, 0);
                // Set the position of the instantiated object to the targetPosition.
                instance.transform.position = targetPosition;
                // Set the instantiated object as a child of the container gameObject.
                instance.transform.SetParent(container.transform);
            }

            // Set the targetPosition to a new Vector3 with the new variables.
            // The rowOffset gives the x offset of each row on the x axis.
            // The Y offset is what gives the space between each row in the y axis.
            targetPosition = new Vector3((rowOffset * i) - 1.0f, targetPosition.y + yOffset, 0f);
        }

        // Add the ContainerComponent to the container.
        // container.AddComponent<ContainerComponent>();
    }

    /// <summary>
    /// The HalfCircleFormation Function.
    /// Creates a Half Circle Formation.
    /// </summary>
    /// <param name="numberOfPoints">The numberOfPoints in the half circle.</param>
    /// <param name="radius">The radius of the half circle.</param>
    /// <returns></returns>
    private List<GameObject> HalfCircleFormation(int numberOfPoints, int radius)
    {
        // Reference to the numberOfPoints to spawn minus 1.
        int pieces = numberOfPoints - 1;

        // Reference to the list of spheres that will be created for the half circle.
        List<GameObject> spheres = new List<GameObject>();

        // Set a container variable that is used to hold child objects in the inspector.
        GameObject container = new GameObject("SphereContainer");
        // Set the position to 0.
        container.transform.position = Vector3.zero;

        // Loop through the numberOfPoints that are in the half circle.
        for (int i = 0; i < numberOfPoints; i++)
        {
            // Instantiate the prefab.
            GameObject instance = Instantiate(prefab);

            // Get the angle of the current index being instantiated
            // from the center.
            // Mathf.PI represents half a circle.
            float theta = Mathf.PI * i / pieces;
            // Get the X Position of the theta angle times 1.5f. 1.5f is the radius.
            float X = Mathf.Cos(theta) * radius;
            // Get the Y Position of the theta angle times 1.5f. 1.5f is the radius.
            float Y = Mathf.Sin(theta) * radius;

            // Set the instantiated gameObjects position to a new Vector3 with the new variables.
            instance.transform.position = new Vector3(X, Y, 0);
            // Set the gameObjects color to green.
            instance.GetComponent<MeshRenderer>().material.color = Color.green;
            // Set the instantiated object as a child of the container gameObject.
            instance.transform.SetParent(container.transform);
            // Add the instance to the list.
            spheres.Add(instance);
        }
        // Return the half circle that was created.
        return spheres;
    }

    /// <summary>
    /// The SphereFormation function.
    /// Creates a Sphere Formation.
    /// </summary>
    /// <param name="numberOfPoints">The numberOfPoints in the sphere.</param>
    /// <param name="radius">The radius of the sphere.</param>
    /// <param name="numberOfMeridians">The number numberOfMeridians in the sphere.</param>
    private void SphereFormation(int numberOfPoints, int radius, int numberOfMeridians)
    {
        // Create a Half Circle Formation and return the list of gameObjects.
        List<GameObject> spheres = HalfCircleFormation(numberOfPoints, radius);
        // Create a list of colors.
        List<Color> colors = new List<Color>() { Color.black, Color.blue, Color.red, Color.yellow };
        // Reference to the index of color being set to spheres.
        int colorIndex = 0;

        // Find the container variable that is used to hold child objects in the inspector.
        GameObject sphereContainer = GameObject.Find("SphereContainer");

        // Loop through the numberOfMeridians.
        // Meridians are the amount of half circles you want to create.
        for (int i = 1; i < numberOfMeridians; i++)
        {// Get the angle of the current index being instantiated from the center.
            float phi = 2 * Mathf.PI * ((float)i / (float)numberOfMeridians);

            // Loop through the numberOfPoints you want to spawn in this meridian
            for (int j = 1; j < numberOfPoints; j++)
            {
                // Instantiate the prefab.
                GameObject instance = Instantiate(prefab);

                // Get the X position of the current sphere being viewed.
                float X = spheres[j].transform.position.x;
                // Get the Y position of the current sphere being viewed.
                float Y = spheres[j].transform.position.y * Mathf.Cos(phi) - spheres[j].transform.position.z * Mathf.Sin(phi);
                // Get the Z position of the current sphere being viewed.
                float Z = spheres[j].transform.position.z * Mathf.Cos(phi) + spheres[j].transform.position.y * Mathf.Sin(phi);

                // Set the instantiated gameObjects position to a new Vector3 with the new variables.
                instance.transform.position = new Vector3(X, Y, Z);
                // Set the gameObjects color to the color being indexed from the colors list.
                instance.GetComponent<MeshRenderer>().material.color = colors[colorIndex];
                // Set the instantiated object as a child of the container gameObject.
                instance.transform.SetParent(sphereContainer.transform);
            }

            // If the colorIndex is greater than or equal to the colors count - 1.
            // Set the color index to 0.
            // This means reset the colors back from the beginning of the list.
            // This will prevent throwing an exception from indexing the list.
            // Else just increment in the colorIndex by 1.
            if (colorIndex >= colors.Count - 1)
                colorIndex = 0;
            else
                colorIndex++;
        }
    }
}
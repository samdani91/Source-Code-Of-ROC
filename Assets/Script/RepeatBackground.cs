using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    public new GameObject camera;
    public float parallaxEffect;
    private float width, positionX;

    void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        positionX = transform.position.x;
    }

    void Update()
    {
        float parallaxDistance = camera.transform.position.x * parallaxEffect;
        float remDist = camera.transform.position.x * (1 - parallaxDistance);

        transform.position = new Vector3(positionX + parallaxDistance, transform.position.y, transform.position.z);

        if (remDist > positionX + width)
        {
            positionX += width;
        }
    }
}

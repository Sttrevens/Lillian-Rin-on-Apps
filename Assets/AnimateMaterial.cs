using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMaterial : MonoBehaviour
{
    public float speed = 1.0f;
    private Material material;
    private float currentOffset = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the material of the object
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new offset
        currentOffset -= Time.deltaTime * speed;

        // Reset the offset if it's less than -0.4
        if (currentOffset < -0.4f)
        {
            currentOffset = 0.6f;
        }

        // Apply the offset to the material's main texture
        material.SetTextureOffset("_MainTex", new Vector2(currentOffset, 0));
    }
}

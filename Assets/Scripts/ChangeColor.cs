using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Material groundMaterial;
    public Color[] colors;
    public float time;

    private float currentTime;
    private int colorid;

    void Update()
    {
        ChangeColorOverTime();
    }

    public void ChangeColorOverTime()
    {
        if (currentTime <= 0)
        {
            colorid++;
            if (colorid >= colors.Length)
            {
                colorid = 0;
            }

            currentTime = time;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }

        groundMaterial.color = Color.Lerp(groundMaterial.color, colors[colorid], 0.2f * Time.deltaTime);
    }
}

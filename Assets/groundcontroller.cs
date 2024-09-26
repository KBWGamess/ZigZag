
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class groundcontroller : MonoBehaviour
{
    public Material groundMaterial;
    public Color[] colors;
    public float time;
    private float stime;
    private int colorid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (stime <= 0)
        {
            colorid++;
            if (colorid >= colors.Length)
            {
                colorid = 0;
            }
            stime = time;
        } 
      else
        {
            stime -= Time.deltaTime;
        }

      groundMaterial.color = Color.Lerp(groundMaterial.color, colors[colorid], 0.2f*Time.deltaTime);       
    }
}

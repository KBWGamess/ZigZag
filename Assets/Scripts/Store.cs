using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    private Animator menuIdle;

    void Start()
    {
        menuIdle = GetComponent<Animator>();
    }


    void Update()
    {
        
    }

    public void OpenMenu()
    {
        menuIdle.SetBool("open", true);
        menuIdle.SetBool("close", false);
    }

    public void CloseMenu()
    {
        menuIdle.SetBool("open", false);
        menuIdle.SetBool("close", true);
    }
}

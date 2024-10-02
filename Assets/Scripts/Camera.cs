using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float speed;
    public Transform ball;
    public Vector3 ofset;
    // Start is called before the first frame update
    void Start()
    {
        ofset = transform.position - ball.position;  
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, ball.position+ofset, speed * Time.deltaTime); 
    }

    public void MusicOff()
    {
        AudioListener.volume = 0f;
    }

}

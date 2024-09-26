
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public Vector3 dir;
    public float speed;
    private bool startgame = false;


    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) 
        {
            if (!startgame)
            {
                startgame = true;
            }
            else
            {
                if (dir.x == -1)
                {
                    dir = Vector3.forward;
                }
                else
                {
                    dir = Vector3.left;
                }
            }
            
        }
        if (startgame)
        {
            transform.position += dir * speed * Time.deltaTime;
        }  
        
        if (transform.position.y <-2f)
        {
            SceneManager.LoadScene(0);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
     if (collision.gameObject.tag=="Floor")
        {
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }

}

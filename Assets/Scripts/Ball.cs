
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public Vector3 dir;
    public float speed;

    private bool startgame = false;

    private GameOver gameOver;
    private Score score;

    private Rigidbody rb;


    void Start()
    {
        gameOver = FindObjectOfType<GameOver>();
        score = FindObjectOfType<Score>();

        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) 
        {
            if (!startgame)
            {
                
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
            gameOver.OpenMenu();
            score.StopScore();
            AudioListener.volume = 0f;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            dir = Vector3.zero;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag=="Floor")
        {
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }

    public void PlayGame()
    {
        startgame = true;
        Debug.Log(startgame);
    }

}

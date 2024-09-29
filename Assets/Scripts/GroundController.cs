using System.Runtime.CompilerServices;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    private Spawner spawner;

    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    void Update()
    {
        CountController(); 
    }


    public void CountController()
    {
        if (transform.position.y < -4f)
        {
            Destroy(gameObject);
            spawner.Create();
        }
    }

  
}


using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject last;
    public GameObject prefabe;

    private GameObject newObJect;
    private int dir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Create(); 
    }
    private void Create()
    {
        dir = Random.Range(0, 2);

        if (dir == 0)
        {
            newObJect = Instantiate(prefabe, new Vector3(last.transform.position.x - 1f, last.transform.position.y, last.transform.position.z), Quaternion.identity);
            last  = newObJect;
        }
       else
        {
            newObJect = Instantiate(prefabe, new Vector3(last.transform.position.x, last.transform.position.y, last.transform.position.z + 1f), Quaternion.identity);
            last = newObJect;
        }
    }
}

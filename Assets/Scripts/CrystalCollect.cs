using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrystalCollect : MonoBehaviour
{
    public TextMeshProUGUI crystalsCountText;

    private int crystals;

    void Start()
    {
        crystals = PlayerPrefs.GetInt("crystals", 0);
        crystalsCountText.text = crystals.ToString();
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Crystal")
        {
            crystals++;
            Destroy(other.gameObject);  
            crystalsCountText.text = crystals.ToString();
            PlayerPrefs.SetInt("crystals", crystals);
        }
    }
}

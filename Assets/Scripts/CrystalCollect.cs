using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CrystalCollect : MonoBehaviour
{
    public TextMeshProUGUI crystalsCountText;
    public TextMeshProUGUI currentSesionCrystalsText;
    public Button doubleButton;

    private int allCrystals;
    private int currentSesionCrystals;
    private ADPlay aDplay;

    void Start()
    {
        aDplay = FindObjectOfType<ADPlay>();
        allCrystals = PlayerPrefs.GetInt("crystals", 0);
        ChangeText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Crystal")
        {
            allCrystals++;
            currentSesionCrystals++;
            Destroy(other.gameObject);
            ChangeText();
            PlayerPrefs.SetInt("crystals", allCrystals);
        }
    }

    public void DoubleCrystals()
    {
        aDplay.AdPlay();
        allCrystals += currentSesionCrystals;
        currentSesionCrystals *= 2;
        doubleButton.interactable = false;
        Debug.Log(allCrystals);
        ChangeText();
        PlayerPrefs.SetInt("crystals", allCrystals);
    }

    public void ChangeText()
    {
        crystalsCountText.text = allCrystals.ToString();
        currentSesionCrystalsText.text = currentSesionCrystals.ToString();
    }
}

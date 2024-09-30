using TMPro;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    public Material[] skins;
    public TextMeshProUGUI crystalsCountText;

    public GameObject secondPrice;
    public GameObject thirdPrice;
    public GameObject fourthPrice;
    public GameObject fifthPrice;
    public GameObject sixthPrice;

    private MeshRenderer meshRenderer;
    private int allCrystals;

    private int skinID;

    private bool boughtsecond = false;
    private bool boughtthird = false;
    private bool boughtfourth = false;
    private bool boughtfiveth = false;
    private bool boughtsixth = false;

    private void Start()
    {
        allCrystals = PlayerPrefs.GetInt("crystals", 0);

        boughtsecond = PlayerPrefs.GetInt("boughtsecond", 0) == 1;
        boughtthird = PlayerPrefs.GetInt("boughtthird", 0) == 1;
        boughtfourth = PlayerPrefs.GetInt("boughtfourth", 0) == 1;
        boughtfiveth = PlayerPrefs.GetInt("boughtfiveth", 0) == 1;
        boughtsixth = PlayerPrefs.GetInt("boughtsixth", 0) == 1;

        if(boughtsecond)
            secondPrice.SetActive(false);
        if(boughtthird)
            thirdPrice.SetActive(false);
        if(boughtfourth)
            fourthPrice.SetActive(false);
        if(boughtfiveth)
            fifthPrice.SetActive(false);
        if(boughtsixth)
            sixthPrice.SetActive(false);

        skinID = PlayerPrefs.GetInt("skinID", 0);
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = skins[skinID];
    }

    public void FirstSkin()
    {
        meshRenderer.material = skins[0];
        skinID = 0;
        PlayerPrefs.SetInt("skinID", skinID);
    }

    public void SecondSkin()
    {
        if (allCrystals >= 50 && !boughtsecond)
        {
            meshRenderer.material = skins[1];
            allCrystals -= 50;
            boughtsecond = true;
            secondPrice.SetActive(false);
            skinID = 1;
            PlayerPrefs.SetInt("skinID", skinID);
            PlayerPrefs.SetInt("crystals", allCrystals);
            PlayerPrefs.SetInt("boughtsecond", boughtsecond ? 1 : 0);
            crystalsCountText.text = allCrystals.ToString();
        }
        else if (boughtsecond)
        {
            meshRenderer.material = skins[1];
            skinID = 1;
            secondPrice.SetActive(false);
            PlayerPrefs.SetInt("skinID", skinID);
        }
    }

    public void ThirdSkin()
    {
        if (allCrystals >= 100 && !boughtthird)
        {
            meshRenderer.material = skins[2];
            allCrystals -= 100;
            boughtthird = true;
            thirdPrice.SetActive(false);
            skinID = 2;
            PlayerPrefs.SetInt("skinID", skinID);
            PlayerPrefs.SetInt("crystals", allCrystals);
            PlayerPrefs.SetInt("boughtthird", boughtthird ? 1 : 0);
            crystalsCountText.text = allCrystals.ToString();
        }
        else if (boughtthird)
        {
            meshRenderer.material = skins[2];
            skinID = 2;
            thirdPrice.SetActive(false);
            PlayerPrefs.SetInt("skinID", skinID);
        }
    }

    public void Fourthskin()
    {
        if (allCrystals >= 150 && !boughtfourth)
        {
            meshRenderer.material = skins[3];
            allCrystals -= 150;
            boughtfourth = true;
            fourthPrice.SetActive(false);
            skinID = 3;
            PlayerPrefs.SetInt("skinID", skinID);
            PlayerPrefs.SetInt("crystals", allCrystals);
            PlayerPrefs.SetInt("boughtfourth", boughtfourth ? 1 : 0);
            crystalsCountText.text = allCrystals.ToString();
        }
        else if (boughtfourth)
        {
            meshRenderer.material = skins[3];
            skinID = 3;
            fourthPrice.SetActive(false);
            PlayerPrefs.SetInt("skinID", skinID);
        }
    }

    public void FifthSkin()
    {
        if (allCrystals >= 200 && !boughtfiveth)
        {
            meshRenderer.material = skins[4];
            allCrystals -= 200;
            boughtfiveth = true;
            fifthPrice.SetActive(false);
            skinID = 4;
            PlayerPrefs.SetInt("skinID", skinID);
            PlayerPrefs.SetInt("crystals", allCrystals);
            PlayerPrefs.SetInt("boughtfiveth", boughtfiveth ? 1 : 0);
            crystalsCountText.text = allCrystals.ToString();
        }
        else if (boughtfiveth)
        {
            meshRenderer.material = skins[4];
            skinID = 4;
            fifthPrice.SetActive(false);
            PlayerPrefs.SetInt("skinID", skinID);
        }
    }

    public void SixthSkin()
    {
        if (allCrystals >= 300 && !boughtsixth)
        {
            meshRenderer.material = skins[5];
            allCrystals -= 300;
            boughtsixth = true;
            sixthPrice.SetActive(false);
            skinID = 5;
            PlayerPrefs.SetInt("skinID", skinID);
            PlayerPrefs.SetInt("crystals", allCrystals);
            PlayerPrefs.SetInt("boughtsixth", boughtsixth ? 1 : 0);
            crystalsCountText.text = allCrystals.ToString();
        }
        else if (boughtsixth)
        {
            meshRenderer.material = skins[5];
            skinID = 5;
            sixthPrice.SetActive(false);
            PlayerPrefs.SetInt("skinID", skinID);
        }
    }
}

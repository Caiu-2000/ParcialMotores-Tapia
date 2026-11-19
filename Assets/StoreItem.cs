using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI SkinName;
    [SerializeField] TextMeshProUGUI Price;

    [SerializeField] Image SkinImage;

    StoreManager storeManager;

    Skinset set;
    [SerializeField] Button BuyButton;
    public void Init(Skinset skin , StoreManager manager)
    {
        storeManager = manager;
     
        SkinName.text = skin.SkinName;
        Price.text = skin.SkinPrice.ToString();
        if (skin.CharacterName == CharacterNames.Davo)
        {
            
         
            Debug.Log("El indice es : " + skin.SkinIndex + " El conteo de lista es : " + GlobalData.SkinsDavo.Count);
            if (GlobalData.SkinsDavo[skin.SkinIndex])
            {
                Price.text = "Comprado";
                BuyButton.gameObject.SetActive(false);
            }

        }
        else if (skin.CharacterName == CharacterNames.Cobra)
        {
            Debug.LogError("FALTA IMPLEMENTAR COBRA MONO");
        }
            
        SkinImage.sprite = skin.NormalSprite;
        set = skin;
    }

    public void BuyPressed()
    {
       if (storeManager.TryBuyThis(set))
        {
            Price.text = "Comprado";
            BuyButton.gameObject.SetActive(false);
        }
    }
}

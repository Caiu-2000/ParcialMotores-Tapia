using System;
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

    int cobraindex = 0;
    int davoindex = 1;


    public void Init(Skinset skin , StoreManager manager)
    {
        int Skinindex;

        if (skin.CharacterName == CharacterNames.Cobra)
        {
            cobraindex++;
            Skinindex = cobraindex;
        }
        else
        {
            davoindex++;
            Skinindex = davoindex;
        }

        storeManager = manager;
     
        SkinName.text = skin.SkinName;
        Price.text = skin.SkinPrice.ToString();
        

            if (SaveManager.IsSkinUnlocked(skin, skin.SkinIndex))
            {
                Price.text = "Comprado";
                BuyButton.gameObject.SetActive(false);
            }

        if (skin.SkinIcon != null)
        {
            SkinImage.sprite = skin.SkinIcon;
        }
        else SkinImage.sprite = skin.NormalSprite;
        
        set = skin;
    }

    public void BuyPressed()
    {
        print("Se toco el boton");
       if (storeManager.TryBuyThis(set))
        {
            Price.text = "Comprado";
            BuyButton.gameObject.SetActive(false);
        }
    }
}

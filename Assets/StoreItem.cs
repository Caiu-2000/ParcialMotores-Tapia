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
    public int Skinindex = 0;
    public void Init(Skinset skin , StoreManager manager)
    {
        storeManager = manager;
     
        SkinName.text = skin.SkinName;
        Price.text = skin.SkinPrice.ToString();
        

            Skinindex = skin.SkinIndex;
            if (SaveManager.IsSkinUnlocked(skin, skin.SkinIndex))
            {
                Price.text = "Comprado";
                BuyButton.gameObject.SetActive(false);
            }

        SkinImage.sprite = skin.NormalSprite;
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

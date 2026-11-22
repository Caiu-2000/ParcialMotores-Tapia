
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [SerializeField] Skinset[] SkinsDavo;
    [SerializeField] GameObject ItemsHolder;

    [SerializeField] StoreItem ItemPrefab;

    [SerializeField] GameObject margin;




    public bool TryBuyThis(Skinset skin)
    {
        print("Se intenta comprar");
        if (GlobalData.CurrentMoney >= skin.SkinPrice)
        {
            GlobalData.RemoveMoney(skin.SkinPrice);
            if (skin.CharacterName == CharacterNames.Davo)
            {
                print(skin.SkinIndex);
                GlobalData.SkinsDavo[skin.SkinIndex] = true;
            }
            return true;
        }
        else
        {
            Debug.Log("Not enough money");
            return false;
        }
    }


    private void WipeStore()
    {
        foreach (Transform child in ItemsHolder.transform)
        {
            if (child.gameObject == margin) continue;
            Destroy(child.gameObject);
        }
    }
    public void LoadStore()
    {
        WipeStore();
  
        int aux = 0;
        foreach (Skinset skin in SkinsDavo)
        {
            var item = Instantiate(ItemPrefab, ItemsHolder.transform);
            item.Init(skin, this);
            aux++;
        }
    }


}

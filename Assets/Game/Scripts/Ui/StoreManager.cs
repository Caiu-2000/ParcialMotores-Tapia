
using UnityEngine;

public class StoreManager : MonoBehaviour
{

    [SerializeField] StoreItem ItemPrefab;
    [SerializeField] GameObject Content;



    public bool TryBuyThis(Skinset skin)
    {
        if (GlobalData.CurrentMoney >= skin.SkinPrice)
        {
            GlobalData.RemoveMoney(skin.SkinPrice);
            if (skin.CharacterName == CharacterNames.Davo)
            {
                GlobalData.SkinsDavo[skin.SkinIndex] = true;
            }
            else if (skin.CharacterName == CharacterNames.Cobra)
            {
                GlobalData.SkinsCobra[skin.SkinIndex] = true;
            }
            return true;
        }
        else
        {
            return false;
        }
    }


    private void WipeStore()
    {
        
        foreach (Transform child in Content.transform)
        {
           
            Destroy(child.gameObject);
        }
    }
    public void LoadStore()
    {
        WipeStore();
        Skinset[] skins = Resources.LoadAll<Skinset>("SkinSets");
  
        int indexCobra = 1;
        int indexDavo = 1;
        
        foreach (Skinset skin in skins)
        {
            var skinInstance = Instantiate(skin);
            
            if (skinInstance.CharacterName == CharacterNames.Cobra)
            {
                skinInstance.SkinIndex = indexCobra;
                indexCobra ++;
            }
            else if (skinInstance.CharacterName == CharacterNames.Davo)
            {
                skinInstance.SkinIndex = indexDavo;
                indexDavo++;
            }

            StoreItem item = Instantiate(ItemPrefab, Content.transform);
            item.Init(skin, this);
            
        }
    }


}

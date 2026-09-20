
using TMPro;
using UnityEngine;


public class AuraUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    


    private void Start()
    {
        text.text = "AURA TOTAL : " + RunManager.RunPoints.ToString(); 
    }
}

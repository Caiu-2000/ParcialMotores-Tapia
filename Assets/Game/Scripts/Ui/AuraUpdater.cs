
using TMPro;
using UnityEngine;


public class AuraUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    


    private void Start()
    {
        text.text =  RunManager.RunPoints.ToString(); 
    }
}

using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class AuraUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;



    private void Start()
    {
        text.text = "AURA TOTAL : " + RunManager.RunPoints.ToString(); 
    }
}

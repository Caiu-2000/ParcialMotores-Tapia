using System.Collections;

using UnityEngine;

public class BombButton : MonoBehaviour
{
    private const float CdTime = 3.5f;
    private bool inCD = false;
    public void Pressed()
    {
        if (inCD) return;
        StartCoroutine(cd());
        print("ApretoBoton");
        EventManager<InputEvents>.Publish<int>(InputEvents.BombPressed, 0);
    }


    private IEnumerator cd()
    {
        inCD = true;
        yield return new WaitForSeconds(CdTime);
        inCD = false;
    }
}


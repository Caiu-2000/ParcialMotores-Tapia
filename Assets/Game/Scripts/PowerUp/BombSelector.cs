using UnityEngine;

public class BombSelector : MonoBehaviour
{
    [SerializeField] Bomb CobraBomb;
    [SerializeField] Bomb DavoBomb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GlobalData.SelectedCharacter == CharacterNames.Cobra)
        {
            var bomb = Instantiate(CobraBomb, transform.position, Quaternion.identity);
        }
        else
        { 
            var bomb = Instantiate(DavoBomb, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}

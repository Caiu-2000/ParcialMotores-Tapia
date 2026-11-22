using UnityEngine;

public class SpriteRotator : MonoBehaviour
{
    [SerializeField] private float rotationspeed =50f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, rotationspeed * Time.deltaTime);
    }
}

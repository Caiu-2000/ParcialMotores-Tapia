using UnityEngine;

public class spritesTretcher : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;


    void Start()
    {
        return;
        spriteRenderer = GetComponent<SpriteRenderer>();
        float altura = Camera.main.orthographicSize * 2f;
        float ancho = altura * Camera.main.aspect;

        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        transform.localScale = new Vector3(
            ancho / spriteSize.x,
            altura / spriteSize.y,
            1f
        );
    }

}

using UnityEngine;

// MEJORAS PLAYER (script nuevo)
// Dibuja la hitbox del jugador (punto blanco con borde rojo) sin necesitar ningún sprite importado.
public class HitboxVisual : MonoBehaviour
{
    private const int TextureSize = 64;
    private const int SortingOrder = 100;
    private static Sprite cachedSprite;

    public static HitboxVisual Create(Transform parent, float hitboxRadius)
    {
        GameObject go = new GameObject("HitboxVisual");
        go.transform.SetParent(parent, false);

        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = GetSprite();
        sr.sortingOrder = SortingOrder;

        // Un poco más grande que la hitbox real para que se vea en pantallas chicas.
        float diameter = Mathf.Max(hitboxRadius * 2f, 0.1f) * 1.6f;
        go.transform.localScale = new Vector3(diameter, diameter, 1f);

        return go.AddComponent<HitboxVisual>();
    }

    private static Sprite GetSprite()
    {
        if (cachedSprite != null) return cachedSprite;

        Texture2D tex = new Texture2D(TextureSize, TextureSize, TextureFormat.RGBA32, false)
        {
            filterMode = FilterMode.Bilinear,
            wrapMode = TextureWrapMode.Clamp,
            name = "HitboxTexture"
        };

        Color32[] pixels = new Color32[TextureSize * TextureSize];
        float center = (TextureSize - 1) * 0.5f;
        for (int y = 0; y < TextureSize; y++)
        {
            for (int x = 0; x < TextureSize; x++)
            {
                float d = Mathf.Sqrt((x - center) * (x - center) + (y - center) * (y - center)) / center;
                Color32 c;
                if (d <= 0.55f) c = new Color32(255, 255, 255, 255);
                else if (d <= 0.8f) c = new Color32(230, 30, 40, 255);
                else if (d <= 1f) c = new Color32(120, 0, 10, (byte)(255 * (1f - (d - 0.8f) / 0.2f)));
                else c = new Color32(0, 0, 0, 0);
                pixels[y * TextureSize + x] = c;
            }
        }
        tex.SetPixels32(pixels);
        tex.Apply(false, true);

        cachedSprite = Sprite.Create(tex, new Rect(0, 0, TextureSize, TextureSize), new Vector2(0.5f, 0.5f), TextureSize);
        cachedSprite.name = "HitboxSprite";
        return cachedSprite;
    }
}

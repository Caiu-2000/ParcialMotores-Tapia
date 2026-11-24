using UnityEngine;

// MEJORAS PLAYER (script nuevo)
// Límites visibles de la cámara principal sobre el plano z = 0.
// Funciona con cámara en perspectiva (la del combate) y ortográfica.
public static class ScreenBounds
{
    private static Camera cachedCamera;
    private static Vector2 min, max;
    private static int lastFrame = -1;

    public static Camera Cam
    {
        get
        {
            if (cachedCamera == null) cachedCamera = Camera.main;
            return cachedCamera;
        }
    }

    public static Vector2 Min { get { Refresh(); return min; } }
    public static Vector2 Max { get { Refresh(); return max; } }
    public static Vector2 Size { get { Refresh(); return max - min; } }

    /// <summary>Fracción de la altura de pantalla tapada abajo por el HUD (0..1).</summary>
    public static float BottomHudFraction { get; set; }

    /// <summary>
    /// Busca la franja de abajo del HUD (inGameUI/Panel, la de BOCA JUNIORS) para que el jugador
    /// no se meta debajo. Si no la encuentra, el jugador puede usar toda la pantalla.
    /// </summary>
    public static void DetectBottomHud(string path = "inGameUI/Panel")
    {
        BottomHudFraction = 0f;
        GameObject panel = GameObject.Find(path);
        if (panel == null || panel.transform is not RectTransform rect) return;

        Canvas.ForceUpdateCanvases();
        Vector3[] corners = new Vector3[4];
        rect.GetWorldCorners(corners); // Canvas en modo Overlay: coordenadas en píxeles
        float top = Mathf.Max(corners[1].y, corners[2].y);
        BottomHudFraction = Mathf.Clamp01(top / Mathf.Max(1f, Screen.height));
    }

    /// <summary>Límite inferior jugable (por encima del HUD de abajo).</summary>
    public static float PlayMinY { get { Refresh(); return min.y + (max.y - min.y) * BottomHudFraction; } }

    private static void Refresh()
    {
        if (lastFrame == Time.frameCount && min != max) return;
        lastFrame = Time.frameCount;

        Camera cam = Cam;
        if (cam == null) return;

        float depth = DepthToGamePlane(cam);
        min = cam.ViewportToWorldPoint(new Vector3(0f, 0f, depth));
        max = cam.ViewportToWorldPoint(new Vector3(1f, 1f, depth));
    }

    private static float DepthToGamePlane(Camera cam)
    {
        float depth = Mathf.Abs(cam.transform.position.z);
        return depth < 0.01f ? 10f : depth;
    }

    /// <summary>margin positivo agranda el área; negativo la achica.</summary>
    public static bool Contains(Vector2 point, float margin = 0f)
    {
        Refresh();
        return point.x >= min.x - margin && point.x <= max.x + margin &&
               point.y >= min.y - margin && point.y <= max.y + margin;
    }

    public static Vector2 Clamp(Vector2 point, float inset = 0f)
    {
        Refresh();
        return new Vector2(
            Mathf.Clamp(point.x, min.x + inset, max.x - inset),
            Mathf.Clamp(point.y, min.y + inset, max.y - inset));
    }

    /// <summary>Como Clamp, pero sin dejar que el jugador se meta debajo del HUD inferior.</summary>
    public static Vector2 ClampToPlayArea(Vector2 point, float inset = 0f)
    {
        Vector2 clamped = Clamp(point, inset);
        clamped.y = Mathf.Max(clamped.y, PlayMinY + inset);
        return clamped;
    }

    public static Vector3 ViewportToWorld(Vector2 viewport)
    {
        Refresh();
        return new Vector3(Mathf.LerpUnclamped(min.x, max.x, viewport.x), Mathf.LerpUnclamped(min.y, max.y, viewport.y), 0f);
    }

    public static Vector2 ScreenToWorld(Vector2 screenPosition)
    {
        Camera cam = Cam;
        if (cam == null) return screenPosition;
        return cam.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, DepthToGamePlane(cam)));
    }
}

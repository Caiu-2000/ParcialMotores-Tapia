using UnityEngine;
using UnityEngine.EventSystems;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

// MEJORAS PLAYER
// Versión mejorada del JoyController original (el original quedó comentado en Scripts/JoyController.cs).
// Usa el mismo GUID que el original, así el ControlInput de la escena sigue funcionando sin tocar nada.
//
// Qué cambia:
//  - Movimiento relativo 1:1: el personaje se mueve exactamente lo mismo que el dedo.
//    Antes "perseguía" al dedo a velocidad fija y por eso se sentía con retraso.
//  - Se mueve en el mismo momento que llega el toque (OnDrag), no en el Update siguiente.
//  - Ignora un segundo dedo (por ejemplo, el que aprieta el botón de bomba).
//  - Teclado para probar en PC: flechas/WASD para mover, Shift para ir lento, X o Espacio para la bomba.
public class JoyController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
                             IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private const int NoPointer = int.MinValue;

    [Tooltip("Cuánto se mueve el personaje por cada unidad que se mueve el dedo (1 = igual).")]
    [SerializeField, Range(0.5f, 2.5f)] private float sensitivity = 1f;
    [SerializeField] private bool keyboardForTesting = true;

    private Player character;
    private int activePointer = NoPointer;
    private Vector2 lastWorld;

    public void SetPlayer(Player player)
    {
        character = player;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (activePointer != NoPointer) return;
        activePointer = eventData.pointerId;
        lastWorld = ScreenBounds.ScreenToWorld(eventData.position);
    }

    public void OnBeginDrag(PointerEventData eventData) { }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != activePointer || character == null) return;

        Vector2 world = ScreenBounds.ScreenToWorld(eventData.position);
        character.MoveBy((world - lastWorld) * sensitivity);
        lastWorld = world;
    }

    public void OnEndDrag(PointerEventData eventData) { }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == activePointer) activePointer = NoPointer;
    }

    private void OnDisable()
    {
        activePointer = NoPointer;
    }

    private void Update()
    {
#if ENABLE_INPUT_SYSTEM
        if (!keyboardForTesting || character == null) return;
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        Vector2 dir = Vector2.zero;
        if (kb.leftArrowKey.isPressed || kb.aKey.isPressed) dir.x -= 1f;
        if (kb.rightArrowKey.isPressed || kb.dKey.isPressed) dir.x += 1f;
        if (kb.downArrowKey.isPressed || kb.sKey.isPressed) dir.y -= 1f;
        if (kb.upArrowKey.isPressed || kb.wKey.isPressed) dir.y += 1f;

        character.IsFocused = kb.leftShiftKey.isPressed || kb.rightShiftKey.isPressed;
        if (dir != Vector2.zero) character.Move(dir.normalized);

        if (kb.xKey.wasPressedThisFrame || kb.spaceKey.wasPressedThisFrame)
            EventManager<InputEvents>.Publish<int>(InputEvents.BombPressed, 0);
#endif
    }
}

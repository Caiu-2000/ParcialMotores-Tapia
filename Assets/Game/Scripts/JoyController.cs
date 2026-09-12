using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class JoyController : MonoBehaviour,  IDragHandler , IBeginDragHandler, IEndDragHandler 
{
    [SerializeField] private Image _image;

    Vector2 firstPosition = new Vector2();

    Vector2 BeguinPosition;

   Player character;
    private bool _dragging = false;
    Vector2 direction;

    private void Start()
    {
         firstPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        BeguinPosition = eventData.position;
        _dragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        direction = (eventData.position - BeguinPosition).normalized;
        character.Move(direction);
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = firstPosition;
        _dragging = false;
       
    }

    private void Update()
    {
        if (_dragging) { character.Move(direction); }
    }

    public void SetPlayer(Player player)
    {
        print("Se mando player");
        character = player;
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class JoyController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    [SerializeField] private Image _image;

    Vector2 firstPosition = new Vector2();

    Vector3 BeguinPosition;
    Vector3 offset;
    Player character;
    private bool _dragging = false;
   

    Vector3 currentposition;


    private void Start()
    {
        firstPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        BeguinPosition = GetWorldPosition(eventData.position);
        offset = character.transform.position - BeguinPosition;
        _dragging = true;
        
    
    }
    
    public void OnDrag(PointerEventData eventData)
    {
      currentposition = GetWorldPosition(eventData.position);
        

    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        _dragging = false;

    }

    private void Update()
    {
        if (_dragging) 
        {
            character.MoveTowards(currentposition + offset); 
        
        }
    }

    public void SetPlayer(Player player)
    {
        print("Se mando player");
        character = player;
    }

    private Vector2 GetWorldPosition(Vector2 screenposition)
    {
        // el 10 es por la distancia a la que esta la camara por defecto en unity
        Vector3 vector = new Vector3(screenposition.x, screenposition.y, 10);



        return Camera.main.ScreenToWorldPoint(vector);
    }

}

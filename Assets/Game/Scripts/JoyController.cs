using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class JoyController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
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
        print("Empezo");
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("Me draguean");
        transform.position = eventData.position;
        direction = (eventData.position - BeguinPosition).normalized;
        character.Move(direction);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("Termino el drag");
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

/*
    [SerializeField] private Image _image;

    Vector2 firstPosition = new Vector2();

    Vector2 BeguinPosition;

    Player character;
    private bool _dragging = false;
    Vector2 direction;





    [SerializeField] private float pauseDelay = 0.5f; // Time in seconds to qualify as a "pause"

    private float idleTimer;
    private bool isDragging;
    private bool isPaused;

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        isPaused = false;
        idleTimer = 0f;
        Debug.Log("Drag Started");
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 1. Move the UI object with the pointer
        transform.position = eventData.position;

        // 2. Check for Resume
        if (isPaused)
        {
            isPaused = false;
            OnDragResumed();
        }

        // 3. Reset the idle timer because movement happened
        idleTimer = 0f;
    }

    private void Update()
    {
        // 4. Increment timer only while finger is down and not already paused
        if (isDragging && !isPaused)
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= pauseDelay)
            {
                isPaused = true;
                OnDragPaused();
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        isPaused = false;
        Debug.Log("Drag Ended");
    }

    private void OnDragPaused()
    {
        Debug.Log("Drag PAUSED (Finger is still on screen!)");
        // Add your custom logic here (e.g., show a tooltip, expand a menu)
    }

    private void OnDragResumed()
    {
        Debug.Log("Drag RESUMED!");
        // Add your custom logic here (e.g., hide the tooltip)
    }
    public void SetPlayer(Player player)
    {
        print("Se mando player");
        character = player;
    }
}*/
using UnityEngine;

public class NativeAndroidInputs : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Flecha para atras del android
        }
        if (Input.GetKeyDown(KeyCode.Menu))
        {
            // Boton de aplicacion de android . boton circular que casi ni se usa
        }

        if (Input.touchCount <= 0) return; // Con esto leemos cuantos dedos tocan la pantalla, si es 0 no hacemos nada

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i); // Obtenemos el toque en la posicion i
            if (touch.phase == TouchPhase.Began)
            {
                // Se ha iniciado un toque
                // Aca se puede hacer lo de tirar un raycast al mundo para ver que tocas
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // Se ha movido un toque
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Se ha levantado un toque
            }
        }
        //Input.touches[i] lista de los toques
        //Input.touchSupported devuelve true si el dispositivo soporta toques
        //Input.multiTouchEnabled devuelve true si el dispositivo soporta multitouch

    }
}

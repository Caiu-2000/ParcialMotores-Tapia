using System.Collections;
using UnityEngine;

public class CobraBomb : Bomb
{
    [SerializeField] float MoveTime = 1.0f;
    [SerializeField] float Distance = 3.0f;  
    public override void Activate()
    {
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        float elapsedtime = 0;
        transform.position = player.transform.position;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, Distance ,0);
        
        while (elapsedtime < MoveTime)
        {
            elapsedtime += Time.deltaTime;

            float t = elapsedtime / MoveTime;

            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, t);

            transform.position = newPosition;

            yield return null;
        }
        elapsedtime = 0;
        startPosition = transform.position;
     
        while (elapsedtime < MoveTime)
        {
            elapsedtime += Time.deltaTime;

            float t = elapsedtime / MoveTime;

            Vector3 newPosition = Vector3.Lerp(startPosition, player.transform.position, t);

            transform.position = newPosition;

            yield return null;
        }

        //Esto para que se salga de la pantalla
        transform.position = new Vector3(1000,1000,0);
    }
}


using UnityEngine;

public static class ExtraMath
{
    public static float ScuareDistance(Vector2 First , Vector2 Second)
    {
        Vector2 distance = First - Second;
     
        return distance.sqrMagnitude;


    }
}
using UnityEngine;

public static class MapBorderChecker
{
    static float minX = -12.5f;
    static float maxX = 12.5f;
    static float minZ = -8f;
    static float maxZ = 8f;

    public static bool IsPositionInsideMap(Vector3 pos)
    {
        bool isInside = true;
        if (pos.x < minX || pos.x > maxX)
        {
            isInside = false;
        }

        if (pos.z < minZ || pos.z > maxZ)
        {
            isInside = false;
        }
        return isInside;
    }

    public static Vector3 GetRandomPointInMap()
    {
        return new Vector3(
               Random.Range(minX+1, maxX -1),
               0.5f,
               Random.Range(minZ +1, maxZ -1)
           );
    }
}

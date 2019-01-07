using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    public  float minHorizontal, maxHorizontal;
    public float minVertical, maxVertical;

    private float positionX;
    private float positionY;

    public Vector3 GetRandomPosition(Vector3 position)
    {
        position.Set(Random.Range(minHorizontal, maxHorizontal), position.y,
        Random.Range(minVertical, maxVertical));
        return position;
    }
}
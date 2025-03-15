using UnityEngine;

public struct ParatrooperData
{
    public int instanceID;
    public float distance;
    public ParatrooperController controller;
    public Collider2D collider;

    public ParatrooperData(int id, float xPosition, ParatrooperController paratrooperController, Collider2D trooperCollider)
    {
        instanceID = id;
        distance = xPosition;
        controller = paratrooperController;
        collider = trooperCollider;
    }
}

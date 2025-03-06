using System.Collections.Generic;
using UnityEngine;

public class TroopController : MonoBehaviour
{
    public Collider2D trooperCollider;

    public Dictionary<int, float> leftTroops = new Dictionary<int, float>();
    public Dictionary<int, float> rightTroops = new Dictionary<int, float>();
    private bool leftTroopsReady = false;
    private bool rightTroopsReady = false;
    public enum Sides { left, right };

    private Rigidbody2D rb;
    //private bool isMoving = false;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //Check if left and right side troops are ready to move.
        while (!leftTroopsReady || !rightTroopsReady)
            TroopsSideReady();
    }

    //private void UpdateTroopsDistance()
    //{

    //}

    private void TroopsSideReady()
    {
        if (leftTroops.Count > 3 && leftTroopsReady == false)
        {
            leftTroopsReady = true;
            MoveTroopForSide(Sides.left);
        }
        if (rightTroops.Count > 3 && rightTroopsReady == false)
        {
            rightTroopsReady = true;
            MoveTroopForSide(Sides.right);
        }

    }

    public void MoveTroopForSide(Sides side)
    {
        Debug.Log(side);
    }

}

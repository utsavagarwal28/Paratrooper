using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TroopController : MonoBehaviour
{
    //public Collider2D trooperCollider;

    public List<ParatrooperData> leftTroops = new List<ParatrooperData>();
    public List<ParatrooperData> rightTroops = new List<ParatrooperData>();
    //public Dictionary<int, float> leftTroops = new Dictionary<int, float>();
    //public Dictionary<int, float> rightTroops = new Dictionary<int, float>();
    private bool leftTroopsReady = false;
    private bool rightTroopsReady = false;
    public int movingTroop;
    public enum Sides { left, right };

    //private Rigidbody2D rb;
    //private bool isMoving = false;




    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //Check if left and right side troops are ready to move.
        if (!leftTroopsReady || !rightTroopsReady)
            TroopsSideReady();
    }

    private void UpdateTroopsDistance(int instanceID, float newValue)
    {
        if (rightTroops.ContainsKey(instanceID))
        {
            rightTroops[instanceID] = newValue;
            Debug.Log($"Updated rightTroops[{instanceID}] = {newValue}");
        }
        else if (leftTroops.ContainsKey(instanceID))
        {
            leftTroops[instanceID] = newValue;
            Debug.Log($"Updated leftTroops[{instanceID}] = {newValue}");
        }
        else
        {
            Debug.LogWarning($"Instance ID {instanceID} not found in either dictionary.");
        }
    }

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
        Debug.Log($"Move {side}");

        //Assign dictionary according to the side.
        Dictionary<int, float> troopDictionary = (side == Sides.left) ? leftTroops : rightTroops;

        //Move First Troop
        int minInstanceID = troopDictionary.Aggregate((x, y) => x.Value < y.Value ? x : y).Key;

        //Movement for point 2->1


    }

    private void MoveTroopTowardsThePyramid(Sides side, int troopInstanceID)
    {

    }

}

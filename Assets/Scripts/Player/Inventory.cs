using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //array to hold the number of each item in the player's inventory
    //each item type has an index which we can use to hold the number of them the player has
    //

    [SerializeField]
    private int[] Items;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUpItem(int itemIndex)
    {
        Items[itemIndex]++;
    }

    public bool UseItem(int itemIndex, bool drop)
    {
        if(Items[itemIndex] > 0)
        {

            if (drop == true)
            {
                Items[itemIndex]--;
            }
            return true;
        }
        else
        {
            return false;
        }               
    }

}

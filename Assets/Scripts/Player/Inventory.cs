using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //array to hold the number of each item in the player's inventory
    //each item type has an index which we can use to hold the number of them the player has
    //
    //index 0: magnifying glass
    //1: necklaces
    //2: ocarinas
    //3: books
    //4: spoons

    [SerializeField]
    private int[] Items;

    public InventoryUI myInventoryUI;

    // Start is called before the first frame update
    void Start()
    {
        myInventoryUI.UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUpItem(int itemIndex)
    {
        if(itemIndex >= Items.Length)
        {
            Debug.Log("error, invalid item id");
            return;
        }

        Items[itemIndex]++;
        myInventoryUI.UpdateUI();
    }

    public bool UseItem(int itemIndex, bool drop, int numberRequired)
    {
        if(Items[itemIndex] >= numberRequired)
        {

            if (drop == true)
            {
                Items[itemIndex]--;
            }
            myInventoryUI.UpdateUI();
            return true;
        }
        else
        {
            return false;
        }               
    }

}

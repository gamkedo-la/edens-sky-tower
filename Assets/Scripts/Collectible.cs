using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int CollectibleType;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision registered!");

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("picked up collectible " + CollectibleType);
            collision.gameObject.GetComponent<Inventory>().PickUpItem(CollectibleType);

            //do something here with the object. Maybe destroy or put it somewhere else, depending on the needs of the rest of the game
        }

    }


}

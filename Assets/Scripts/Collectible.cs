using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Tooltip("0: book \n 1: chalice \n 2: mug \n 3: hammer \n 4: pencil \n 5: spoon \n 6: wrench \n 7: flute")]
    public int CollectibleType;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.05f, 0.05f, 0.05f, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision registered!");

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("picked up collectible " + CollectibleType);
            collision.gameObject.GetComponent<Inventory>().PickUpItem(CollectibleType);

            //do something here with the object. Maybe destroy or put it somewhere else, depending on the needs of the rest of the game
            Destroy(gameObject);
        }

    }


}

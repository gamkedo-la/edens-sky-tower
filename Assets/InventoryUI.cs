using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Text[] TextBoxes;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI(int[] numbers)
    {
        for(int i = 0; i < numbers.Length; i++)
        {
            TextBoxes[i].text = numbers[i].ToString();
        }



    }
}

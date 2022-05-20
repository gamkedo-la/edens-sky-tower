using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Text TabletCount;
    public Text StoryText;
    public Image[] keys;
    public Image[] invItems;
    public Text[] invCount;

    bool open = false;

    public float OpenPos;
    public float ClosePos;

    public float AnimSpeed = 1;

    private float A;
    private float B;
    private float T = 0;

    public GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        A = ClosePos;
        B = ClosePos;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.Lerp(A, B, T), 30);
        T += Time.unscaledDeltaTime * AnimSpeed;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenClose();
        }
    }

    public void UpdateUI()
    {
        string revealedStory = " ";
        if(PlayerPrefs.GetInt("seenTablet1", 0) == 1)
        {
            revealedStory += " I have seen tablet 1";
        } else
        {
            revealedStory += "don't have it yet";
        }

        if (PlayerPrefs.GetInt("seenTablet2", 0) == 1)
        {
            revealedStory += " I have seen tablet 2";
        }
        else
        {
            revealedStory += "don't have it yet";
        }

        if (PlayerPrefs.GetInt("seenTablet3", 0) == 1)
        {
            revealedStory += " I have seen tablet 3";
        }
        else
        {
            revealedStory += "don't have it yet";
        }

        if (PlayerPrefs.GetInt("seenTablet4", 0) == 1)
        {
            revealedStory += " I have seen tablet 4";
        }
        else
        {
            revealedStory += "don't have it yet";
        }

        if (PlayerPrefs.GetInt("seenTablet5", 0) == 1)
        {
            revealedStory += " I have seen tablet 5";
        }
        else
        {
            revealedStory += "don't have it yet";
        }

        if (PlayerPrefs.GetInt("seenTablet6", 0) == 1)
        {
            revealedStory += " I have seen tablet 6";
        }
        else
        {
            revealedStory += "don't have it yet";
        }

        if (PlayerPrefs.GetInt("seenTablet7", 0) == 1)
        {
            revealedStory += " I have seen tablet 7";
        }
        else
        {
            revealedStory += "don't have it yet";
        }

        StoryText.text = revealedStory;
        TabletCount.text = "3/7";
        bool showKey = (PlayerPrefs.GetInt("usedKey1", 0) + PlayerPrefs.GetInt("holdKey1", 0)) >= 1;
        keys[0].enabled = showKey;
        showKey = (PlayerPrefs.GetInt("usedKey2", 0) + PlayerPrefs.GetInt("holdKey2", 0)) >= 1;
        keys[1].enabled = showKey;
        showKey = (PlayerPrefs.GetInt("usedKey3", 0) + PlayerPrefs.GetInt("holdKey3", 0)) >= 1;
        keys[2].enabled = showKey;
        showKey = (PlayerPrefs.GetInt("usedKey4", 0) + PlayerPrefs.GetInt("holdKey4", 0)) >= 1;
        keys[3].enabled = showKey;

        for(int i = 0; i < invItems.Length; i++)
        {
            int howMany = PlayerPrefs.GetInt("invNum" + i, 0);
            invItems[i].enabled = howMany > 0;
            invCount[i].text = "" + howMany; 
        }


    }

    public void OpenClose()
    {
        UpdateUI();
        if(open == true)
        {
            A = OpenPos;
            B = ClosePos;
            GM.UnpauseGame();
        }
        else
        {
            A = ClosePos;
            B = OpenPos;
            GM.PauseGame();
        }
        if (T < 1)
        {
            T = 1 - T;
        }
        else
        {
            T = 0;
        }
        open = !open;
    }
}

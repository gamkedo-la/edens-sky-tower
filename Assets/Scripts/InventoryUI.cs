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
            revealedStory += " The city of Eden is the core of Earth, the source of the purest water and the strongest life energy from which all living things depend on to thrive. With Eden, plants blossom in every direction and color, creatures of all kin come forth as real.The city was filled with humans too…They were tasked with being keepers of creation and living in harmony with each other. But time passed... ";
        } else
        {
            revealedStory += "(Missing Story Portion #1)";
        }

        if (PlayerPrefs.GetInt("seenTablet2", 0) == 1)
        {
            revealedStory += " Their way was lost, becoming self-centered. They turned against each other and disregarded nature as a resource to be consumed and other humans to be used. Amidst a world-breaking earthquake, emptying its streets, the Tower deemed human’s worthy no more. Fragmenting and breaking the Tower took the city and rose... ";
        }
        else
        {
            revealedStory += "(Missing Story Portion #2)";
        }

        if (PlayerPrefs.GetInt("seenTablet3", 0) == 1)
        {
            revealedStory += " Eden was no longer within human reach. Humans were left to face the unabashed consequences of their new ways. A world devoid of life’s source. An Earth that would now crumble and rust. The tower now floating amidst a sea of clouds starts to undo what humans have done. The fragmented floating city is now a haven, creatures and plants are created anew, the Tower its keeper once more... ";
        }
        else
        {
            revealedStory += "(Missing Story Portion #3)";
        }

        if (PlayerPrefs.GetInt("seenTablet4", 0) == 1)
        {
            revealedStory += " It is only after a millennia of being in the sky that the prophecy of old emerges and unfolds. A being from the tower, a Self on its own, emerges to deliver upon humanities soul. The Self is tasked with assembling the shattered tower and bringing together the city once more...";
        }
        else
        {
            revealedStory += "(Missing Story Portion #4)";
        }

        if (PlayerPrefs.GetInt("seenTablet5", 0) == 1)
        {
            revealedStory += " The Self, comes to know about human’s past. A history filled with greatness drowned by selfishness. For the prophecy to fulfill the self must decide. For the Tower to go back, the self must sacrifice.The self must give up what he has learned, encountered, and lived. All memories to be emptied, his body turned a shell.He...will be gone, but a new opportunity would come...";
        }
        else
        {
            revealedStory += "(Missing Story Portion #5)";
        }

        if (PlayerPrefs.GetInt("seenTablet6", 0) == 1)
        {
            revealedStory += " A chance for humans to enter Eden once more relies on a decision to be made, as the prophecy foretold. As humans have heard, as some believed, someone could pay the price for their horrible deeds. Deeds to be forgotten so Eden can be lowered, a city no longer in the skies, its gates once more opened...";
        }
        else
        {
            revealedStory += "(Missing Story Portion #6)";
        }

        if (PlayerPrefs.GetInt("seenTablet7", 0) == 1)
        {
            revealedStory += " Amanu, the Self, finally learning his name, the Last Key is now ready to be. He is now worthy and ready to decide if his journey is to end here. He must ponder on humanity and the sacrifice that is he.Are they worthy of such creation? They already destroyed it once.But also consider, is nature complete if humans are not?...Questions to consider for the Last Key, his last decision to be made where he first saw a tree.";
        }
        else
        {
            revealedStory += "(Missing Story Portion #7)";
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

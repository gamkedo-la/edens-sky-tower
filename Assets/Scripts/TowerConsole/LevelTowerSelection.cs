using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTowerSelection : MonoBehaviour
{
    private bool triggering;

    public Text toolTipText;

    public bool UIElevButtonBottom = false;
    public bool UIElevButtonMiddle = false;
    public bool UIElevButtonTop = false;
    public GameObject TowerElevatorUI;
    public GameObject TowerElevatorButtonOne;
    public GameObject TowerElevatorButtonTwo;
    public GameObject TowerElevatorButtonThree;
    public GameObject SelectShardText;
    public GameObject TipText;
    public GameObject EndSceneWindowUI;
    public GameObject EndSceneQuestion;
    public GameObject EndSceneInputOne;
    public GameObject EndSceneInputTwo;
    public bool selectedLevel = false;
    public bool endSceneWindow = false;
    public bool noAnswer = true;
    public towerMonolithMove towerMonolithMove;
    public Player playerScript;


    void Start()
    {
        //toolTipText.gameObject.SetActive(false);
        TowerElevatorUI.SetActive(false);
        TowerElevatorButtonOne.SetActive(false);
        TowerElevatorButtonTwo.SetActive(false);
        TowerElevatorButtonThree.SetActive(false);
        SelectShardText.SetActive(false);
        EndSceneWindowUI.SetActive(false);
        EndSceneQuestion.SetActive(false);
        EndSceneInputOne.SetActive(false);
        EndSceneInputTwo.SetActive(false);

    }

    void Update()
    {
        if(triggering)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                print("Console has been activated.");
            }
        }

        if (PlayerPrefs.GetInt("usedKey1", 0) == 1)
        {
            TipText.SetActive(false);
            SelectShardText.SetActive(true);
            TowerElevatorButtonOne.SetActive(true);

            UIElevButtonBottom = true;
        }

        if (PlayerPrefs.GetInt("usedKey2", 0) == 1)
        {

            TowerElevatorButtonTwo.SetActive(true);
            UIElevButtonMiddle = true;
        }

        if (PlayerPrefs.GetInt("usedKey3", 0) == 1)
        {

            TowerElevatorButtonThree.SetActive(true);
            UIElevButtonTop = true;
        }

        if (PlayerPrefs.GetInt("usedKey4", 0) == 1)
        {
            Debug.Log("end scene window!0");
            TowerElevatorUI.SetActive(false);
            TipText.SetActive(false);
            SelectShardText.SetActive(false);
            
            if(noAnswer)
            {
                EndSceneQuestion.SetActive(true);
            }
            
            endSceneWindow = true;
            //TowerElevatorButtonThree.SetActive(true);
            //UIElevButtonTop = true;
        }


    }


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            if (PlayerPrefs.GetInt("usedKey4", 0) == 1)
            {
                EndSceneWindowUI.SetActive(true);
                if(playerScript.choseAnswerZ)
                {
                    towerMonolithMove.EndSceneMonolithRise();
                }                
            }




                if (selectedLevel == false)
            {
                TowerElevatorUI.SetActive(true);
            }
            


            //toolTipText.text = "Which Floor?\n";

            bool noKeys = true;

            if(PlayerPrefs.GetInt("usedKey1", 0) == 1)
            {

                toolTipText.text += "U: Floor One\n";
                noKeys = false;
            }

            if (PlayerPrefs.GetInt("usedKey2", 0) == 1)
            {

                toolTipText.text += "I: Floor Two\n";
                noKeys = false;
            }

            if (PlayerPrefs.GetInt("usedKey3", 0) == 1)
            {

                toolTipText.text += "O: Floor Three\n";
                noKeys = false;
            }

            if (noKeys)
            {
                toolTipText.text += "[Search for Keys]";
            }

            //toolTipText.gameObject.SetActive(true);
            triggering = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            UIElevButtonBottom = false;
            UIElevButtonMiddle = false;
            UIElevButtonTop = false;
            //toolTipText.gameObject.SetActive(false);
            triggering = false;

            TowerElevatorUI.SetActive(false);
            EndSceneWindowUI.SetActive(false);
            Debug.Log("end scene to turn off");
        }
    }

}
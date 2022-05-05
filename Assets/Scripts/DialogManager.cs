using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public Text DialogUI;
    public CanvasGroup Window;
    public GameManager GM;

    [SerializeField] private GameObject hintManager;
    [SerializeField] private TextMeshProUGUI hintManagerTMProTextbox;
    public string myHintText = "Press E to continue";

    public string[] CurrentDialog;

    private int bookmark = 0;
    private int CharMark = 0;

    public string CurrentLine = "";

    public bool DialogActive = false;

    public float TypingSpeed = .05f;
    private float TSReset; 

    // Start is called before the first frame update
    void Start()
    {
        TSReset = TypingSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        if(DialogActive == true)
        {
            Debug.Log("dialog active " + TypingSpeed);

            if (CharMark < CurrentLine.Length)
            {
                TypingSpeed -= Time.unscaledDeltaTime;

                if(TypingSpeed <= 0)
                {
                    DialogUI.text += CurrentLine[CharMark];
                    CharMark++;
                    TypingSpeed = TSReset;
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    DialogUI.text = CurrentLine;
                    CharMark = CurrentLine.Length;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    NextLine();
                }
            }
        }
    }

    public void StartDialog(string[] DialogEvent)
    {
        //if (!GameManager.dialogHintShown)
       // {
            hintManager.SetActive(true);
            hintManagerTMProTextbox.text = myHintText;
            hintManagerTMProTextbox.gameObject.SetActive(true);
            GameManager.dialogHintShown = true;
            GameManager.aboutToShowJumpHint = false;
       // }

        CurrentDialog = DialogEvent;
        bookmark = 0;

        DialogUI.text = "";

        Window.alpha = 1;
        Window.blocksRaycasts = true;
        Window.interactable = true;

        DialogActive = true;
        GM.PauseGame();
        NextLine();
    }

    private void NextLine()
    {
        if(bookmark < CurrentDialog.Length)
        {
            DialogUI.text = "";
            CurrentLine = CurrentDialog[bookmark];
            CharMark = 0;
            TypingSpeed = 0;
            bookmark++;
        }
        else
        {
            Window.alpha = 0;
            Window.blocksRaycasts = false;
            Window.interactable = false;

            DialogActive = false;
            GM.UnpauseGame();
            hintManager.SetActive(false);
            /*if (GameManager.aboutToShowJumpHint)
            {
                GM.PauseGame();
                hintManagerTMProTextbox.text = "Now go left and jump up the platforms to the obelisk. \nSpace bar to jump. E to continue.";
                hintManager.SetActive(true);
                GameManager.aboutToShowJumpHint = false;
            } */
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    // this is what handles the game's dialogue from NPCs
    public static DialogueSystem instance;
    void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        HideDialogueUI();
    }

    Dialogue currentDialogue;
    DialogueBranch currentBranch;
    int CurrentDialogueIteration;

    public void StartDialogue(Dialogue dialogue)
    {
        // we need to create a dialogue UI based on the dialogue given
        // we always start with the 0th index of the branch regardless of its ID
        currentDialogue = dialogue;
        DisplayDialogueBranch(dialogue.DialogueBranches[0]);
        
        ShowDialogueUI();
    }

    DialogueBranch FindDialogueBranchByID(int ID)
    {
        foreach (DialogueBranch branch in currentDialogue.DialogueBranches)
        {
            if (branch.BranchID == ID )
            {
                return branch;
            }
        }
        return null;
    }
    
    public GameObject UI_DialogueResponse;
    public GameObject UI_DialogueResponseHolder;
    public Button NextDialogueButton;

    void DisplayDialogueBranch(DialogueBranch branch)
    {
        currentBranch = branch;
        CurrentDialogueIteration = 0;
        ShowDialogueText(branch.Dialogue[0]);
        NextDialogueButton.interactable = true;
    }

    public void SelectDialogueResponse(DialogueResponse response)
    {
        // clear current responses
        foreach (Transform child in UI_DialogueResponseHolder.transform)
        {
            Destroy(child.gameObject);
        }
        if (response.EndsDialogue)
        {
            // we are done, close dialogue
            CloseDialogue();
        }
        else
        {
            DisplayDialogueBranch(FindDialogueBranchByID(response.GoToBranchID));
        }
    }

    public void NextDialogueText()
    {
        if(CurrentDialogueIteration + 1 >= currentBranch.Dialogue.Count)
        {
            if(currentBranch.EndsDialogue)
            {
                CloseDialogue();
            }
            else
            {
                NextDialogueButton.interactable = false;
                // we need to instantiate a response for each response in the branch
                foreach (DialogueResponse response in currentBranch.DialogueResponses)
                {
                    GameObject UI = Instantiate(UI_DialogueResponse, UI_DialogueResponseHolder.transform);
                    UI.GetComponent<DialogueResponseUI>().Setup(response);
                }
            }
        }
        else
        {
            CurrentDialogueIteration++;
            ShowDialogueText(currentBranch.Dialogue[CurrentDialogueIteration]);
        }
        
    }

    public TMP_Text UI_DialogueText; // the text box you are looking at
    void ShowDialogueText(string dialogueText)
    {
        UI_DialogueText.text = dialogueText;
    }

    public void CloseDialogue()
    {
        HideDialogueUI();
        // clear data
        CurrentDialogueIteration = 0;
        currentBranch = null;
    }

    public CanvasGroup cgDialogueUI;

    void ShowDialogueUI()
    {
        cgDialogueUI.alpha = 1;
        cgDialogueUI.interactable = true;
        cgDialogueUI.blocksRaycasts = true;
    }

    void HideDialogueUI()
    {
        cgDialogueUI.alpha = 0;
        cgDialogueUI.interactable = false;
        cgDialogueUI.blocksRaycasts = false;
    }

    public Dialogue TestDialogue;

    public void UI_TestDialogue()
    {
        StartDialogue(TestDialogue);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class Dialogue : ScriptableObject
{
    // a dialogue is an interaction with an Interactive
    // it starts with the 0th branch, each branch has response nodes
    public List<DialogueBranch> DialogueBranches = new List<DialogueBranch>();
}

[System.Serializable]
public class DialogueResponse
{
    // this is what the player clicks
    public string ResponseText;
    public bool EndsDialogue; // if true, this response ends the dialogue
    public int GoToBranchID; // when this response is selected, this sends up to the branch ID listed here
}

[System.Serializable]
public class DialogueBranch
{
    // this is what is displayed from the NPC
    public int BranchID;
    public List<string> Dialogue = new List<string>(); // strings in order, on the last one, we present the list of dialogue options
    public List<DialogueResponse> DialogueResponses = new List<DialogueResponse>(); // the responses for this node
    public bool EndsDialogue; // if true, we don't show a response, we just end
}
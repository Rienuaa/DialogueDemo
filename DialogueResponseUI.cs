using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueResponseUI : MonoBehaviour
{
    DialogueResponse CurrentResponse;
    public TMP_Text dialogueText;
    public void Setup(DialogueResponse response)
    {
        CurrentResponse = response;
        dialogueText.text = response.ResponseText;
    }
    
    public void SendDialogueResponse()
    {
        DialogueSystem.instance.SelectDialogueResponse(CurrentResponse);
    }
}

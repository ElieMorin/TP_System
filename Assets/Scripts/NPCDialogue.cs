using UnityEngine;

[CreateAssetMenu(fileName = "NPCDialogue", menuName = "Scriptable Objects/NPCDialogue")]
public class NPCDialogue : ScriptableObject
{
    public string OriginalQuestion;

    public string Option1;
    public string response1;

    public string Option2;
    public string response2;
}

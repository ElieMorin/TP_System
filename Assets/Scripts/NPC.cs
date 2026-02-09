using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private NPCDialogue m_Dialogues;
    [SerializeField] private UI m_UI;

    public void SetDialogue()
    {
        m_UI.ChangeText(m_Dialogues.OriginalQuestion);
        m_UI.GetOptions(m_Dialogues.Option1, m_Dialogues.Option2);
    }

    public void SetResponse(int choice)
    {
        if(choice == 1)
        {
            m_UI.ChangeText(m_Dialogues.response1);
        }
        else
        {
            m_UI.ChangeText(m_Dialogues.response2);
        }
    }
}

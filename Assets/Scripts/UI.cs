using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private NPC m_NPC;
    [SerializeField] private TextMeshProUGUI m_Text;
    [SerializeField] private TextMeshProUGUI m_ButtonText1, m_Buttontext2;
    [SerializeField] private Button m_Button1, m_Button2;
    [SerializeField] private PlayerController m_PlayerController;

    public void ChangeText(string dialogue)
    {
        m_Text.gameObject.SetActive(true);
        m_Text.text = dialogue;
    }

    public void GetOptions(string option1, string option2)
    {
        m_Button1.gameObject.SetActive(true);
        m_ButtonText1.text = option1;
        m_Button2.gameObject.SetActive(true);
        m_Buttontext2.text = option2;
    }


    public void Click(int optionPicked)
    {
        m_NPC.SetResponse(optionPicked);
        m_Button1.gameObject.SetActive(false);
        m_Button2.gameObject.SetActive(false);
    }

    public void DialogueInteract()
    {
        m_PlayerController.ResetControls();
        m_Text.gameObject.SetActive(false);
    }

}

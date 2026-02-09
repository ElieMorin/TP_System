using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_RunningSpeed, m_WalkingSpeed;
    [SerializeField] private float m_JumpForce;
    [SerializeField] private VitalsManager m_VitalsManager;
    [SerializeField] private UI m_UI;

    private float m_PlayerSpeed;
    private bool m_IsGrounded;
    private bool m_CanTalk;

    private Rigidbody m_Rigidbody;
    private Vector3 m_MovementInput;
    private List<GameObject> m_NearbyNPC = new List<GameObject>();

    public bool m_IsTalking;

    private Action m_CurrentInput;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_PlayerSpeed = m_WalkingSpeed;

        m_CurrentInput = Movement;
    }

    void Update()
    {
        if (!m_IsTalking)
        {
            LookAround();
            Interact();
        }
        m_CurrentInput?.Invoke();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift) && m_VitalsManager.m_CanRun)
        {
            m_PlayerSpeed = m_RunningSpeed;
            m_VitalsManager.m_IsRunning = true;
        }
        else
        {
            m_PlayerSpeed = m_WalkingSpeed;
            m_VitalsManager.m_IsRunning = false;
        }

        m_MovementInput = new Vector3
            (Input.GetAxis("Horizontal") * m_PlayerSpeed,
            m_Rigidbody.linearVelocity.y,
            Input.GetAxis("Vertical") * m_PlayerSpeed);

        m_Rigidbody.linearVelocity = m_MovementInput;

        if (Input.GetKeyDown(KeyCode.Space) && m_IsGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        m_Rigidbody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
    }

    void LookAround()
    {
        Vector3 movementInput = new Vector3(m_MovementInput.x, 0, m_MovementInput.z);
        if (movementInput != Vector3.zero)
        {
            var relative = (transform.position + movementInput) - transform.position;
            var rotation = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = rotation;
        }
    }

    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && m_CanTalk && !m_IsTalking)
        {
            m_NearbyNPC[0].GetComponent<NPC>().SetDialogue();
            m_CurrentInput = TalkingControls;
            m_IsTalking = true;
        }
    }

    void TalkingControls()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            m_UI.DialogueInteract();
        }
    }

    public void ResetControls()
    {
        m_IsTalking = false;
        m_CurrentInput = Movement;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_IsGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            m_IsGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC") && !m_NearbyNPC.Contains(other.gameObject))
        {
            m_NearbyNPC.Add(other.gameObject);
            m_CanTalk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            m_NearbyNPC.Remove(other.gameObject);
            if (m_NearbyNPC.Count == 0)
            {
                m_CanTalk = false;
            }
        }
    }
}

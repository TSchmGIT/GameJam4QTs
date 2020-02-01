using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    #region Unity References

    public CharacterController m_CharacterController;
    public float m_MovementSpeed = 5.0f;
    public bool m_InMachineRange = false;
    public Item m_ItemToPick = null;
    public Item m_HeldItem = null;
    public string m_VerticalAxisName = null;
    public string m_HorizontalAxisName = null;
    public KeyCode m_InteractKey = KeyCode.Space;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {

        //Horizontal and Vertical movement of the player character
        float hor = Input.GetAxis(m_HorizontalAxisName);
        Vector3 horVector = Vector3.right * hor;

        float ver = Input.GetAxis(m_VerticalAxisName);
        Vector3 verVector = Vector3.forward * ver;

        Vector3 delta = horVector + verVector;
        Vector3 deltaNormalized = delta.normalized;
        Vector3 movementVector = deltaNormalized * m_MovementSpeed;
        m_CharacterController.Move(movementVector * Time.deltaTime);

        //Rotate the player chracter in the direction of movement
        if (delta != Vector3.zero)
        {
            transform.forward = delta;
        }

        // Interact with a Machine when in range and holding an item
        if (m_InMachineRange == true)
        {
            if (Input.GetKeyDown(m_InteractKey))
            {
                if (m_HeldItem != null)
                {
                    Debug.Log("Execute Machine Interaction");
                }
            }
        }

        // Drop an item when holding one and not in range of a machine
        if (m_HeldItem != null)
        {
            if (Input.GetKeyDown(m_InteractKey))
            {
                if (m_InMachineRange == false)
                {
                    Debug.Log("Drop Item");
                    Rigidbody ItemRb = m_HeldItem.gameObject.GetComponent<Rigidbody>();
                    ItemRb.isKinematic = false;
                    m_HeldItem.transform.parent = this.transform.parent;
                    m_HeldItem = null;
                    return;
                }
            }
        }

        // Pick up an item when in range and not yet holding one
        if (m_ItemToPick != null)
        {
            if (m_HeldItem == null)
            {
                if (Input.GetKeyDown(m_InteractKey))
                {
                    Debug.Log("Pick Up Item");
                    m_HeldItem = m_ItemToPick;
                    Rigidbody ItemRb = m_HeldItem.gameObject.GetComponent<Rigidbody>();
                    ItemRb.isKinematic = true;
                    m_HeldItem.transform.parent = this.transform;
                    m_HeldItem.transform.localPosition = new Vector3(0f, -0.6f, 1.2f);
                }
            }
        }
    }

    // Check what trigger was entered and respond accordingly
    private void OnTriggerEnter(Collider other)
    {
        MachineInteraction Machine = other.GetComponent<MachineInteraction>();
        Item Item = other.GetComponent<Item>();

        if (Machine != null)
        {
            m_InMachineRange = true;
        }

        if (Item != null)
        {
            m_ItemToPick = Item;

        }
    }

    // Check what trigger was exited and respond accordingly
    private void OnTriggerExit(Collider other)
    {
        MachineInteraction Machine = other.GetComponent<MachineInteraction>();
        Item Item = other.GetComponent<Item>();

        if (Machine != null)
        {
            m_InMachineRange = false;
        }

        if (Item != null)
        {
            m_ItemToPick = null;
        }
        
    }
}

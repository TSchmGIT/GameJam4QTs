using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    #region Unity References

    public CharacterController m_CharacterController;
    public float m_MovementSpeed = 5.0f;

    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Horizontal and Vertical movement of the player character
        float hor = Input.GetAxis("HorizontalPlayer2");
        Vector3 horVector = Vector3.right * hor;

        float ver = Input.GetAxis("VerticalPlayer2");
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
    }
}

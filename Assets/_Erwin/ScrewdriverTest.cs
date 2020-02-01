using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrewdriverTest : MonoBehaviour
{

    #region Unity References

    public float m_MinTargetSize = 0.5f;
    public float m_MaxTargetSize = 2f;
    public float m_MinPinSpeed = 6f;
    public float m_MaxPinSpeed = 9f;
    public GameObject m_Pin = null;
    public GameObject m_Target = null;
    float m_TargetSize = 0.25f;
    float m_PinSpeed = 0.25f;
    bool m_PinGoesLeft = false;
    float m_LeftBound = -0.5f;
    float m_RightBound = 0.5f;
    float m_LeftTarget = -0.5f;
    float m_RightTarget = 0.5f;
    public Text m_ResultText = null;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_TargetSize = Random.Range(m_MinTargetSize, m_MaxTargetSize);
        m_PinSpeed = Random.Range(m_MinPinSpeed, m_MaxPinSpeed);
        Debug.Log("Target Size = " + m_TargetSize);
        Debug.Log("Pin Speed = " + m_PinSpeed);
        m_Target.transform.localScale = new Vector3(m_TargetSize, 1f, 1f);
        m_LeftTarget = m_TargetSize * m_LeftBound;
        m_RightTarget = m_TargetSize * m_RightBound;
    }

    // Update is called once per frame
    void Update()
    {


        // Pin Movement
        if (m_PinGoesLeft == false)
        {
            m_Pin.transform.Translate(Vector3.right * Time.deltaTime * m_PinSpeed);
        }

        if (m_Pin.transform.position.x > 6)
        {
            m_PinGoesLeft = true;
        }

        if (m_Pin.transform.position.x < -6)
        {
            m_PinGoesLeft = false;
        }

        if (m_PinGoesLeft == true)
        {
            m_Pin.transform.Translate(Vector3.left * Time.deltaTime * m_PinSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_PinSpeed = 0;
            CheckPlayerSuccess();
        }
    }

    void CheckPlayerSuccess()
    {
        if (m_Pin.transform.position.x > m_LeftTarget && m_Pin.transform.position.x < m_RightTarget)
        {
            m_ResultText.gameObject.SetActive(true);
            m_ResultText.text = "Success";
        }

        else
        {
            m_ResultText.gameObject.SetActive(true);
            m_ResultText.text = "Fail";
        }
        //success check
    }
}

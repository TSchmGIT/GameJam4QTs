using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatcherMinigame : MonoBehaviour
{

    #region Unity References

    public List<GameObject> m_Viruses;
    public GameObject m_Target = null;
    public GameObject m_UpPos = null;
    public GameObject m_RightPos = null;
    public GameObject m_DownPos = null;
    public GameObject m_LeftPos = null;
    int m_Index = 0;
    int m_IndexUp = 0;
    int m_IndexRight = 0;
    int m_IndexDown = 0;
    int m_IndexLeft = 0;
    GameObject pickedVirusTarget = null;
    GameObject pickedVirusUp = null;
    GameObject pickedVirusRight = null;
    GameObject pickedVirusDown = null;
    GameObject pickedVirusLeft = null;

    #endregion

    // Start is called before the first frame update
    void Start()
    {

        //Pick a virus to be the middle target
        m_Index = Random.Range(0, m_Viruses.Count);
        pickedVirusTarget = m_Viruses[m_Index];

        MeshRenderer rendererOfVirus = pickedVirusTarget.GetComponent<MeshRenderer>();
        var textureOfVirus = rendererOfVirus.material.GetTexture("_MainTex");
        MeshRenderer rendererOfTarget = m_Target.GetComponent<MeshRenderer>();
        rendererOfTarget.material.SetTexture("_MainTex", textureOfVirus);
        
        //Pick a virus to be the "up" option
        m_IndexUp = Random.Range(0, m_Viruses.Count);
        pickedVirusUp = m_Viruses[m_IndexUp];

        MeshRenderer rendererOfVirusUp = pickedVirusUp.GetComponent<MeshRenderer>();
        var textureOfVirusUp = rendererOfVirusUp.material.GetTexture("_MainTex");
        MeshRenderer rendererOfUp = m_UpPos.GetComponent<MeshRenderer>();
        rendererOfUp.material.SetTexture("_MainTex", textureOfVirusUp);

        m_Viruses.Remove(pickedVirusUp);

        //Pick a virus to be the "right" option
        m_IndexRight = Random.Range(0, m_Viruses.Count);
        pickedVirusRight = m_Viruses[m_IndexRight];

        MeshRenderer rendererOfVirusRight = pickedVirusRight.GetComponent<MeshRenderer>();
        var textureOfVirusRight = rendererOfVirusRight.material.GetTexture("_MainTex");
        MeshRenderer rendererOfRight = m_RightPos.GetComponent<MeshRenderer>();
        rendererOfRight.material.SetTexture("_MainTex", textureOfVirusRight);

        m_Viruses.Remove(pickedVirusRight);

        //Pick a virus to be the "down" option
        m_IndexDown = Random.Range(0, m_Viruses.Count);
        pickedVirusDown = m_Viruses[m_IndexDown];

        MeshRenderer rendererOfVirusDown = pickedVirusDown.GetComponent<MeshRenderer>();
        var textureOfVirusDown = rendererOfVirusDown.material.GetTexture("_MainTex");
        MeshRenderer rendererOfDown = m_DownPos.GetComponent<MeshRenderer>();
        rendererOfDown.material.SetTexture("_MainTex", textureOfVirusDown);

        m_Viruses.Remove(pickedVirusDown);

        //Pick a virus to be the "left" option
        pickedVirusLeft = m_Viruses[0];

        MeshRenderer rendererOfVirusLeft = pickedVirusLeft.GetComponent<MeshRenderer>();
        var textureOfVirusLeft = rendererOfVirusLeft.material.GetTexture("_MainTex");
        MeshRenderer rendererOfLeft = m_LeftPos.GetComponent<MeshRenderer>();
        rendererOfLeft.material.SetTexture("_MainTex", textureOfVirusLeft);

        Debug.Log(pickedVirusTarget);
        Debug.Log(pickedVirusUp);
        Debug.Log(pickedVirusRight);
        Debug.Log(pickedVirusDown);
        Debug.Log(pickedVirusLeft);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GuessTop();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GuessRight();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GuessDown();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GuessLeft();
        }
    }

    void GuessTop()
    {
        if (pickedVirusTarget == pickedVirusUp)
        {
            Debug.Log("Success");
        }
        else
        {
            Debug.Log("Fail");
        }
    }

    void GuessRight()
    {
        if (pickedVirusTarget == pickedVirusRight)
        {
            Debug.Log("Success");
        }
        else
        {
            Debug.Log("Fail");
        }
    }

    void GuessDown()
    {
        if (pickedVirusTarget == pickedVirusDown)
        {
            Debug.Log("Success");
        }
        else
        {
            Debug.Log("Fail");
        }
    }

    void GuessLeft()
    {
        if (pickedVirusTarget == pickedVirusLeft)
        {
            Debug.Log("Success");
        }
        else
        {
            Debug.Log("Fail");
        }
    }


}

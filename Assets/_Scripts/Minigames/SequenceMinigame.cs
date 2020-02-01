using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceMinigame : BaseMinigame
{
    public MinigameDisplayComponent DisplayComponent => throw new System.NotImplementedException();

    ////////////////////////////////////////////////////////////////
    
    private List<InputHelper.Keys>  m_OriginalSequence;
    private int                     m_InSequenceID;
    private List<MeshRenderer>      m_Renderers;

    private GameObject m_SequenceMinigameObject;

    ////////////////////////////////////////////////////////////////

    public override void Start()
    {
        Debug.Log("Start Minigame Sequence");

        GenerateSequence();
        GenerateSequenceSprites();
    }

    ////////////////////////////////////////////////////////////////

    public override MinigameTickResult Tick()
    {
        MinigameTickResult result = AcceptInput();
        TickRunes();
        //DisplaySequence();

        return result;
    }

    ////////////////////////////////////////////////////////////////

    public override void Finish()
    {
        GameObject.Destroy(m_SequenceMinigameObject);

        ////////////////////////////////////////////////////////////////

        Debug.Log("Finished Minigame Sequence");
    }

    ////////////////////////////////////////////////////////////////

    void GenerateSequence()
    {
        const int MIN_LENGTH = 5;
        const int MAX_LENGTH = 7;

        int length = Mathf.RoundToInt(Random.value * (MAX_LENGTH - MIN_LENGTH) + MIN_LENGTH);
        
        m_OriginalSequence  = new List<InputHelper.Keys>();
        m_InSequenceID      = 0;

        for (int i = 0; i < length; i++)
        {
            InputHelper.Keys key = InputHelper.Keys.Up;

            int keyDirection = Mathf.RoundToInt(Random.value * 3);
            switch (keyDirection)
            {
                case 0: key = InputHelper.Keys.Up;      break;
                case 1: key = InputHelper.Keys.Right;   break;
                case 2: key = InputHelper.Keys.Down;    break;
                case 3: key = InputHelper.Keys.Left;    break;
            }

            m_OriginalSequence.Add(key);
        }
    }

    ////////////////////////////////////////////////////////////////
    
    MinigameTickResult AcceptInput()
    {
        if (m_InSequenceID >= m_OriginalSequence.Count)
        {
            return MinigameTickResult.Failed;
        }

        KeyCode keyCode = GetKeyCode(m_OriginalSequence[m_InSequenceID]);

        bool anyKeyDown = AnyArrowKeyDown();
        if (!anyKeyDown)
        {
            return MinigameTickResult.ContinueTick;
        }

        if (Input.GetKeyDown(keyCode))
        {
            m_InSequenceID ++;
            
            if (m_InSequenceID == m_OriginalSequence.Count)
            {
                Debug.Log("You Win");
                return MinigameTickResult.EarlySuccess;
            }

            return MinigameTickResult.ContinueTick;
        }

        Debug.Log("You lose");
        return MinigameTickResult.Failed;
    }

    ////////////////////////////////////////////////////////////////
    
    void GenerateSequenceSprites()
    {
        m_SequenceMinigameObject = new GameObject("SequenceGame");
        m_SequenceMinigameObject.transform.parent = null;
        m_SequenceMinigameObject.transform.position = new Vector3(100, 0, 0);
        
        const float RING_HEIGHT_FACTOR   = 0.2f;
        const float PADDING_X_FACTOR     = 0.1f;
        const float PADDING_Y_FACTOR     = 0.3f;

        m_Renderers = new List<MeshRenderer>();

        // TODO
        for (int i = 0; i < m_OriginalSequence.Count; i++)
        {
            Texture2D texture = GameManager.Instance.settings.RuneTextures[(int) m_OriginalSequence[i]];

            GameObject spriteObject         = GameObject.Instantiate(GameManager.Instance.settings.RuneSpritePrefab);
            spriteObject.transform.parent   = m_SequenceMinigameObject.transform;
            spriteObject.transform.position = new Vector3(m_GamePlayRect.xMin + m_GamePlayRect.width * PADDING_X_FACTOR + (m_GamePlayRect.width - m_GamePlayRect.width * 2 * PADDING_X_FACTOR) / (float) m_OriginalSequence.Count * i, 0, 
                                                          m_GamePlayRect.yMin + m_GamePlayRect.height * PADDING_Y_FACTOR + (-Mathf.Cos((i / (float) m_OriginalSequence.Count) * Mathf.PI * 2.0f) * 0.5f + 0.5f) * m_GamePlayRect.height * RING_HEIGHT_FACTOR);

            MeshRenderer renderer           = spriteObject.GetComponent<MeshRenderer>();
            
            renderer.material.SetTexture("_MainTex", texture);

            m_Renderers.Add(renderer);
        }
    }

    ////////////////////////////////////////////////////////////////
    
    void TickRunes()
    {
        GameSettings settings = GameManager.Instance.settings;
        for (int i = 0; i < m_Renderers.Count; i++)
        {
            float glowAmount    = Mathf.Cos(Time.time + m_Renderers[i].transform.localPosition.x) * 0.5f + 0.5f;
            bool isDone         = i < m_InSequenceID;

            m_Renderers[i].material.SetColor("_Color", isDone ? settings.GoodColorBase : settings.BadColorBase);
            m_Renderers[i].material.SetColor("_GlowColor", isDone ? settings.GoodColorGlow : settings.BadColorGlow);
            m_Renderers[i].material.SetFloat("_GlowAmount", glowAmount);
        }
    }
}

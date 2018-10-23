using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{

    private GameObject gameObject;

    private static PlayerManager m_instance;
    public static PlayerManager Instance{
        get{
            if(m_instance == null){
                m_instance = new PlayerManager();
                m_instance.gameObject = new GameObject("_playerManager");
                m_instance.gameObject.AddComponent<Timer>();
            }
            return m_instance;
        }
    }

    private Timer m_Timer;
    public Timer Timer{
        get{
            if(m_Timer == null){
                m_Timer = gameObject.GetComponent<Timer>();
            }
            return m_Timer;
        }
    }

}

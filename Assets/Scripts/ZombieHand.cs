using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHand : MonoBehaviour {

    [SerializeField]
    private GameObject m_playerGO;

    private Player m_player;

    public Player PlayerObj
    {
        get { return m_player; }
        set { m_player = value; }
    }

    public GameObject PlayerGO
    {
        get { return m_playerGO; }
        set { m_playerGO = value; }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == m_playerGO)
        {
            m_player.Health -= 5;
        }
    }
}

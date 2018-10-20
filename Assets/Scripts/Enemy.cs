using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	private int m_health = 100;

	[SerializeField]
	private GameObject m_playerGO;

	[SerializeField]
	private GameObject m_enemyHitPrefab;

	[SerializeField]
	private Animator m_thisAnimator;

	private Player m_player;

	[SerializeField]
	private ZombieHand m_leftHand;

	[SerializeField]
	private ZombieHand m_rightHand;

	[SerializeField]
	private Rigidbody m_thisRigidbody;

	[SerializeField]
	private CapsuleCollider m_thisCollider;

	Coroutine c_dyingCoroutine = null;

	private void Awake ()
	{
		if (m_playerGO == null)
			m_playerGO = GameObject.FindGameObjectWithTag ("Player");

		if (m_player == null)
			m_player = m_playerGO.GetComponent <Player> ();

		m_leftHand.PlayerGO = m_playerGO;
		m_leftHand.PlayerObj = m_player;

		m_rightHand.PlayerGO = m_playerGO;
		m_rightHand.PlayerObj = m_player;
	}

	private void FixedUpdate ()
	{
		gameObject.transform.LookAt (new Vector3 (m_playerGO.transform.position.x, 0, m_playerGO.transform.position.z));
		//gameObject.transform.Translate (0, 0, Time.deltaTime * 5);
	}

	public int Health {
		get { return m_health; }
		set {
			m_health = value;
			if (m_health <= 0) {
				if (c_dyingCoroutine == null)
					c_dyingCoroutine = StartCoroutine (CDie ());
			}
		}
	}

	IEnumerator CDie ()
	{
		m_thisRigidbody.constraints = RigidbodyConstraints.FreezeAll;
		m_thisCollider.enabled = false;
		m_thisAnimator.SetTrigger ("Die");
		yield return new WaitForSeconds (m_thisAnimator.GetCurrentAnimatorStateInfo (0).length);
		m_player.UpdateScore (10);
		Destroy (this.gameObject);

		c_dyingCoroutine = null;
	}

	private void OnTriggerEnter (Collider other)
	{
		if (other.name == "PlayerAura") {
			m_thisAnimator.SetTrigger ("Attack");
		}
	}

	private void OnTriggerExit (Collider other)
	{
		if (other.name == "PlayerAura") {
			m_thisAnimator.SetTrigger ("Walk");
		}
	}
}

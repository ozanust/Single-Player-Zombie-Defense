using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{

	[SerializeField]
	private GameManager m_gameManager;

	[SerializeField]
	private GameObject m_pauseMenu;

	[SerializeField]
	private Player m_player;

	[SerializeField]
	private GameObject m_enemyGO;

	[SerializeField]
	private ParticleSystem m_spawnEffect;

	private float m_enemySpawnFrequency = 5;

	void Awake ()
	{
		Init ();
	}

	void Start ()
	{
		StartCoroutine (CreateEnemy ());
	}

	void Init ()
	{
		if (m_gameManager == null)
			m_gameManager = GameManager.Instance;

		m_gameManager.SetVolumeFromCache ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			m_pauseMenu.SetActive (true);
			Time.timeScale = 0;
		} else if (Input.GetKeyDown (KeyCode.F) || Input.GetMouseButtonDown (0)) {
			m_player.Shoot ();
		}
	}

	IEnumerator CreateEnemy ()
	{
		while (true) {
			yield return new WaitForSeconds (m_enemySpawnFrequency);
			Vector3 spawnPoint = new Vector3 (Random.Range (-10, 10), 0, Random.Range (-10, 10));
			Instantiate (m_spawnEffect, spawnPoint, Quaternion.identity);
			yield return new WaitForSeconds (m_spawnEffect.main.duration / 2f);
			Instantiate (m_enemyGO, spawnPoint, Quaternion.identity);

			if (m_enemySpawnFrequency > 0.5f)
				m_enemySpawnFrequency -= 0.1f;
		}
	}
}

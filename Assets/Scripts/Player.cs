using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

	public enum GunType
	{
		Pistol = 0,
		Sniper = 1
	}

	public enum BulletColor
	{
		Red = 0,
		Green = 1,
		Blue = 2
	}

	[SerializeField]
	private GameManager m_gameManager;

	[SerializeField]
	private int m_health = 100;

	[SerializeField]
	private int m_score = 0;

	[SerializeField]
	private GunType m_gunType;

	[SerializeField]
	private BulletColor m_bulletColor;

	[SerializeField]
	private GameObject m_bulletPrefab;

	[SerializeField]
	private GameObject m_pistolBulletPrefab;

	[SerializeField]
	private GameObject m_sniperBulletPrefab;

	[SerializeField]
	private GameObject m_gunPoint;

	[SerializeField]
	private Text m_healthText;

	[SerializeField]
	private Text m_scoreText;

	public int Health {
		get{ return m_health; }
		set {
			m_health = value;
			m_healthText.text = string.Format ("{0} {1}", "Health:", m_health.ToString ());
			if (m_health <= 0) {
				m_gameManager.GameOver ();
			}
		}
	}

	public int Score {
		get{ return m_score; }
		set {
			m_score = value;
			m_scoreText.text = string.Format ("{0} {1}", "Score:", m_score.ToString ());
			m_gameManager.Score = m_score;
		}
	}

	void Awake ()
	{
		Init ();
	}

	void Init ()
	{
		if (m_gameManager == null)
			m_gameManager = GameManager.Instance;

		SetPlayerType ((BulletColor)m_gameManager.PlayerType);
		SetGunType ((GunType)m_gameManager.GunType);
	}

	public void Shoot ()
	{
		GameObject bulletGO = Instantiate (m_bulletPrefab, m_gunPoint.transform.position, Quaternion.identity);
		Bullet bullet = bulletGO.GetComponent <Bullet> ();
		bullet.BulletColor = (Bullet.ColorType)m_bulletColor;
		bullet.Fire (m_gunPoint.transform.forward);
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.name == "KillZone") {
			m_gameManager.GameOver ();
		}
	}

	void SetPlayerType (BulletColor bulletColor)
	{
		m_bulletColor = bulletColor;
	}

	void SetGunType (GunType gunType)
	{
		m_gunType = gunType;

		if (m_gunType == GunType.Pistol)
			m_bulletPrefab = m_pistolBulletPrefab;
		else
			m_bulletPrefab = m_sniperBulletPrefab;
	}

	public void UpdateScore (int scoreIncrement)
	{
		Score += scoreIncrement;
	}
}

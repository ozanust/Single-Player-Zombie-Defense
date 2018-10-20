using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public enum ColorType
	{
		Red = 0,
		Green = 1,
		Blue = 2
	}

	public enum BulletType
	{
		PistolBullet = 0,
		SniperBullet = 1
	}

	private int m_bulletPower = 0;
	private int m_bulletSpeed = 0;
	private ColorType m_colorType;
	private Vector3 m_bulletSize;
	private GameObject m_thisGameObject;

	[SerializeField]
	private BulletType m_bulletType;

	[SerializeField]
	private MeshRenderer m_bulletRenderer;

	[SerializeField]
	private GameObject m_redHitEffect;

	[SerializeField]
	private GameObject m_greenHitEffect;

	[SerializeField]
	private GameObject m_blueHitEffect;

	[SerializeField]
	private GameObject m_hitEffect;

	private float m_timer = 0.0f;
	private Player m_player;

	public int BulletPower {
		get{ return m_bulletPower; }
		set{ m_bulletPower = value; }
	}

	public int BulletSpeed {
		get { return m_bulletSpeed; }
		set { m_bulletSpeed = value; }
	}

	public Vector3 BulletSize {
		get { return m_bulletSize; }
		set { m_bulletSize = value; }
	}

	public ColorType BulletColor {
		get{ return m_colorType; }
		set{ m_colorType = value; }
	}

	public BulletType BulletGunType {
		get { return m_bulletType; }
		set { m_bulletType = value; }
	}

	void Awake ()
	{
		m_thisGameObject = this.gameObject;
		SetBulletType ();

		if (m_player == null)
			m_player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}

	private void Start ()
	{
		SetBulletColor ();
	}

	void Update ()
	{
		m_timer += Time.deltaTime;
		if (m_timer >= 3)
			Destroy (this.gameObject);
	}

	private void SetBulletColor ()
	{
		if (m_colorType == ColorType.Red) {
			m_bulletRenderer.material.color = Color.red;
			m_hitEffect = m_redHitEffect;
		} else if (m_colorType == ColorType.Green) {
			m_bulletRenderer.material.color = Color.green;
			m_hitEffect = m_greenHitEffect;
		} else {
			m_bulletRenderer.material.color = Color.blue;
			m_hitEffect = m_blueHitEffect;
		}
	}

	private void SetBulletType ()
	{
		if (m_bulletType == BulletType.PistolBullet) {
			m_bulletPower = 20;
			m_bulletSpeed = 10;
			m_bulletSize = new Vector3 (0.2f, 0.2f, 0.2f);
			m_thisGameObject.transform.localScale = m_bulletSize;
		} else {
			m_bulletPower = 120;
			m_bulletSpeed = 30;
			m_bulletSize = new Vector3 (0.05f, 0.05f, 0.05f);
			m_thisGameObject.transform.localScale = m_bulletSize;
		}
	}

	public void Fire (Vector3 direction)
	{
		m_thisGameObject.GetComponent <Rigidbody> ().AddForce (direction * m_bulletSpeed, ForceMode.Impulse);
	}

	void OnTriggerEnter (Collider col)
	{
		
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent <Player> ().Health -= m_bulletPower;
		} else if (col.gameObject.tag == "Enemy") {
			col.gameObject.GetComponent <Enemy> ().Health -= m_bulletPower;
		}

		Instantiate (m_hitEffect, this.gameObject.transform.position, Quaternion.identity);

		Destroy (this.gameObject);
	}
}

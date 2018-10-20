using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoManager<GameManager>
{

	[SerializeField]
	private bool m_dontDestroyOnLoad = true;

	[SerializeField]
	private float m_gameVolume = 1;

	[SerializeField]
	private GameObject m_gameOverScreen;

	[SerializeField]
	private Text m_scoreText;

	private int m_playerType = 0;
	private int m_gunType = 0;
	private int m_score = 0;

	public float GameVolume {
		get{ return m_gameVolume; }
	}

	public int PlayerType {
		get { return m_playerType; }
		set { m_playerType = value; }
	}

	public int GunType {
		get { return m_gunType; }
		set { m_gunType = value; }
	}

	public int Score {
		get { return m_score; }
		set { m_score = value; }
	}

	private Coroutine cGameOverCoroutine = null;

	void Awake ()
	{
		if (m_dontDestroyOnLoad)
			DontDestroyOnLoad (this);

		if (SceneManager.GetActiveScene ().buildIndex == 0)
			LoadScene (1);
	}

	#region SceneManagement

	public void LoadScene (int index)
	{
		SceneManager.LoadScene (index);
	}

	#endregion


	#region SettingsManagement

	public void SetVolumeFromCache ()
	{
		AudioListener.volume = m_gameVolume;
	}

	public void SetVolume (float volumeValue)
	{
		m_gameVolume = volumeValue;
		AudioListener.volume = volumeValue;
	}

	public void SetGraphicSetting (int settingIndex)
	{
		QualitySettings.SetQualityLevel (settingIndex);
	}

	#endregion

	public void GameOver ()
	{
		if (cGameOverCoroutine == null)
			cGameOverCoroutine = StartCoroutine (CGameOver ());
	}

	IEnumerator CGameOver ()
	{
		m_scoreText.text = string.Format ("{0} {1}", "Your score is: ", m_score);
		m_gameOverScreen.SetActive (true);
		yield return new WaitForSeconds (5);
		m_gameOverScreen.SetActive (false);
		m_score = 0;
		cGameOverCoroutine = null;
		LoadScene (1);
	}

}

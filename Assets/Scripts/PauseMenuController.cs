using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

	[SerializeField]
	private GameManager m_gameManager;

	[SerializeField]
	private GameObject m_pauseMenu;

	void Awake ()
	{
		if (m_gameManager == null)
			m_gameManager = GameManager.Instance;
	}

	public void LoadSceneViaButton (int index)
	{
		m_gameManager.LoadScene (index);
	}

	public void ReturnToGame ()
	{
		Time.timeScale = 1;
		m_pauseMenu.SetActive (false);
	}

	public void RestartGame ()
	{
		m_gameManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}

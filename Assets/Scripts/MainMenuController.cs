using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{

	[SerializeField]
	private GameManager m_gameManager;

	[SerializeField]
	private Slider m_volumeSlider;

	[SerializeField]
	private Toggle m_lowGraphicToggle;

	[SerializeField]
	private Toggle m_mediumGraphicToggle;

	[SerializeField]
	private Toggle m_highGraphicToggle;

    [SerializeField]
    private Dropdown m_playerTypeDrowdown;

    [SerializeField]
    private Dropdown m_gunTypeDropdown;

	[SerializeField]
	private GameObject m_pauseMenu;

	void Awake ()
	{
		if (m_gameManager == null)
			m_gameManager = GameManager.Instance;

		Init ();
	}

	void Start ()
	{
		if (m_gameManager == null)
			m_gameManager = GameManager.Instance;
	}

	private void Init ()
	{
		RegisterEvents ();
	}

	private void RegisterEvents ()
	{
		if (m_volumeSlider != null)
			m_volumeSlider.onValueChanged.AddListener (OnVolumeChanged);

		if (m_lowGraphicToggle != null)
			m_lowGraphicToggle.onValueChanged.AddListener (OnLowGraphicSettingToggleValueChanged);

		if (m_mediumGraphicToggle != null)
			m_mediumGraphicToggle.onValueChanged.AddListener (OnMediumGraphicSettingToggleValueChanged);

		if (m_highGraphicToggle != null)
			m_highGraphicToggle.onValueChanged.AddListener (OnHighGraphicSettingToggleValueChanged);

        if (m_playerTypeDrowdown != null)
            m_playerTypeDrowdown.onValueChanged.AddListener(OnPlayerTypeValueChanged);

        if (m_gunTypeDropdown != null)
            m_gunTypeDropdown.onValueChanged.AddListener(OnGunTypeValueChanged);
    }

	#region EventListeners

	void OnVolumeChanged (float value)
	{
		m_gameManager.SetVolume (value);
	}

	void OnLowGraphicSettingToggleValueChanged (bool value)
	{
		if (value) {
			m_mediumGraphicToggle.isOn = false;
			m_highGraphicToggle.isOn = false;
			m_lowGraphicToggle.interactable = false;

			OnGraphicSettingsChanged (0);
		} else {
			m_lowGraphicToggle.interactable = true;
		}
	}

	void OnMediumGraphicSettingToggleValueChanged (bool value)
	{
		if (value) {
			m_lowGraphicToggle.isOn = false;
			m_highGraphicToggle.isOn = false;
			m_mediumGraphicToggle.interactable = false;

			OnGraphicSettingsChanged (2);
		} else {
			m_mediumGraphicToggle.interactable = true;
		}
	}

	void OnHighGraphicSettingToggleValueChanged (bool value)
	{
		if (value) {
			m_mediumGraphicToggle.isOn = false;
			m_lowGraphicToggle.isOn = false;
			m_highGraphicToggle.interactable = false;

			OnGraphicSettingsChanged (5);
		} else {
			m_highGraphicToggle.interactable = true;
		}
	}

	void OnGraphicSettingsChanged (int settingIndex)
	{
		m_gameManager.SetGraphicSetting (settingIndex);
	}

    void OnPlayerTypeValueChanged(int playerType)
    {
        m_gameManager.PlayerType = playerType;
    }

    void OnGunTypeValueChanged(int gunType)
    {
        m_gameManager.GunType = gunType;
    }

    #endregion


    #region PublicMethods

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
		Time.timeScale = 1;
		m_gameManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void ReturnToManu ()
	{
        Time.timeScale = 1;
        m_gameManager.PlayerType = 0;
        m_gameManager.GunType = 0;
        m_gameManager.LoadScene (1);
	}

	#endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{

	[SerializeField]
	private float m_mapSize;

	[SerializeField]
	private float m_mapAltitude;

	[SerializeField]
	private Camera m_miniMapCam;

	void Awake ()
	{
		if (m_miniMapCam != null) {
			if (m_miniMapCam.orthographic)
				m_miniMapCam.orthographicSize = m_mapSize;
			else
				m_miniMapCam.transform.position = new Vector3 (m_miniMapCam.transform.position.x, m_mapAltitude, m_miniMapCam.transform.position.z);
		}
	}
}

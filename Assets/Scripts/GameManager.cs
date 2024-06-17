using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _paddleOneScore = 0;
    [SerializeField] private int _paddleTwoScore = 0;
    
    [SerializeField] private int _victoryScore = 10;
    
    [SerializeField] private Camera _mainCamera;
    
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioKASSINO;
    [SerializeField] private AudioClip _audioKassinoVictory1;
    [SerializeField] private AudioClip _audioKassinoVictory2;
    
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    
    private Vector3 _startShakePosition = new Vector3(0f,0f,0f);
    
    private float _shakeTimer;

    void Start()
    {
	    _instance = this;
	    PlayAudioClip(_audioKASSINO);
    }
    
    private void OnShakeCamera(float intensity, float time)
    {
	    _startShakePosition = _mainCamera.transform.position;
	    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
		    _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

	    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
	    _shakeTimer = time;
    }

    void Update()
    {
	    if (_shakeTimer > 0)
	    {
		    _shakeTimer -= Time.deltaTime;
		    if (_shakeTimer <= 0f)
		    {
			    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
				    _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

			    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
			    _mainCamera.transform.rotation = Quaternion.Slerp(_mainCamera.transform.rotation, Quaternion.Euler(0f, 0f, 0f), .8f);
			    _mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position, _startShakePosition, .8f);
		    }
	    }
    }

	public void AddPoints(bool isPlayerOne, int points)
	{
		if(isPlayerOne)
		{
			_paddleOneScore += points;
		} else
		{
			_paddleTwoScore += points ;
		}

		UIManager.Instance.UpdateScorePoints();
		OnShakeCamera(5f,.2f);

		if (_paddleOneScore >= _victoryScore)
		{
			PlayAudioClip(_audioKassinoVictory1);
			UIManager.Instance.Victory(true);
		}
		else if (_paddleTwoScore >= _victoryScore)
		{
			PlayAudioClip(_audioKassinoVictory1);
			UIManager.Instance.Victory(false);
		}
	}

	public int[] GetPoints()
	{
		int[] pointsArray = new int[] {_paddleOneScore, _paddleTwoScore};
		return pointsArray;
	}

	public void ClearPoints()
	{
		_paddleOneScore = 0;
		_paddleTwoScore = 0;
		UIManager.Instance.UpdateScorePoints();
	}

	public void PlayAudioClip(AudioClip _pointAudioClip)
	{
		_audioSource.clip = _pointAudioClip;
		_audioSource.Play();
	}

}

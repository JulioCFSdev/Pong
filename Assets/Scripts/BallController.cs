using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private List<Vector2> _startDirections;
    [SerializeField] private int _pointValue = 1;
    [SerializeField] private float _maxSpeed = 20f;
    [SerializeField] private float _startSpeed = 4f;
	[SerializeField] private AudioClip _ballScore;
	[SerializeField] private AudioClip _ballCollision;
	
    private Rigidbody2D _rigidBall;
    public float m_Thrust = 1f;
    private Vector2 _currentDirectionMoviment;
    private Vector2 _maxDirectionMoving = new Vector2(0f, 1f);
    private Vector2 _minDirectionMoving = new Vector2(0f, -1f);
	
    
    void Start()
    {
        _rigidBall = GetComponent<Rigidbody2D>();
        Invoke("StartBallBehavior", 1.0f);
    }
    
    private void StartBallBehavior()
    {
        transform.position = Vector3.zero;
        int _indexDirectionMoviment = Random.Range(0,_startDirections.Count - 1);
        _rigidBall.velocity = _startDirections[_indexDirectionMoviment] * _startSpeed;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
			GameManager.Instance.PlayAudioClip(_ballCollision);
			if (_rigidBall.velocity.magnitude <= _maxSpeed) return;

            _currentDirectionMoviment = new Vector2(-1f * _currentDirectionMoviment.x, -1f * _currentDirectionMoviment.y);
            _rigidBall.velocity = Vector3.ClampMagnitude(_rigidBall.velocity, _maxSpeed);
        }
        if (collision.gameObject.CompareTag("Walls"))
        {
			GameManager.Instance.PlayAudioClip(_ballCollision);
			if (_rigidBall.velocity.magnitude <= _maxSpeed) return;
            _currentDirectionMoviment = new Vector2(_currentDirectionMoviment.x, -1f * _currentDirectionMoviment.y);
            _rigidBall.velocity = Vector3.ClampMagnitude(_rigidBall.velocity, _maxSpeed);
			GameManager.Instance.PlayAudioClip(_ballCollision);
        }

        if (collision.gameObject.CompareTag("Limits"))
        {
            GameManager.Instance.AddPoints(true, _pointValue);
			//GameManager.Instance.PlayAudioClip(_ballScore);
            StartBallBehavior();
        }
        if (collision.gameObject.CompareTag("Limits2"))
        {
            GameManager.Instance.AddPoints(false, _pointValue);
			//GameManager.Instance.PlayAudioClip(_ballScore);
            StartBallBehavior();
        }
    } 
}

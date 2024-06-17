using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerOneLabel;
    [SerializeField] private TextMeshProUGUI _playerTwoLabel;
    
    [SerializeField] private GameObject _victoryPanel;
    
    [SerializeField] private TextMeshProUGUI _playerVictoryLabel;

    [SerializeField] private float _winnerFontTextSize = 200f;
    [SerializeField] private float _tieFontTextSize = 156f;
    [SerializeField] private float _losserFontTextSize = 100f;
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    void Awake()
    {
        Time.timeScale = 0f;
    }
    
    void Start()
    {
        _instance = this;
    }

    public void UpdateScorePoints()
    {
        int[] points = GameManager.Instance.GetPoints();
        if (points[0] > points[1])
        {
            _playerOneLabel.fontSize = _winnerFontTextSize;
            _playerOneLabel.text = points[0].ToString();

            _playerTwoLabel.fontSize = _losserFontTextSize;
            _playerTwoLabel.text = points[1].ToString();
            
        } else if(points[0] < points[1])

        {
            _playerTwoLabel.fontSize = _winnerFontTextSize;
            _playerTwoLabel.text = points[1].ToString();

            _playerOneLabel.fontSize = _losserFontTextSize;
            _playerOneLabel.text = points[0].ToString();
        }
        else
        {
            _playerTwoLabel.fontSize = _tieFontTextSize;
            _playerTwoLabel.text = points[1].ToString();

            _playerOneLabel.fontSize = _tieFontTextSize;
            _playerOneLabel.text = points[0].ToString();
        }
    }

    public void Play()
    {
        Time.timeScale = 1f;
    }

    public void Victory(bool playerOneIsWinner)
    {
        Time.timeScale = 0f;
        _victoryPanel.SetActive(true);
        if (playerOneIsWinner)
        {
            _playerVictoryLabel.text = "Player 1 Wins";
        }
        else
        {
            _playerVictoryLabel.text = "Player 2 Wins";
        }
    }
}

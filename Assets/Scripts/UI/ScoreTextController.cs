using System;
using TMPro;
using UnityEngine;

public class ScoreTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private bool updateOnce;
    
    private GameManager _gameManager;

    private bool _isUpdated;
    
    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        SetText();
    }

    private void SetText()
    {
        if (_isUpdated) return;
        textMesh.text = "Score: " + _gameManager.Score;
        if (updateOnce) _isUpdated = true;
    }
}
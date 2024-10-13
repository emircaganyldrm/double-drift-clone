using System;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Score { get; private set; }
    
    public event Action GameOver;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        var distance = Vector3.Distance(GameFollowManager.Instance.FollowTarget.position, Vector3.zero);
        if (distance > Score)
        {
            Score = Mathf.RoundToInt(distance);
        }
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void EndGame()
    {
        GameOver?.Invoke();
    }
}

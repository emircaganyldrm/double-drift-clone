using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        private void OnEnable()
        {
            GameManager.Instance.GameOver += ShowPanel;
        }
        
        private void OnDisable()
        {
            GameManager.Instance.GameOver -= ShowPanel;
        }
        
        private void ShowPanel()
        {
            panel.SetActive(true);
        }
    }
}
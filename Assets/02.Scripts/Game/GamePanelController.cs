using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanelController : MonoBehaviour
{
    [SerializeField] private GameObject _quizCartPrefab;
    [SerializeField] private Transform _quizCartParent;
    
    public void OnClickGameOverButton()
    {
        GameManager.Instance.QuitGame();
    }
}

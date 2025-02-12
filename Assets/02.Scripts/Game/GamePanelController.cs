using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanelController : MonoBehaviour
{
    [SerializeField] private GameObject _quizCartPrefab;
    [SerializeField] private Transform _quizCardParent;

    private
    
    void Start()
    {
        ShowQuizCard();
    }
    
    private void ShowQuizCard()
    {
        var quizCardObject = Instantiate(_quizCartPrefab, _quizCardParent);
    }
    
    public void OnClickGameOverButton()
    {
        GameManager.Instance.QuitGame();
    }
}

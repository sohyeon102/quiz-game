using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GamePanelController : MonoBehaviour
{
    private GameObject _firstQuizCardObject;
    private GameObject _secondQuizCardObject;
    
    private List<QuizData> _quizDataList;
    private Queue<QuizData> _quizQueue; // Queue로 퀴즈 데이터 관리
    private int _quizIndex = 0; // 현재 퀴즈 진행 상태 추적
    private int _currentQuizIndex = 0;  // 현재 표시 중인 퀴즈 번호
    private int _quizCount = 10;        // 총 퀴즈 개수

    private void Start()
    {
        //테스트
        _quizDataList = QuizDataController.LoadQuizData(0);
        _quizQueue = new Queue<QuizData>(_quizDataList); //
        
        InitQuizCards();
    }

    private void InitQuizCards()
    {
        _firstQuizCardObject = ObjectPool.Instance.GetObject();
        _secondQuizCardObject = ObjectPool.Instance.GetObject();

        // 퀴즈 데이터를 설정
        _firstQuizCardObject.GetComponent<QuizCardController>().SetQuiz(_quizDataList[_currentQuizIndex], OnCompletedQuiz);
    
        _currentQuizIndex = (_currentQuizIndex + 1) % _quizCount; // 다음 문제로 이동
        _secondQuizCardObject.GetComponent<QuizCardController>().SetQuiz(_quizDataList[_currentQuizIndex], OnCompletedQuiz);
    
        SetQuizCardPosition(_firstQuizCardObject, 0);
        SetQuizCardPosition(_secondQuizCardObject, 1);
    }

    
    private void OnCompletedQuiz(int cardIndex)
    {
        
    }

    
    private void SetQuizCardPosition(GameObject quizCardObject, int index)
    {
        var quizCardTransform = quizCardObject.GetComponent<RectTransform>();
    
        if (index == 0)
        {
            quizCardTransform.anchoredPosition = new Vector2(0, 0);
            quizCardTransform.localScale = Vector3.one;
            quizCardTransform.SetAsLastSibling();
        }
        else if (index == 1)
        {
            quizCardTransform.anchoredPosition = new Vector2(0, 160);
            quizCardTransform.localScale = Vector3.one * 0.9f;
            quizCardTransform.SetAsFirstSibling();
        }
    }

    private void ChangeQuizCard()
    {
        /*var temp = _firstQuizCardObject;
        _firstQuizCardObject = _secondQuizCardObject;
        _secondQuizCardObject = ObjectPool.Instance.GetObject();
        SetQuizCardPosition(_firstQuizCardObject, index: 0);
        SetQuizCardPosition(_secondQuizCardObject, index: 1);
        
        ObjectPool.Instance.ReturnObject(temp);*/
        
        var temp = _firstQuizCardObject;  // 현재 퀴즈를 저장
        _firstQuizCardObject = _secondQuizCardObject;  // 두 번째 카드를 첫 번째로 변경
        _secondQuizCardObject = ObjectPool.Instance.GetObject();  // 새로운 카드를 가져옴
    
        // 현재 퀴즈 인덱스를 1 증가시키고, 10을 넘어가면 0으로 변경
        _currentQuizIndex = (_currentQuizIndex + 1) % _quizCount;

        _secondQuizCardObject.GetComponent<QuizCardController>().SetQuiz(_quizDataList[_currentQuizIndex], OnCompletedQuiz);

        SetQuizCardPosition(_firstQuizCardObject, 0);
        SetQuizCardPosition(_secondQuizCardObject, 1);

        // 이전 카드를 풀로 반환
        ObjectPool.Instance.ReturnObject(temp);
    }

    public void OnClickNextButton()
    {
        ChangeQuizCard();
    }

}

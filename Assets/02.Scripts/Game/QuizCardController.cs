using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct QuizData
{
    public string question;
    public string description;
    public int type;
    public int answer;
    public string firstOption;
    public string secondOption;
    public string thirdOption;
}

public class QuizCardController : MonoBehaviour
{
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Button[] optionButtons;

    public delegate void QuizCardDelegate(int cardIndex);
    private event QuizCardDelegate onCompleted;
    
    public void SetQuiz(QuizData quizData, QuizCardDelegate onCompleted)
    {
        // 1. 퀴즈
        // 2. 설명
        // 3. 타입 (0: OX퀴즈, 1: 보기 3개 객관식)
        // 4. 정답
        // 5. 보기 (1,2,3)
        
        // 퀴즈 데이터 표현
        questionText.text = quizData.question;
        // descriptionText.text = quizData.description;
        
        var firstButtonText = optionButtons[0].GetComponentInChildren<TMP_Text>();
        firstButtonText.text = quizData.firstOption;
        var secondButtonText = optionButtons[1].GetComponentInChildren<TMP_Text>();
        secondButtonText.text = quizData.secondOption;
        var thirdButtonText = optionButtons[2].GetComponentInChildren<TMP_Text>();
        thirdButtonText.text = quizData.thirdOption;
        
        this.onCompleted = onCompleted;
    }
}

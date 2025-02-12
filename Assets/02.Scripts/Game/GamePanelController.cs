using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GamePanelController : MonoBehaviour
{
    [SerializeField] private GameObject quizCardPrefab;            // Quiz Card Prefab
    [SerializeField] private Transform quizCardParent;             // Quiz Card가 표시될 UI Parent
    private List<GameObject> quizCards = new List<GameObject>();
    
    
    void Start()
    {
        InitQuizCard();
    }

    private void InitQuizCard()
    {
        for (int i = 0; i < 3; i++)
        {
            var card = ObjectPool.Instance.GetObject();
            card.transform.SetParent(quizCardParent, false);
            card.transform.localScale = Vector3.one; 
            card.AddComponent<QuizCardController>();  
            quizCards.Add(card);
        }

        ArrangeCards();
    }
    
    private void ArrangeCards()
    {
        for (int i = 0; i < quizCards.Count; i++)
        {
            quizCards[i].transform.SetSiblingIndex(i);

            // 두번째 카드 위치 위로 
            float offsetY = 0f;
            if (i == 1)
            {
                offsetY = 50f;
            }
            quizCards[i].transform.DOLocalMove(new Vector3(0, offsetY, 0), 0.5f);
        }
    }
    
    public void CardToBack()
    {
        if (quizCards.Count < 3) return;
        
        GameObject firstCard = quizCards[0];
        quizCards.RemoveAt(0);
        
        quizCards.Add(firstCard);
        
        ArrangeCards();
    }
}

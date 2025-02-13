using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class QuizDataController
{
    static string ROW_SEPARATOR = @"\r\n|\n\r|\n|\r"; //모든 종류의 줄바꿈을 기준으로 문자열을 나누려는 목적
    static string COL_SEPARATOR  = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; 
    private static char[] TRIM_CHARS = { '\"' };

    public static List<QuizData> LoadQuizData(int stageIndex)
    {
        var fileName = "QuizData-" + stageIndex;
        
        TextAsset quizDataAsset = Resources.Load(fileName) as TextAsset; //Resources가 오브젝트 타입이여서 TextAsset으로 형변환
        var lines = Regex.Split(quizDataAsset.text, ROW_SEPARATOR); // 줄바꿈문자(LINE_SEPARATOR)를 기준으로 문자열을 나눠서 lines 배열에 저장 -> 즉 quiz-data 내용을 줄 단위로 나눔
        
        var quizDataList = new List<QuizData>();
        
        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], COL_SEPARATOR);
            QuizData quizData = new QuizData();
            
            for (var j = 0; j < values.Length; j++)
            {
                var value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                
                switch (j)
                {
                    case 0:
                        quizData.question = value;
                        break;
                    case 1:
                        quizData.description = value;
                        break;
                    case 2:
                        quizData.type = int.Parse(value);
                        break;
                    case 3:
                        quizData.answer = int.Parse(value);
                        break;
                    case 4:
                        quizData.firstOption = value;
                        break;
                    case 5:
                        quizData.secondOption = value;
                        break;
                    case 6:
                        quizData.thirdOption = value;
                        break;
                }
            }
            quizDataList.Add(quizData);
        }
        return quizDataList;
    }
}

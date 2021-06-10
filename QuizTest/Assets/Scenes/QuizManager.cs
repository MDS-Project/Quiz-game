using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public QuestionTF[] questionsTF;
    public Question4[] question4;

    [SerializeField]
    private Text factText;
    [SerializeField]
    private Text answer1;
    [SerializeField]
    private Text answer2;
    [SerializeField]
    private Text answer3;
    [SerializeField]
    private Text answer4;
    [SerializeField]
    private float time = 1f;


    private static List<Question> unansweredQuestion;

    private static Question currentQuestion;
    void Start ()
    {
        if (unansweredQuestion == null || unansweredQuestion.Count == 0)
        {
            unansweredQuestion = questionsTF.ToList<Question>();
            unansweredQuestion.AddRange(question4.ToList<Question>());
            getRandomQuestion();
        }

        showQuestion();

        Debug.Log(currentQuestion.fact + " is " + currentQuestion.getTrue() + " " + currentQuestion.GetType());
        
    }
    void showQuestion()
    {
        if (currentQuestion.GetType() == typeof(Question4))
        {

            Question4 q = (Question4)currentQuestion;
            answer1.text = q.answers[0];
            answer2.text = q.answers[1];
            answer3.text = q.answers[2];
            answer4.text = q.answers[3];
        }
        else
        {
            factText.text = currentQuestion.fact;
        }
    }

    void getRandomQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestion.Count);
        currentQuestion = unansweredQuestion[randomQuestionIndex];
        if (currentQuestion.GetType() == typeof(Question4))
        {
            SceneManager.LoadScene("Scene_!");
        }
        else
        {
            SceneManager.LoadScene("Scene_2");
        }
            unansweredQuestion.RemoveAt(randomQuestionIndex);
    }

   

    public void UserSelectAnswer(int i)
    {
        if(currentQuestion.getIndex() == i)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Wrong!");
        }
        getRandomQuestion();
    }

    public void UserSelectTrue ()
    {
        if (currentQuestion.getTrue())
        {
            Debug.Log("CORRECT!");
        }else
        {
            Debug.Log("Wrong!");
        }
        getRandomQuestion();
    }

    public void UserSelectFalse()
    {
        if (!currentQuestion.getTrue())
        {
            Debug.Log("CORRECT!");
        }
        else
        {
            Debug.Log("Wrong!");
        }
        getRandomQuestion();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class QuizManager : MonoBehaviour
{
    public QuestionTF[] questionsTF = new QuestionTF[] {new QuestionTF("4+4=2",false),new QuestionTF("2+2=4",true)};
    public Question4[] question4 = new Question4[] {new Question4("Cat face 2+2?",new string[]{"2","3","4","5"},2),new Question4("Cat face 3+2?",new string[]{"2","3","4","5"},3)};

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
    private Text Score;
    [SerializeField]
    private GameObject Blocker;
    [SerializeField]
    private Text Timer;
    private static int ok = 0;
    private static float score = 0;
    private float timeRemaining = 5;
    private bool timerIsRunning = false;
    private int score1 = (int) score;
    private static List<Question> unansweredQuestion;
    private static Question currentQuestion;

    private Button button;

    public static int getScore(){
        return (int) score;
    }

    public static void setScore(){
        score = 0;
    }
    void Start ()
    {
        Blocker.SetActive(false);
        if ((unansweredQuestion == null || unansweredQuestion.Count == 0) && ok == 0)
        {
            unansweredQuestion = questionsTF.ToList<Question>();
            unansweredQuestion.AddRange(question4.ToList<Question>());
            getRandomQuestion();
            ok++;
        }

         Score.text = score1.ToString();

        showQuestion();
        timerIsRunning = true;

        
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                int timeR = (int) timeRemaining;
                Timer.text = timeR.ToString();
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                Timer.text = timeRemaining.ToString();
                getRandomQuestion();
            }
        }
    }

    void showQuestion()
    {
        if (currentQuestion.GetType() == typeof(Question4))
        {
            factText.text = currentQuestion.fact;
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
        Blocker.SetActive(false);
        if ((unansweredQuestion == null || unansweredQuestion.Count == 0) && ok == 1)
        {
            Debug.Log("terminat");
            Debug.Log("Score "+score);
            ok = 0;
            SceneManager.LoadScene("Scene_SingleFinal");
        }else{
        int randomQuestionIndex = Random.Range(0, unansweredQuestion.Count);
        currentQuestion = unansweredQuestion[randomQuestionIndex];
        if (currentQuestion.GetType() == typeof(Question4))
        {
            SceneManager.LoadScene("Scene_1");
        }
        else
        {
            SceneManager.LoadScene("Scene_2");
        }
        
            unansweredQuestion.RemoveAt(randomQuestionIndex);
        }
    
    }

    public void getButton(Button btn)
    {
        button =  btn;
    }
    public void UserSelectAnswer(int i)
    {    
        Blocker.SetActive(true);
        if(currentQuestion.getIndex() == i)
        {
            Debug.Log("Correct!");
            score = score+(timeRemaining*1000); 
            score1 = (int) score;
            Score.text = score1.ToString();
            button.image.color = Color.green;
        }
        else
        {
            Debug.Log("Wrong!");
            button.image.color = Color.red;
        }
        timerIsRunning = false;
        button = null;
        StartCoroutine(Wait2());
    }

    public void UserSelectTrue ()
    {
         Blocker.SetActive(true);
         Debug.Log(currentQuestion.getTrue());
        if (currentQuestion.getTrue())
        {
            Debug.Log("CORRECT!");
            score += +(timeRemaining*1000); 
            score1 = (int) score;
            Score.text = score1.ToString();
            button.image.color = Color.green;
        }else
        {
            button.image.color = Color.red;

            Debug.Log("Wrong!");
        }
        timerIsRunning = false;
        StartCoroutine(Wait2());
    }

    public void UserSelectFalse()
    {
          Blocker.SetActive(true);
          currentQuestion = (QuestionTF) currentQuestion;
        if (!currentQuestion.getTrue())
        {
            Debug.Log("CORRECT!");
            button.image.color = Color.green;
            score = score+(timeRemaining*1000); 
            score1 = (int) score;
            Score.text = score1.ToString();
        }
        else
        {   
            button.image.color = Color.red;
            Debug.Log("Wrong!");
        }
        timerIsRunning = false;
        StartCoroutine(Wait2());
        
    }
    IEnumerator Wait2(){
        
        yield return new WaitForSeconds (2);
         timerIsRunning = true;
        getRandomQuestion();
    }

 
}

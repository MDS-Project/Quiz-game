using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class QuizManager : MonoBehaviour
{
    public QuestionTF[] questionsTF = new QuestionTF[] {new QuestionTF("The Channel Tunnel is the longest rail tunnel in the world",false),new QuestionTF("A woman has walked on the Moon",false),new QuestionTF("According to Scottish law, it is illegal to be drunk in charge of a cow",true),new QuestionTF("Vietnamese is an official language in Canada",false), new QuestionTF("The setting for the ITV drama Midsomer Murders is a fictional English county called Midsomer",true), new QuestionTF("An emu can fly",false), new QuestionTF("The can-opener was not invented until 45 years after the tin can",true), new QuestionTF("There are McDonald’s one every continent except one",true), new QuestionTF("Stephen Hawking declined a knighthood from the Queen",true), new QuestionTF("The five rings on the Olympic flag are interlocking?",true),new QuestionTF(" Mount Kilimanjaro is the highest mountain in the world?",false),new QuestionTF("A group of swans is known as a bevy?",true),new QuestionTF("Sydney is the capital of Australia?",false),new QuestionTF("Fish cannot blink?",true),new QuestionTF("Seahorses have no teeth or stomach?",true),new QuestionTF("Nepal is the only country in the world which does not have a rectangular flag?",true),new QuestionTF("Switzerland shares land borders with four other countries?",false),new QuestionTF("The knight is the only piece in chess which can only move diagonally?",true)};
    public Question4[] question4 = new Question4[] {new Question4("What is the capital of Chile?",new string[]{"Santiago","Barcelona","Moscow","Mexico City"},0),new Question4("What is the largest country in the world?",new string[]{"USA","Africa","China","Russia"},3), new Question4("In football, which team has won the Champions League the most?",new string[]{"FC Barcelona","FC Steaua","Real Madrid","Manchester City"},2),new Question4("What is Japanese sake made from?",new string[]{"Rice","Wood","Bamboo","Grapes"},0),new Question4("What is the capital of Westeros in Game of Thrones?",new string[]{"Free Cities","Yunkai","Oldtown","King’s Landing"},3),new Question4("Which singer has the most UK Number One singles ever?",new string[]{"Justin Beber"," Elvis Presley","Adele","Scorpions"},1),new Question4("What is the smallest planet in our solar system?",new string[]{"Mercury","Pluto","Earth","Neptun"},0),new Question4("Which popular video game franchise has released games with the subtitles World At War and Black Ops?",new string[]{"Assassins Creed","The witcher","Counter Strike","COD"},3),new Question4("What is the currency of Denmark?",new string[]{"Dollar","RON","Krone","Euro"},2),new Question4("How many permanent teeth does a dog have?",new string[]{"43","50","31","42"},3),new Question4("What temperature centigrade does water boil at?",new string[]{"99","101","100","120"},2),new Question4("In which year was the Microsoft XP operating system released?",new string[]{"2001","2004","2007","2002"},0), new Question4("Who discovered penicillin?",new string[]{"Louis Pasteur"," Sigmund Freud"," Edward Jenner"," Alexander Fleming"},3)};

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
    [SerializeField]
    private GameObject Wrong;
    [SerializeField]
    private GameObject Right;

    private static int ok = 0;
    private static float score = 0;
    private float timeRemaining = 20;
    private bool timerIsRunning = false;
    private int score1 = (int) score;
    private static List<Question> unansweredQuestion = new List<Question>();
    private static Question currentQuestion;

    private Button button;

    public static int getScore(){
        return (int) score;
    }

    public static void setScore(){
        score = 0;
    }
    public List<Question> getRandomQuestions(){
        List<Question> quest1 = questionsTF.ToList<Question>();
        List<Question> quest= new List<Question>();
        List<Question> quest2 = question4.ToList<Question>();
        for(int i=0;i<6;i++)
        {
        int randomQuestionIndex = Random.Range(0, quest1.Count);
        Debug.Log(quest1[randomQuestionIndex].GetType());
        quest.Add(quest1[randomQuestionIndex]);
        quest1.RemoveAt(randomQuestionIndex);
        }
        for(int i=0;i<4;i++)
        {
        int randomQuestionIndex = Random.Range(0, quest2.Count);
        quest.Add(quest2[randomQuestionIndex]);
        quest2.RemoveAt(randomQuestionIndex);
        }
        
        return quest;
    }
    void Start ()
    {
        if ((unansweredQuestion == null || unansweredQuestion.Count == 0) && ok == 0)
        {
            unansweredQuestion.AddRange(getRandomQuestions());

            getRandomQuestion();
            ok++;
        }

         Score.text = score1.ToString();

        showQuestion();
        Blocker.SetActive(false);
        Wrong.SetActive(false);
        Right.SetActive(false);
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
            Right.SetActive(true);
            score = score+(timeRemaining*1000); 
            score1 = (int) score;
            Score.text = score1.ToString();
            button.image.color = Color.green;
        }
        else
        {
            Debug.Log("Wrong!");
            Wrong.SetActive(true);
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
            Right.SetActive(true);
            score += +(timeRemaining*1000); 
            score1 = (int) score;
            Score.text = score1.ToString();
            button.image.color = Color.green;
        }else
        {
            button.image.color = Color.red;

            Debug.Log("Wrong!");
            Wrong.SetActive(true);
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
            Right.SetActive(true);
            button.image.color = Color.green;
            score = score+(timeRemaining*1000); 
            score1 = (int) score;
            Score.text = score1.ToString();
        }
        else
        {   
            button.image.color = Color.red;
            Debug.Log("Wrong!");
            Wrong.SetActive(true);
        }
        timerIsRunning = false;
        StartCoroutine(Wait2());
        
    }
    IEnumerator Wait2(){
        
        yield return new WaitForSeconds (1);
         timerIsRunning = true;

        getRandomQuestion();
    }

    IEnumerator Wait1(){
        
        yield return new WaitForSeconds (1);
         timerIsRunning = true;

    }
    

 
}

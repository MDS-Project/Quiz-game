using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetScore : MonoBehaviour
{
    int score = QuizManager.getScore();
     [SerializeField]
    private Text Score;
     void Start ()
    {
        Score.text = score.ToString();
        QuizManager.setScore();

    }
}

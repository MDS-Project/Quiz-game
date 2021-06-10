using System.Collections;

 
public class Question
{
    public string fact;
    public virtual bool getTrue()
    {
        return false;
    }
    public virtual int getIndex()
    {
        return 0;
    }
    public Question(string q){
        fact = q;
    }
}


public class QuestionTF: Question
{
    public bool isTrue;
    public override bool getTrue()
    {
        return isTrue;
    }
    public QuestionTF (string q,bool T): base (q){
        isTrue = T;
    }
    
}

public class Question4: Question
{
    public string[] answers;
    public int correctIndex;
    public override int getIndex()
    {
        return correctIndex;
    }
    public Question4 (string q, string[] ans,int t): base (q){
        answers = ans;
        correctIndex = t;
    }
}


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
}


public class QuestionTF: Question
{
    public bool isTrue;
    public override bool getTrue()
    {
        return isTrue;
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
}


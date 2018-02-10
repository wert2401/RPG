using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewNPC", menuName = "RPG/Creatures/NPC")]
public class NPC : Creature
{
    public bool isAgressiveBase;
    public bool isAgressive;
    public int Money;

    //public List<Answer> Answers;
    //public List<Answer> PlAnswers;

    public Answer FAnswer;
    public Answer CAnswer;


    public override void SetEnemyUI()
    {
        UIManager.instance.AddDynButton("Поговорить", Talk, UIManager.instance.dynButtonsHolder.transform);
    }

    public override void Talk()
    {
        CAnswer = FAnswer;
        UIManager.instance.Print(FAnswer.Words);
        UIManager.instance.ClearDialogue();
        for (int i = 0; i < FAnswer.Answers.Count; i++)
        {
            UIManager.instance.AddDiaButton(FAnswer.PlAns[i], NPCT, UIManager.instance.dynButtonsHolder.transform,i);
        }
    }

    public virtual void NPCT(int id)
    {
        UIManager.instance.ClearDialogue();
        CAnswer = CAnswer.Answers[id];
        UIManager.instance.Print(CAnswer.Words);
        for (int i = 0; i < CAnswer.PlAns.Count; i++)
        {
            UIManager.instance.AddDiaButton(CAnswer.PlAns[i], NPCT, UIManager.instance.dynButtonsHolder.transform,i);
        }
    }
}
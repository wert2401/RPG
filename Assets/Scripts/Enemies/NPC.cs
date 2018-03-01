using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewNPC", menuName = "RPG/Creatures/NPC")]
public class NPC : Creature
{
    public int Money;
    public List<Item> Inventory;
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

    public override int GetMoney()
    {
        return Money;
    }

    public override void GetDrop()
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            InventoryManager.instance.AddItem(Inventory[i]);
        }
    }

    public void NPCT(int id)
    {
        UIManager.instance.ClearDialogue();
        CAnswer = CAnswer.Answers[id];
        UIManager.instance.Print(CAnswer.Words);
        for (int i = 0; i < CAnswer.PlAns.Count; i++)
        {
            UIManager.instance.AddDiaButton(CAnswer.PlAns[i], NPCT, UIManager.instance.dynButtonsHolder.transform,i);
        }
        //int a = id[0];
        //id.Clear();
        //UIManager.instance.Print(Answers[a].Words);
        //for (int i = 0; i < PlAnswers.Count; i++)
        //{
        //    id.Add(i);
        //    UIManager.instance.AddDynButton(PlAnswers[i].Words, PT, UIManager.instance.dynButtonsHolder.transform);
        //}
    }

    public void PT()
    {

    }
}
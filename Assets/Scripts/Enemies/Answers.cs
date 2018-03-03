using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewAnswer", menuName = "RPG/Answer")]
public class Answer : ScriptableObject
{
    public string Words;
    //public List<int> NoA;
    public List<string> PlAns;
    public List<Answer> Answers;
    public bool CanEndDialogue;
}
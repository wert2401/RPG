using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewAnswer", menuName = "RPG/Answer")]
public class Answer : ScriptableObject
{
    public string Words;
    public List<string> PlAns;
    public List<Answer> Answers;
    public bool CanEndDialogue;
}
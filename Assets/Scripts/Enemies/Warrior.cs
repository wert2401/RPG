using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewWarrior", menuName = "RPG/Creatures/NPC/Warrior")]
public class Warrior : NPC
{
    [MenuItem("Tools/MyTool/Do It in C#")]
    static void DoIt()
    {
        EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
    }
}
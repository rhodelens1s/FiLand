using System.Collections.Generic;
using UnityEngine;

// Create a ScriptableObject to hold the question data
[CreateAssetMenu(fileName = "New Question", menuName = "Quiz Game/Question")]
public class QuestionData : ScriptableObject
{
    public List<Question_> questions;
}

// Enum to define the question types
public enum QuestionType_
{
    MultipleChoice,
    FillInTheBlanks
}

[System.Serializable]
public class Question_
{
    public string questionInfo;         //question text
    public QuestionType_ questionType;   //type
    public List<string> options;        //options to select
    public string correctAns;           //correct option
}

// Create a ScriptableObject to hold the level data for each category
[CreateAssetMenu(fileName = "New Level", menuName = "Quiz Game/Level")]
public class LevelData : ScriptableObject
{
    public QuestionData[] preparedQuestions;
}


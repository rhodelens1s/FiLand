using System.Collections.Generic;
using UnityEngine;

public class QuizManagers : MonoBehaviour
{
    public LevelData[] categoryLevels;
    private int currentCategoryIndex;

    private void Start()
    {
        // Initialize game settings and start the first category
        currentCategoryIndex = 0;
        StartCategory();
    }

    private void StartCategory()
    {
        // Load questions for the current category and level
        QuestionData[] preparedQuestions = categoryLevels[currentCategoryIndex].preparedQuestions;

        // Randomly select 3 questions for the current level
        List<QuestionData> selectedQuestions = new List<QuestionData>();
        while (selectedQuestions.Count < 3)
        {
            QuestionData randomQuestion = preparedQuestions[Random.Range(0, preparedQuestions.Length)];
            if (!selectedQuestions.Contains(randomQuestion))
            {
                selectedQuestions.Add(randomQuestion);
            }
        }

        // Display the questions and handle user input
        foreach (QuestionData question in selectedQuestions)
        {
            // Show the question on the UI
            // Handle user input and check the answer
            // Update the score
        }
    }

    // Other methods to handle user input, progress through levels, etc.
}

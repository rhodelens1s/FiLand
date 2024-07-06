using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class tryDragAndDrop : MonoBehaviour
{
    // Start is called before the first frame update
    public string a;
    public GameObject LetterHolderPrefab;
    [SerializeField] private GameObject lettersHolder;
    [SerializeField] private List<GameObject> letters;
    [SerializeField] private List<GameObject> answer;

    private char[] wordsArray = new char[12];

    //[SerializeField] private  optionsWordList;

    void Start()
    {
        Letters_();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Letters_()
    {
        a = "Pangngalan";

        for (int i = 0; i < a.Length; i++)
        {
            wordsArray[i] = char.ToUpper(a[i]);
        }

        for (int j = a.Length; j < wordsArray.Length; j++)
        {
            wordsArray[j] = (char)UnityEngine.Random.Range(65, 90);
        }

        wordsArray = ShuffleList.ShuffleListItems<char>(wordsArray.ToList()).ToArray(); //Randomly Shuffle the words array

        //set the options words Text value
        for (int k = 0; k < a.Length; k++)
        {
            //a[k].SetWord(wordsArray[k]);
        }
    }
}

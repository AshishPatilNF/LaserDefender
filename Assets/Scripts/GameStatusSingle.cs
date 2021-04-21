using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatusSingle : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    int score = 0;

    private void Awake()
    {
        int objectCount = FindObjectsOfType<GameStatusSingle>().Length;

        if(objectCount > 1)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void AddScore(int NewScore)
    {
        score += NewScore;
        text.text = score.ToString();
    }

    public void TextFieldToUse(TextMeshProUGUI textToUse)
    {
        text = textToUse;
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetGame()
    {
        score = 0;
    }
}

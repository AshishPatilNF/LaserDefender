using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreText : MonoBehaviour
{
    GameStatusSingle gameStatus;

    private void Start()
    {
        gameStatus = FindObjectOfType<GameStatusSingle>();
        gameStatus.TextFieldToUse(GetComponent<TextMeshProUGUI>());
        GetComponent<TextMeshProUGUI>().text = gameStatus.GetScore().ToString();
    }
}

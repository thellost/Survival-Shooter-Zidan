using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static int score;
    private static ScoreManager _instance;
    public static ScoreManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ScoreManager();
        }
        return _instance;
    }
    TextMeshProUGUI text;


    void Awake ()
    {
        text = GetComponent<TextMeshProUGUI>();
        score = 0;
    }


    void Update ()
    {
        text.text = "Score: " + score;
    }
}

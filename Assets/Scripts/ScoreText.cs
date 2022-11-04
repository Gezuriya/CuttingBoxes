using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    TextMeshProUGUI text;
    public int score = -1;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        GameManager.OnCubeSpawned += GameManager_OnCubeSpawned;
    }
    private void OnDestroy()
    {
        GameManager.OnCubeSpawned -= GameManager_OnCubeSpawned;
    }
    void GameManager_OnCubeSpawned()
    {
        score++;
        text.text = score.ToString();
    }
}

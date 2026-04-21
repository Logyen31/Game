using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("Game Settings")]
    public int coins = 0;
    public float workDuration = 3f;
    public int minRewardCoins = 5;
    public int maxRewardCoins = 15;

    [Header("UI")]
    public TMP_Text coinsText;
    public TMP_Text timerText;
    public TMP_Text statusText;
    public Button workButton;

    private bool isWorking = false;

    private void Start()
    {
        UpdateUI();

        if (timerText != null)
            timerText.text = "";

        if (statusText != null)
            statusText.text = "Готово к работе";
    }

    public void StartWork()
    {
        Debug.Log("StartWork вызван");

        if (isWorking)
            return;

        StartCoroutine(WorkRoutine());
    }

    private IEnumerator WorkRoutine()
    {
        isWorking = true;

        if (workButton != null)
            workButton.interactable = false;

        if (statusText != null)
            statusText.text = "Разработка...";

        float timeLeft = workDuration;

        while (timeLeft > 0)
        {
            if (timerText != null)
                timerText.text = "Время: " + Mathf.Ceil(timeLeft).ToString();

            timeLeft -= Time.deltaTime;
            yield return null;
        }

        int reward = Random.Range(minRewardCoins, maxRewardCoins + 1);
        coins += reward;

        UpdateUI();

        if (timerText != null)
            timerText.text = "Награда: +" + reward;

        if (statusText != null)
            statusText.text = "Готово!";

        yield return new WaitForSeconds(1f);

        if (timerText != null)
            timerText.text = "";

        if (statusText != null)
            statusText.text = "Готово к работе";

        if (workButton != null)
            workButton.interactable = true;

        isWorking = false;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void UpdateUI()
    {
        if (coinsText != null)
            coinsText.text = "Монетки: " + coins.ToString();
    }
}
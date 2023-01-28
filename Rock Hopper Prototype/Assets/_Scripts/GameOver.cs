using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using LootLocker.Requests;

public class GameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI leaderboardScoreText;
    [SerializeField] TextMeshProUGUI leaderboardNameText;

    [SerializeField] TMP_InputField inputField;

    private int score = 0;
    private int leaderboardID = 11211;
    private int leaderboardTopCount = 10;

    public void StopGame(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
        GetLeaderboard();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SubmitScore()
    {
        StartCoroutine(SubmitScoreToLeaderboard());
    }

    private IEnumerator SubmitScoreToLeaderboard()
    {
        bool? nameSet = null;
        LootLockerSDKManager.SetPlayerName(inputField.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Player name submitted");
                nameSet = true;
            }
            else
            {
                Debug.Log("Not able to set name");
                nameSet = false;
            }
        });
        yield return new WaitUntil(() => nameSet.HasValue);
        bool? scoreSet = null;
        LootLockerSDKManager.SubmitScore("", score, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Player score set");
                scoreSet = true;
            }
            else
            {
                Debug.Log("Not able to set score");
                scoreSet = false;
            }
        });
        yield return new WaitUntil(() => scoreSet.HasValue);
        if (!scoreSet.Value) yield break;
        GetLeaderboard();
    }

    private void GetLeaderboard()
    {
        LootLockerSDKManager.GetScoreList(leaderboardID, leaderboardTopCount, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully retrieved leaderboard");
                string leaderboardName = "";
                string leaderboardScore = "";
                LootLockerLeaderboardMember[] members = response.items;
                for (int i = 0; i < members.Length; i++)
                {
                    if (members[i].player == null) continue;

                    if (members[i].player.name != "")
                    {
                        leaderboardName += members[i].player.name + "\n";
                    }
                    else
                    {
                        leaderboardName += members[i].player.id + "\n";
                    }
                    leaderboardScore += members[i].score + "\n";
                }
                leaderboardNameText.SetText(leaderboardName);
                leaderboardScoreText.SetText(leaderboardScore);
            }
            else
            {
                Debug.Log("Unable to retrive leaderboard");
            }
        });
    }
}

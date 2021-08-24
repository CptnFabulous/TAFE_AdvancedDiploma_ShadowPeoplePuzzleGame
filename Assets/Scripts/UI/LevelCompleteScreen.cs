using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteScreen : Menu
{
    public Text score;
    public Text highScore;
    //public Button nextLevelButton;
    //public Button quitButton;
    public void Populate(Player p)
    {
        score.text = "Finished with " + p.Health.lives + " lives remaining";
        string highscore = "Highscore: " + LevelData.Current.optimalLivesLeft + " lives";
        if (p.Health.lives >= LevelData.Current.optimalLivesLeft)
        {
            highscore = "Surpassed highscore by " + (p.Health.lives - LevelData.Current.optimalLivesLeft) + "!";
        }
        highScore.text = highscore;
        //nextLevelButton.onClick.RemoveAllListeners();
        //nextLevelButton.onClick.AddListener(LoadNextLevel);
        //quitButton.onClick.RemoveAllListeners();
        //quitButton.onClick.AddListener(QuitGame);
    }
}

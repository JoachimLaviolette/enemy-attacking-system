using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private Text UIScore;
    private Image UIHealth;
    private Player mPlayer;

    // Const
    private static int HEALTH_LEVEL_0 = 0;
    private static int HEALTH_LEVEL_1 = 1;
    private static int HEALTH_LEVEL_2 = 2;
    private static int HEALTH_LEVEL_3 = 3;
    private static int HEALTH_LEVEL_4 = 4;
    private static int HEALTH_LEVEL_5 = 5;
    private static int HEALTH_LEVEL_FULL = 6;
    
    private void Awake()
    {
        // Get the reference of the UI score element
        this.UIScore = gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
        // Get the reference of the UI health element
        this.UIHealth = gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Image>();
    }

    private void Start()
    {
        this.mPlayer = GameAssets.mInstance.GetPlayer();
    }

    private void Update()
    {
        Update_UI_Score();
        Update_UI_Health();
    }

    // Each frame update player's score on screen
    private void Update_UI_Score()
    {
        this.UIScore.text = "Score: " + this.mPlayer.GetScore();
    }

    // Each frame update player's health on screen
    private void Update_UI_Health()
    {
        float currentPlayerHealth = (float) this.mPlayer.GetHealth();

        // 0-6 HP sprite
        if (currentPlayerHealth <= 0f)
        {
            this.UIHealth.sprite = GameAssets.mInstance.GetHealthSprite(HEALTH_LEVEL_0);

            return;
        }

        // 1-6 HP sprite
        if (currentPlayerHealth <= ((float)((1f / 6f) * Player.TOTAL_HEALTH)))
        {
            this.UIHealth.sprite = GameAssets.mInstance.GetHealthSprite(HEALTH_LEVEL_1);

            return;
        }

        // 2-6 HP sprite
        if (currentPlayerHealth <= ((float)((2f / 6f) * Player.TOTAL_HEALTH)))
        {
            this.UIHealth.sprite = GameAssets.mInstance.GetHealthSprite(HEALTH_LEVEL_2);

            return;
        }

        // 3-6 HP sprite
        if (currentPlayerHealth <= ((float)((3f / 6f) * Player.TOTAL_HEALTH)))
        {
            this.UIHealth.sprite = GameAssets.mInstance.GetHealthSprite(HEALTH_LEVEL_3);

            return;
        }

        // 4-6 HP sprite
        if (currentPlayerHealth <= ((float)((4f / 6f) * Player.TOTAL_HEALTH)))
        {
            this.UIHealth.sprite = GameAssets.mInstance.GetHealthSprite(HEALTH_LEVEL_4);

            return;
        }

        // 5-6 HP sprite
        if (currentPlayerHealth <= ((float)((5f / 6f) * Player.TOTAL_HEALTH)))
        {
            this.UIHealth.sprite = GameAssets.mInstance.GetHealthSprite(HEALTH_LEVEL_5);

            return;
        }

        // If player's health > 5/6 * TOTAL_HEALTH
        // Full HP sprite
        this.UIHealth.sprite = GameAssets.mInstance.GetHealthSprite(HEALTH_LEVEL_FULL);
    }
}

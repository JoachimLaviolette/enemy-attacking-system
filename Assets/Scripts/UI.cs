using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // UI Components
    private Text UIScore;
    private Image UIHealth;
    private Image UIParticleCanon;

    // Models
    private Player mPlayer;

    // Const
    private const int HEALTH_LEVEL_0 = 0;
    private const int HEALTH_LEVEL_1 = 1;
    private const int HEALTH_LEVEL_2 = 2;
    private const int HEALTH_LEVEL_3 = 3;
    private const int HEALTH_LEVEL_4 = 4;
    private const int HEALTH_LEVEL_5 = 5;
    private const int HEALTH_LEVEL_FULL = 6;

    private const int PARTICLE_CANON_AMOUNT_0 = 0;
    private const int PARTICLE_CANON_AMOUNT_1 = 1;
    private const int PARTICLE_CANON_AMOUNT_2 = 2;
    private const int PARTICLE_CANON_AMOUNT_3 = 3;
    private const int PARTICLE_CANON_AMOUNT_4 = 4;
    private const int PARTICLE_CANON_AMOUNT_FULL = 5;

    // Tags
    private const string tag_UIScore = "UIScore";
    private const string tag_UIHealth = "UIHealth";
    private const string tag_UIParticleCanon = "UIParticleCanon";

    private void Awake()
    {
        // Get the reference of the UI score element (text)
        this.UIScore = GameObject.FindGameObjectWithTag(tag_UIScore).GetComponentInChildren<Text>();
        // Get the reference of the UI health element (image)
        this.UIHealth = GameObject.FindGameObjectWithTag(tag_UIHealth).GetComponentInChildren<Image>();
        // Get the reference of the UI particle canon element (image)
        this.UIParticleCanon = GameObject.FindGameObjectWithTag(tag_UIParticleCanon).GetComponentInChildren<Image>();
    }

    private void Start()
    {
        this.mPlayer = GameAssets.mInstance.GetPlayer();
    }

    private void Update()
    {
        Update_UI_Score();
        Update_UI_Health();
        Update_UI_ParticleCanon();
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

    // Each frame update player's particle canon munitions on screen
    private void Update_UI_ParticleCanon()
    {
        int currentPlayerParticleCanonMunitions = this.mPlayer.GetParticleCanonMunitions();

        // 0 munition
        if (currentPlayerParticleCanonMunitions == 0)
        {
            this.UIParticleCanon.sprite = GameAssets.mInstance.GetParticleCanonSprite(PARTICLE_CANON_AMOUNT_0);

            return;
        }

        // 1 munitions
        if (currentPlayerParticleCanonMunitions == 1)
        {
            this.UIParticleCanon.sprite = GameAssets.mInstance.GetParticleCanonSprite(PARTICLE_CANON_AMOUNT_1);

            return;
        }

        // 2 munitions
        if (currentPlayerParticleCanonMunitions == 2)
        {
            this.UIParticleCanon.sprite = GameAssets.mInstance.GetParticleCanonSprite(PARTICLE_CANON_AMOUNT_2);

            return;
        }

        // 3 munitions
        if (currentPlayerParticleCanonMunitions == 3)
        {
            this.UIParticleCanon.sprite = GameAssets.mInstance.GetParticleCanonSprite(PARTICLE_CANON_AMOUNT_3);

            return;
        }

        // 4 munitions
        if (currentPlayerParticleCanonMunitions == 4)
        {
            this.UIParticleCanon.sprite = GameAssets.mInstance.GetParticleCanonSprite(PARTICLE_CANON_AMOUNT_4);

            return;
        }

        // If player's particle canon munitions = 5
        // Full munitions sprite
        this.UIParticleCanon.sprite = GameAssets.mInstance.GetParticleCanonSprite(PARTICLE_CANON_AMOUNT_FULL);
    }
}

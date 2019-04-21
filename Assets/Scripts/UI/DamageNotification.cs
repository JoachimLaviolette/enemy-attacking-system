using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNotification : MonoBehaviour
{
    // The speed the notification moves at
    private float mSpeed;
    private Vector3 mMoveVector;
    // Time the notification disappear after
    private float mSpawnTime;
    // Speed the notification disappear at
    private float mDisappearSpeed;
    // The text mesh pro attached to the notification
    private TextMeshPro mTextMesh;
    // The damage notification prefab
    private static DamageNotification mPrefab;
    // Notification text color
    private Color mTextColor;

    private static int sortingOrder = 0;

    // Const
    private const int FONT_SIZE_NORMAL = 3;
    private const int FONT_SIZE_CRITICAL = 4;

    private const string FONT_COLOR_BODY_NO_DAMAGE = "C1C1C1";
    private const string FONT_COLOR_BODY_NORMAL = "F4F59F";
    private const string FONT_COLOR_BODY_CRITICAL = "F67580";

    private const string FONT_COLOR_OUTLINE_NO_DAMAGE = "757575";
    private const string FONT_COLOR_OUTLINE_NORMAL = "EE911C";
    private const string FONT_COLOR_OUTLINE_CRITICAL = "A41915";

    private const float FONT_DISAPPEAR_TIME_MAX = 1f;

    private void Awake()
    {
        this.mTextMesh = this.transform.GetComponent<TextMeshPro>();
    }

    // Set up the damage notification properties
    private void Setup(int damages, bool isCritical)
    {
        this.mTextMesh.SetText(damages.ToString());
        this.SetupFont(damages, isCritical);
        sortingOrder++;
        this.mTextMesh.sortingOrder = sortingOrder;
        this.mSpeed = 2f;
        this.mMoveVector = new Vector3(1f, 1f) * this.mSpeed;
        this.mSpawnTime = FONT_DISAPPEAR_TIME_MAX;
        this.mDisappearSpeed = 4f;
    }

    // Set up the notification font
    private void SetupFont(int damages, bool isCritical)
    {
        if (damages == 0)
        {
            this.mTextMesh.fontSize = FONT_SIZE_NORMAL;
            this.mTextColor = this.mTextMesh.color = this.mTextMesh.faceColor = Utils.GetColorFromString(FONT_COLOR_BODY_NO_DAMAGE);
            this.mTextMesh.outlineColor = Utils.GetColorFromString(FONT_COLOR_OUTLINE_NO_DAMAGE);

            return;
        }

        this.mTextMesh.fontSize = FONT_SIZE_NORMAL;
        this.mTextColor = this.mTextMesh.color = this.mTextMesh.faceColor = Utils.GetColorFromString(FONT_COLOR_BODY_NORMAL);
        this.mTextMesh.outlineColor = Utils.GetColorFromString(FONT_COLOR_OUTLINE_NORMAL);

        if (isCritical)
        {
            this.mTextMesh.fontSize = FONT_SIZE_CRITICAL;
            this.mTextColor = this.mTextMesh.color = this.mTextMesh.faceColor = Utils.GetColorFromString(FONT_COLOR_BODY_CRITICAL);
            this.mTextMesh.outlineColor = Utils.GetColorFromString(FONT_COLOR_OUTLINE_CRITICAL);
        }
    }

    // Update the damage notification every frame
    private void Update()
    {
        this.HandleMovements();
        this.HandleFontScaling();
        this.HandleDisappearance();
    }

    // Handle how the notificatgion moves on screen
    private void HandleMovements()
    {
        this.transform.position += this.mMoveVector * Time.deltaTime;
        this.mMoveVector -= this.mMoveVector * 8f * Time.deltaTime;
    }
    
    private void HandleFontScaling()
    {
        float scaleFactor = 1f;
        this.transform.localScale -= Vector3.one * scaleFactor * Time.deltaTime;

        if (this.mSpawnTime > FONT_DISAPPEAR_TIME_MAX * .5f)
        {
            this.transform.localScale += Vector3.one * scaleFactor * Time.deltaTime;
        }
    }

    // Handle the disappearance of the notification
    private void HandleDisappearance()
    {
        this.mSpawnTime -= Time.deltaTime;

        if (this.mSpawnTime < 0)
        {
            // Start to disappear
            this.mTextColor.a -= this.mDisappearSpeed * Time.deltaTime;
            this.mTextMesh.color = this.mTextColor;

            // Destroy the notification if finished to disappear
            if (this.mTextColor.a < 0f)
            {
                this.Destroy();
            }
        }
    }

    // Destroy the notification
    private void Destroy()
    {
        Destroy(this.gameObject);
    }

    private static void SetupPrefab()
    {
        mPrefab = GameAssets.mInstance.GetDamageNotification();
    }

    // Create a damage notification
    public static DamageNotification Create(Vector3 spawnPosition, int damages, bool isCritical)
    {
        SetupPrefab();

        Transform damageNotificationTransform = Instantiate(mPrefab.transform, spawnPosition, Quaternion.identity);

        DamageNotification damageNotification = damageNotificationTransform.GetComponent<DamageNotification>();
        damageNotification.Setup(damages, isCritical);

        return damageNotification;
    }
}

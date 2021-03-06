﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Rewired;
using EZCameraShake;

public class PlayerController : MonoBehaviour
{

    public static bool hasClicked = false;

    [Header("Rewired Player Settings")]
    public int _PlayerId;
    private Rewired.Player inputPlayer;

    [Header("Cursor Object")]
    public Image cursor;
    private RaycastHit hit;
    public float raduisSphereCast = 0.5f;
    [Space(5)]
    public float leftMax;
    public float rightMax;
    public float UpMax;
    public float DownMax;

    [Header("Player Settings")]
    public LayerMask layerMask;
    public float moveSpeedValue;
    private float cooldownFailHit;
    public float cooldownFailHitMax;
    [HideInInspector] public int score;
    private bool canHit;
    public PlayersVar players;

    [Header("Player UI")]
    public Text hitText;
    public Image crosshairUI;
    public GameObject hitParticleNormal; //hitball2 in Ultimate VFX package
    public GameObject hitParticleImpostor; //spark_twinkle in Ultimate VFX package

    [Header("Camera Shaker")]
    public float magnitude;
    public float roughness;
    public float fadeIn;
    public float fadeOut;

    // Start is called before the first frame update
    private void Start()
    {
        inputPlayer = ReInput.players.GetPlayer(_PlayerId);

        hit = new RaycastHit();

        canHit = true;

        ScoreUpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state == EnumStateScene.Level)
        {
            if (!canHit)
            {
                cooldownFailHit += Time.deltaTime;

                crosshairUI.fillAmount += (cooldownFailHit / cooldownFailHitMax) / 100;
                if (cooldownFailHit >= cooldownFailHitMax)
                {
                    cooldownFailHit = 0;
                    canHit = true;
                }
            }
            else
            {
                if (inputPlayer.GetButtonDown("Select") && !hasClicked)
                {
                    CursorRaycast();
                }
            }

            CursorMovement();
        }
        else if (GameManager.instance.state == EnumStateScene.FinalScore)
        {
            if (inputPlayer.GetButtonDown("Restart"))
            {
                SoundManager.instance.StopMusic();
                SceneManager.LoadScene("GameScene");
                //GameManager.instance.Init();
            }
            else if (inputPlayer.GetButtonDown("Return"))
            {
                SoundManager.instance.StopMusic();
                SceneManager.LoadScene("Menu");
                GameManager.instance.state = EnumStateScene.Menu;
            }
        }

        if (GameManager.instance.state == EnumStateScene.Level || GameManager.instance.state == EnumStateScene.Pause)
        {

            if (inputPlayer.GetButtonDown("Pause"))
            {
                GameManager.instance.Pause();
            }
        }
    }

    private void CursorMovement()
    {
        float horizontal = 0;
        horizontal = inputPlayer.GetAxis("HorizontalMove");
        float vertical = 0;
        vertical = inputPlayer.GetAxis("VerticalMove");

        Vector2 actualPosition = cursor.rectTransform.anchoredPosition;

        actualPosition.x += horizontal * moveSpeedValue * Time.deltaTime * 100;
        actualPosition.y += vertical * moveSpeedValue * Time.deltaTime * 100;

        if (actualPosition.x < leftMax)
        {
            actualPosition.x = leftMax;
        }
        else if (actualPosition.x > rightMax)
        {
            actualPosition.x = rightMax;
        }

        if (actualPosition.y < DownMax)
        {
            actualPosition.y = DownMax;
        }
        else if (actualPosition.y > UpMax)
        {
            actualPosition.y = UpMax;
        }

        cursor.rectTransform.anchoredPosition = actualPosition;
    }

    private void CursorRaycast()
    {
        var mainCam = Camera.main;

        if (Physics.SphereCast(mainCam.transform.localPosition, raduisSphereCast, (cursor.transform.position - mainCam.transform.position).normalized, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.transform.gameObject.layer == 9)
            {
                score++;
                OnHitSomeonePlayThis(true);
                SoundManager.instance.PlayYes();
                if (_PlayerId == 0)
                {
                    PlayerUI.instance.panelScoreJ1.GetComponent<Animator>().SetBool("Change", !PlayerUI.instance.panelScoreJ1.GetComponent<Animator>().GetBool("Change"));
                }
                else
                {
                    PlayerUI.instance.panelScoreJ2.GetComponent<Animator>().SetBool("Change", !PlayerUI.instance.panelScoreJ2.GetComponent<Animator>().GetBool("Change"));
                }
            }
            else
            {
                if (_PlayerId == 0)
                {
                    players.player2.score++;
                    players.player2.ScoreUpdateText();
                }
                else
                {
                    players.player1.score++;
                    players.player1.ScoreUpdateText();
                }
                //canHit = false;
                //crosshairUI.fillAmount = 0;
                SoundManager.instance.PlayNo();
                CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeIn, fadeOut);
                OnHitSomeonePlayThis(false);
            }
            ScoreUpdateText();
            hasClicked = false;
            GameManager.instance.NextRound();
        }
//#if UNITY_EDITOR
//        Debug.DrawRay(mainCam.transform.localPosition, (cursor.transform.position - mainCam.transform.position).normalized * 100000f, Color.yellow, 0.5f);
//        Debug.Break();
//#endif
    }

    public void OnHitSomeonePlayThis(bool isImpostor)
    {
        if (isImpostor)
        {
            GameObject go = Instantiate(hitParticleImpostor, hit.point, Quaternion.identity);
            go.transform.parent = GameManager.instance.plateauTournant.transform;
            go.name = hitParticleImpostor.name + "(Clone)";
            Destroy(go, 2f);
        }
        else
        {
            GameObject go = Instantiate(hitParticleNormal, hit.point, Quaternion.identity);
            go.transform.parent = GameManager.instance.plateauTournant.transform;
            go.name = hitParticleNormal.name + "(Clone)";
            Destroy(go, 2f);
        }
    }

    public void ScoreUpdateText()
    {
        if (score < 10)
        {
            hitText.text = "0" + score;
        }
        else
        {

            hitText.text = score.ToString();
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        if(hit.transform != null)
            Gizmos.DrawSphere(hit.transform.position, raduisSphereCast);
    }
#endif
}

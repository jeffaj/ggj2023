using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroModalController : MonoBehaviour
{
    const string GAME_SCENE_NAME = "GameScene";

    public static bool WasPresented = false;

    bool enterPressAllowed;

    public void Present()
    {
        var loreText = transform.Find("Lore").GetComponent<TextMeshProUGUI>();
        var tutorialController = transform.Find("Tutorial").GetComponent<TutorialController>();
        var enterText = transform.Find("PressEnter").GetComponent<TextMeshProUGUI>();

        loreText.alpha = 0;
        tutorialController.SetAlpha(0);
        enterText.alpha = 0;

        gameObject.SetActive(true);
        WasPresented = true;

        StartCoroutine(PresentLore(loreText, () =>
        {
            StartCoroutine(PresentTutorial(tutorialController, () =>
            {
                StartCoroutine(PresentEnter(enterText, () =>
                {

                }));
            }));
        }));
    }

    private IEnumerator PresentLore(TextMeshProUGUI loreText, UnityAction callback)
    {
        float fadeDuration = 1f;
        float startTime = Time.time;
        while (startTime + fadeDuration > Time.time)
        {
            loreText.alpha = (Time.time - startTime) / fadeDuration;
            yield return null;
        }

        loreText.alpha = 1;

        float loreReadTime = 2f;
        yield return new WaitForSeconds(loreReadTime);

        callback();
    }

    private IEnumerator PresentTutorial(TutorialController tutorial, UnityAction callback)
    {
        float fadeDuration = 1f;
        float startTime = Time.time;
        while (startTime + fadeDuration > Time.time)
        {
            tutorial.SetAlpha((Time.time - startTime) / fadeDuration);
            yield return null;
        }

        tutorial.SetAlpha(1);

        float tutorialReadTime = 1f;
        yield return new WaitForSeconds(tutorialReadTime);

        callback();
    }

    private IEnumerator PresentEnter(TextMeshProUGUI enterText, UnityAction callback)
    {
        enterPressAllowed = true;

        float fadeDuration = 1f;
        float startTime = Time.time;
        while (startTime + fadeDuration > Time.time)
        {
            enterText.alpha = (Time.time - startTime) / fadeDuration;
            yield return null;
        }

        enterText.alpha = 1;

        callback();
    }

    private void Update()
    {
        if (enterPressAllowed && Players.PlayerInput.ReturnPressed)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayUIConfirm();
            }

            SceneManager.LoadScene(GAME_SCENE_NAME);
        }
    }
}

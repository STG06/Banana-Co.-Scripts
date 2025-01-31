using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Victory_Gameover : MonoBehaviour
{

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI victoryText;

    public GameObject victoryScrene;
    public GameObject gameOverScrene;

    private float fadeDuration = 2.0f;

    public void Victory()
    {
        victoryScrene.SetActive(true);
        StartCoroutine(FadeInText(victoryText));
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void GameOver()
    {
        gameOverScrene.SetActive(true);
        StartCoroutine(FadeInText(gameOverText));
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }



    private IEnumerator FadeInText(TextMeshProUGUI text)
    {
        Color originalColor = text.color;
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);

            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);

    }
}

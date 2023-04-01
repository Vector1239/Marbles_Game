using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSelector : MonoBehaviour
{
    [SerializeField] private GameObject trailSelector;
    [SerializeField] private GameObject player;

    private TrailRenderer trailRenderer;
    private Gradient gradient;
    public void selectTrail()
    {
        trailSelector.SetActive(true);
        Time.timeScale = 0f;
    }

    public void blueIT()
    {
        trailSelector.SetActive(false);
        Time.timeScale = 1f;

        Transform trailTransform = player.transform.Find("trail");
        TrailRenderer trailRenderer = trailTransform.GetComponent<TrailRenderer>();

        gradient = new Gradient();
        GradientColorKey[] colorKey = new GradientColorKey[2];
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[3];

        Color gradientColor = Color.clear;
        ColorUtility.TryParseHtmlString("#186EB0", out gradientColor);
        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey[0].color = gradientColor;
        colorKey[0].time = 0.0f;

        ColorUtility.TryParseHtmlString("#51FFD0", out gradientColor);
        colorKey[1].color = gradientColor;
        colorKey[1].time = 55.6f;
        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 72.4f;
        alphaKey[2].alpha = 0.0f;
        alphaKey[2].time = 100.0f;
        gradient.SetKeys(colorKey, alphaKey);
        trailRenderer.colorGradient = gradient;
    }

    public void orangeIT()
    {
        trailSelector.SetActive(false);
        Time.timeScale = 1f;
        trailRenderer = player.GetComponentInChildren<TrailRenderer>();

        gradient = new Gradient();
        GradientColorKey[] colorKey = new GradientColorKey[2];
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[3];

        Color gradientColor = Color.clear;
        ColorUtility.TryParseHtmlString("#FF3B10", out gradientColor);
        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey[0].color = gradientColor;
        colorKey[0].time = 0.0f;

        ColorUtility.TryParseHtmlString("#FF802A", out gradientColor);
        colorKey[1].color = gradientColor;
        colorKey[1].time = 55.6f;
        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 72.4f;
        alphaKey[2].alpha = 0.0f;
        alphaKey[2].time = 100.0f;
        gradient.SetKeys(colorKey, alphaKey);
        trailRenderer.colorGradient = gradient;
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSearchUI : MonoBehaviour
{
    [SerializeField] private Slider searchSlider;

    public void InitializeSearchBar(float maxSearch)
    {
        searchSlider.maxValue = maxSearch;
        searchSlider.value = 0f;
    }

    public void UpdateSearchUI(float newValue)
    {
        searchSlider.value = newValue;
    }
}

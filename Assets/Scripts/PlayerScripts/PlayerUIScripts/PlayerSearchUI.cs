using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSearchUI : MonoBehaviour
{
    [SerializeField] private Slider searchSlider;

    private void Update()
    {

    }

    public void InitializeSearchBar(float maxSearch)
    {
        searchSlider.maxValue = maxSearch;
        searchSlider.value = maxSearch;
    }

    public void UpdateSearchUI(float newValue)
    {
        searchSlider.value = newValue;
    }
}

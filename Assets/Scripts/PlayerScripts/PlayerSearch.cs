using System.Collections.Generic;
using UnityEngine;

public class PlayerSearch : MonoBehaviour
{
    [SerializeField] PlayerSearchUI searchUI;
    private readonly float maxSearch = 1f;
    private float currentSearch;

    void Update()
    {
        // Fill later.
    }

    void Start()
    {
        currentSearch = maxSearch;
        searchUI.InitializeSearchBar(maxSearch);
    }

    public void UpdateSearch(float value)
    {
        currentSearch = Mathf.Clamp(value, 0, maxSearch);
        searchUI.UpdateSearchUI(currentSearch);
    }

    public void ResetSearch()
    {
        currentSearch = maxSearch;
        searchUI.UpdateSearchUI(currentSearch);
    }
}
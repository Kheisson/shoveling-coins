using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.Objects.Placemat
{
    public class PlacematView : MonoBehaviour
    {
        #region --- Fields ---

        [SerializeField] private Slider progressBar;
        [SerializeField] private TextMeshProUGUI progressText;

        #endregion


        #region --- Public Methods ---

        public void UpdateProgress(int currentWorth, int requiredWorth)
        {
            if (progressBar != null)
            {
                progressBar.value = (float)currentWorth / requiredWorth;
            }

            if (progressText != null)
            {
                progressText.text = $"{currentWorth} / {requiredWorth}";
            }
        }

        public void OnPlacematFull()
        {
            Debug.Log("Placemat is full!");
        }

        #endregion
    }
}
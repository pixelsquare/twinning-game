using UnityEngine;
using UnityEngine.UI;

namespace PxlSq.Game
{
    /// <summary>
    /// Combo view
    /// </summary>
    public class ComboView : MonoBehaviour
    {
        [SerializeField] private Image _comboSlider;

        /// <summary>
        /// Sets the combo slider active
        /// </summary>
        /// <param name="active"></param>
        public void SetComboSliderActive(bool active)
        {
            _comboSlider.gameObject.SetActive(active);
        }

        /// <summary>
        /// Updates the combo slider progress
        /// </summary>
        /// <param name="percent"></param>
        public void UpdateComboSlider(float percent)
        {
            _comboSlider.fillAmount = percent;
        }
    }
}

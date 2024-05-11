using UnityEngine;
using UnityEngine.UI;

namespace PxlSq.Game
{
    public class ComboView : MonoBehaviour
    {
        [SerializeField] private Image _comboSlider;

        public void SetComboSliderActive(bool active)
        {
            _comboSlider.gameObject.SetActive(active);
        }

        public void UpdateComboSlider(float percent)
        {
            _comboSlider.fillAmount = percent;
        }
    }
}

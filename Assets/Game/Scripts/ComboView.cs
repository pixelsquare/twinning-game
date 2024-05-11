using UnityEngine;
using UnityEngine.UI;

namespace PxlSq.Game
{
    public class ComboView : MonoBehaviour
    {
        [SerializeField] private Image _comboSlider;

        public void UpdateComboSlider(float percent)
        {
            _comboSlider.fillAmount = percent;
        }
    }
}

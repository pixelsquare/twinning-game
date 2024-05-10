using TMPro;
using UnityEngine;

namespace PxlSq.Game
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _turnCountText;
        [SerializeField] private TMP_Text _matchCountText;

        public void UpdateTurnCount(uint turnCount)
        {
            _turnCountText.text = $"Turns: {turnCount}";
        }

        public void UpdateMatchCount(uint matchCount)
        {
            _matchCountText.text = $"Matches: {matchCount}";
        }
    }
}

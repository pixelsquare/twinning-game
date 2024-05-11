using UnityEngine;

namespace PxlSq.Game
{
    /// <summary>
    /// Game configurations
    /// </summary>
    [CreateAssetMenu(menuName = "PxlSq/Configs/GameConfig", fileName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        private static GameConfig _instance;
        public static GameConfig Instance
        {
            get
            {
                _instance = Resources.Load<GameConfig>($"Configs/{nameof(GameConfig)}");
                Debug.Assert(_instance != null, "Unable to find GameConfig. Please make sure it is located at `Resources/Configs`");
                return _instance;
            }
        }

        [SerializeField] private uint _scorePoint;
        [SerializeField] private uint _turnLimit;
        [SerializeField] private uint _comboMultiplier = 100;
        [SerializeField] private BoardSize _boardSize = new(2, 2);

        public uint ScorePoint => _scorePoint;
        public uint TurnLimit => _turnLimit;
        public float ComboMultiplier => _comboMultiplier / 100f;
        public BoardSize BoardSize => _boardSize;

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Configs/GameConfig")]
        private static void InitConfig()
        {
            var instance = Instance;
            UnityEditor.Selection.activeObject = instance;
            UnityEditor.EditorGUIUtility.PingObject(instance);
        }
#endif
    }
}

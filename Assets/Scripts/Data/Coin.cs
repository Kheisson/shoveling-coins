using InGame.Objects;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "NewCoin", menuName = "Game/Coin")]
    public class Coin : ScriptableObject
    {
        #region --- Enums ---
        
        public enum CoinType
        {
            Gold,
            Silver,
            Bronze,
        }
        
        #endregion
        
        
        #region --- Fields ---
        
        [SerializeField] private CoinObject _coinObject;
        [SerializeField] private CoinType _type;
        [SerializeField] private int _worth;
        
        #endregion
        
        
        #region --- Properties ---
        
        public CoinObject CoinObject => _coinObject;
        public CoinType Type => _type;
        public int Worth => _worth;
        
        #endregion
    }
}
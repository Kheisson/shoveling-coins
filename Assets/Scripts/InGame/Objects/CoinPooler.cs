using System.Collections.Generic;
using Data;
using Infra;
using UnityEngine;

namespace InGame.Objects
{
    public class CoinPooler : MonoBehaviour, IService
    {
        #region --- Fields ---
        
        [SerializeField] private Coin[] _coins;
        private Dictionary<Coin.CoinType, Queue<CoinObject>> _coinPools;
        
        #endregion
        
        
        #region --- MonoBehaviour Methods ---

        private void Awake()
        {
            ServiceLocator.RegisterService(this);

            _coinPools = new Dictionary<Coin.CoinType, Queue<CoinObject>>();

            foreach (var coin in _coins)
            {
                _coinPools[coin.Type] = new Queue<CoinObject>();
            }
        }
        
        private void OnDestroy()
        {
            ServiceLocator.UnregisterService<CoinPooler>();
        }
        
        #endregion
        
        
        #region --- Public Methods ---

        public CoinObject GetCoinObject(Coin.CoinType type)
        {
            if (_coinPools[type].Count == 0)
            {
                foreach (var coin in _coins)
                {
                    if (coin.Type != type) continue;
                    
                    var newCoin = Instantiate(coin.CoinObject);
                    newCoin.Initialize(coin);
                    newCoin.gameObject.SetActive(false);
                    
                    return newCoin;
                }
            }
            else
            {
                var oldCoin = _coinPools[type].Dequeue();
                oldCoin.gameObject.SetActive(true); 
                
                return oldCoin;
            }

            return null;
        }

        public void ReturnCoinObject(CoinObject coinObject, Coin.CoinType type)
        {
            coinObject.gameObject.SetActive(false);
            _coinPools[type].Enqueue(coinObject);
        }
        
        #endregion
    }
}
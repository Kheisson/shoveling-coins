using Infra;
using UnityEngine;

namespace InGame.Objects.Placemat
{
    public class PlacematController : MonoBehaviour
    {
        #region --- Fields ---
        
        [SerializeField] private PlacematView _view;
        private PlacematModel _model;
        private CoinPooler _coinPooler;
        
        #endregion
        
        
        #region --- Unity Methods ---
        
        private void Awake()
        {
            _model = new PlacematModel(100);
            _model.OnProgressChanged += _view.UpdateProgress;
            _model.OnPlacematFull += _view.OnPlacematFull;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger detected!");
            
            if (!other.TryGetComponent(out CoinObject coinObject)) return;
            
            AddCoin(coinObject.Coin);
            
            if (_coinPooler == null)
            {
                _coinPooler = ServiceLocator.GetService<CoinPooler>();
            }
            
            _coinPooler.ReturnCoinObject(coinObject, coinObject.Coin.Type);
        }

        #endregion
        
        
        #region --- Public Methods ---

        private void AddCoin(Data.Coin coin)
        {
            _model.AddCoin(coin);
        }
        
        #endregion
    }
}
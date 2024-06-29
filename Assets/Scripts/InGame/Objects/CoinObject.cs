using Data;
using UnityEngine;
using Utils;

namespace InGame.Objects
{
    public class CoinObject : MonoBehaviour
    {
        #region --- Fields ---
        
        private Coin _coin;
        private Rigidbody _rigidbody;
        
        #endregion
        
        
        #region --- Properties ---
        
        public Coin Coin => _coin;
        
        #endregion
        
        
        #region --- Public Methods ---
        
        public void Initialize(Coin coin)
        {
            if (_coin != null) return;
            
            _coin = coin;
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        #endregion
        
        
        #region --- MonoBehaviour Methods ---
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(Constants.PLAYER_TAG)) return;
            
            var direction = (transform.position - other.transform.position).normalized; 
            _rigidbody.AddForce(direction * 0.5f, ForceMode.Impulse);
        }
        
        #endregion
    }
}
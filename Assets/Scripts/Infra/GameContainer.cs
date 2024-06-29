using Data;
using InGame.Objects;
using UnityEngine;

namespace Infra
{
    public class GameContainer : MonoBehaviour, IService
    {
        #region --- Fields ---
        
        private static GameContainer _instance;
        
        #endregion
        
        
        #region --- Properties ---

        public static GameContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameContainer>();
                }
                
                return _instance;
            }
        }
        
        #endregion
        
        
        #region --- MonoBehaviour Methods ---
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(gameObject);
        }
        
        private void Start()
        {
            TempQueueInit();
        }
        
        #endregion
        
        
        #region --- Public Methods ---
        
        public void Initialize()
        {
            // Initialize services here
        }
        
        #endregion
        
        
        #region --- Private Methods ---

        private void TempQueueInit()
        {
           var coinPooler = ServiceLocator.GetService<CoinPooler>();
           
           for (var i = 0; i < 50; i++)
           {
               var coin = coinPooler.GetCoinObject(Coin.CoinType.Bronze);
               coin.transform.position = new Vector3(668, 3, 672);
               coin.gameObject.SetActive(true);
           }
        }
        
        #endregion
    }
}
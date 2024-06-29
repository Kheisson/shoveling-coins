using System;
using Data;

namespace InGame.Objects.Placemat
{
    public class PlacematModel
    {
        #region --- Properties ---

        public int RequiredWorth { get; private set; }
        public int CurrentWorth { get; private set; }

        #endregion


        #region --- Events ---

        public event Action OnPlacematFull;
        public event Action<int, int> OnProgressChanged;

        #endregion


        #region --- Constructor ---

        public PlacematModel(int requiredWorth)
        {
            RequiredWorth = requiredWorth;
            CurrentWorth = 0;
        }

        #endregion


        #region --- Public Methods ---

        public void AddCoin(Coin coin)
        {
            if (coin == null) return;

            CurrentWorth += coin.Worth;
            OnProgressChanged?.Invoke(CurrentWorth, RequiredWorth);

            if (CurrentWorth >= RequiredWorth)
            {
                OnPlacematFull?.Invoke();
            }
        }

        #endregion
    }
}
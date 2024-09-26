using R3;
using Utils;
using UnityEngine;

namespace Services.Currency
{
    public class CurrencyService
    {
        public ReadOnlyReactiveProperty<float> CurrencyAmount => _currencyAmount;

        private ReactiveProperty<float> _currencyAmount = new();

        public CurrencyService()
        {
            UpdateAmount();
        }

        public bool TryChangeCurrencyForAmont(float value)
        {
            if (value < 0 && IsEnoughCurrency(value) == false)
            {
                return false;
            }

            _currencyAmount.Value += value;
            PlayerPrefs.SetFloat(SaveKey.CURRENCY_KEY, _currencyAmount.Value);
            return true;
        }

        public bool IsEnoughCurrency(float value)
        {
            if(_currencyAmount.Value < Mathf.Abs(value)) return false; 
            return true;
        }

        public void UpdateAmount()
        {
            if (PlayerPrefs.HasKey(SaveKey.CURRENCY_KEY))
            {
                _currencyAmount.Value = PlayerPrefs.GetFloat(SaveKey.CURRENCY_KEY);
            }
            else
            {
                _currencyAmount.Value = 0f;
                PlayerPrefs.SetFloat(SaveKey.CURRENCY_KEY, _currencyAmount.Value);
            }
        }
    }
}
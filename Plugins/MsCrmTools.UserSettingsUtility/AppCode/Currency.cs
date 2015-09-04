using Microsoft.Xrm.Sdk;

namespace MsCrmTools.UserSettingsUtility.AppCode
{
    internal class Currency
    {
        private readonly Entity currency;

        public Currency(Entity currency)
        {
            this.currency = currency;
        }

        public EntityReference CurrencyReference
        {
            get { return currency.ToEntityReference(); }
        }

        public override string ToString()
        {
            return currency.GetAttributeValue<string>("currencyname");
        }
    }
}
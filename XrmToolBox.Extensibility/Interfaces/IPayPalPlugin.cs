using System;

namespace XrmToolBox.Extensibility.Interfaces
{
    /// <summary>
    /// This interface allows a user to specify Paypal account
    /// to add a menu to redirect end user to Paypal donation page
    /// </summary>
    public interface IPayPalPlugin
    {
        /// <summary>
        /// Description to add in Donation page
        /// </summary>
        String DonationDescription { get; }

        /// <summary>
        /// PayPal email account
        /// </summary>
        String EmailAccount { get; }
    }
}
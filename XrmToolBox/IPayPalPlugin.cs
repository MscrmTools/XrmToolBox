using System;

namespace XrmToolBox
{
    public interface IPayPalPlugin
    {
        String EmailAccount { get; }
        
        String DonationDescription { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MsCrmTools.FetchXmlBuilder.UserControls
{
    interface IFetchXmlControl
    {
        XmlNode GetNode();
    }
}

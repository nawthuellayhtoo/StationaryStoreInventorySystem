using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RetrievalDetailBO
/// </summary>
public class RetrievalDetailBO
{
    RetrivalBO rbo;
    List<BreakDownByDepBO> bdboLst;

    public RetrivalBO Rbo
    {
        get { return rbo; }
        set { rbo = value; }
    }

    public List<BreakDownByDepBO> BdboLst
    {
        get { return bdboLst; }
        set { bdboLst = value; }
    }

}

using System;

namespace Facebook.Archive.Model.Page.Architecture.Base
{
    public interface IElement
    {
        string Parser { get; set; }

        int ParserVersion { get; set; }

        int GetQuality();
    }
}

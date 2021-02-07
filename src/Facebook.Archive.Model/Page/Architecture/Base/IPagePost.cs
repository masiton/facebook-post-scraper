using System;

namespace Facebook.Archive.Model.Page.Architecture.Base
{
    public interface IPagePost : IElement
    {
        string Url { get; }

        string Html { get; }

        DateTime? Timestamp { get; }

        string TimestampRaw { get; set; }
    }
}
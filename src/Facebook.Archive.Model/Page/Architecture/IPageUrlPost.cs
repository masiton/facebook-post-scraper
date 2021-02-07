using Facebook.Archive.Model.Page.Architecture.Base;

namespace Facebook.Archive.Model.Page.Architecture
{
    public interface IPageUrlPost : IPagePost
    {
        string LinkUrl { get; }

        string LinkUrlRaw { get; }

        string LinkText { get; }
    }
}
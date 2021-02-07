using Facebook.Archive.Model.Page.Architecture.Base;

namespace Facebook.Archive.Model.Page.Architecture
{
    public interface IPagePhotoPost : IPagePost
    {
        string ImageUrl { get; }

        byte[] ImageData { get; }
    }
}
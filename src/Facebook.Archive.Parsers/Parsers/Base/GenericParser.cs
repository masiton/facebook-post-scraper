using HtmlAgilityPack;
using System;
using System.Globalization;
using System.Net.Http;

namespace Facebook.Archive.Parsers.Parsers.Base
{
    public abstract class GenericParser
    {
        protected byte[] GetImageBytes(string url)
        {
            using(var httpClient = new HttpClient())
            {
                var task = httpClient.GetByteArrayAsync(url);
                task.Wait();

                if(task.Status == System.Threading.Tasks.TaskStatus.RanToCompletion)
                {
                    return task.Result;
                }

                throw new InvalidOperationException();
            }
        }

        protected HtmlNode GetNameHtmlNode(HtmlNode postNode)
        {
            return postNode.SelectSingleNode(".//h5");
        }

        protected HtmlNode GetTimestampNodeFromNameNode(HtmlNode nameNode)
        {
            return nameNode.ParentNode.SelectSingleNode(".//div").SelectSingleNode(".//a");
        }

        protected string GetPostUrlFromTimestampNode(HtmlNode timestampNode)
        {
            var value = timestampNode.Attributes["href"].Value.Substring(0, timestampNode.Attributes["href"].Value.IndexOf('?'));
            return $"https://www.facebook.com{value}";
        }

        protected string GetTimestampStringFromTimestampNode(HtmlNode timestampNode)
        {
            return timestampNode.SelectSingleNode(".//span[contains(@class, 'timestampContent')]").ParentNode.Attributes["title"].Value;
        }

        protected DateTime GetDateFromPostString(string value)
        {
            var values = value.Trim().Split(new[] { " at" }, StringSplitOptions.RemoveEmptyEntries);
            var formatted = string.Join(null, values);
            var date = DateTime.ParseExact(formatted, "dddd, MMMM d, yyyy h:mm tt", CultureInfo.InvariantCulture);
            return date;
        }

        protected string EscapeUrlSequence(string url)
        {
            return url.Replace("&amp;", "&").Replace("%3A", ":").Replace("%2F", "/");
        }

        protected string GetLinkUrlFromFacebookLink(string facebookUrl)
        {
            var value = facebookUrl.Substring("https://l.facebook.com/l.php?u=".Length);
            value = System.Net.WebUtility.UrlDecode(value);

            return value;
        }
    }
}

using Archivator.Facebook.Integration;
using System;
using System.IO;
using System.Text;

namespace Archivator.Facebook.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var examples = File.ReadAllLines("Examples.txt");

            foreach(var example in examples)
            {
                Console.WriteLine("".PadLeft(20, '*'));
                Console.WriteLine(example);
                Console.WriteLine("".PadLeft(20, '*'));

                var task = DataParser.GetPostsFromUri(example);
                task.Wait();

                if(task.Exception != null)
                {
                    Console.WriteLine(" -> Error.");
                }
                else
                {
                    foreach(var post in task.Result)
                    {
                        Console.WriteLine($" -> {post.CreatedUtc:u} - {post.Text}");
                    }
                }
            }
        }
    }
}

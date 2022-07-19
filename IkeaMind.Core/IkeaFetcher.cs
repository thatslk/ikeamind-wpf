using IkeaMind.Infrastructure;
using System.Windows.Media.Imaging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace IkeaMind.Core
{
    public static class IkeaFetcher
    {
        // public static List<Task> running = new List<Task>();
        public static EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.AutoReset);
        public static Queue<IkeaProduct> queue = new Queue<IkeaProduct>();
        public static ConcurrentDictionary<string, BitmapImage> imagesBytesByArticles
            = new ConcurrentDictionary<string, BitmapImage>();

        public static Random random = new Random();
        public static IkeaArticlesContext db = new IkeaArticlesContext();

        public static Action<EventWaitHandle> InternetFailed;

        public static void AppendProductsInQueue(int count)
        {

            for (int i = 0; i < count; i++)
            {
                var randomPosition = random.Next(db.IkeaProducts.Count());
                var randomProduct = db.IkeaProducts.Skip(randomPosition).First();
                queue.Enqueue(randomProduct);

                var fetchImageTask = Task.Run(() => LoadProductImage(randomProduct.ImageUrl, randomProduct.Article));
                // running.Add(fetchImageTask);
            }
        }

        public static void LoadProductImage(string url, string articleNumber)
        {
            var webClient = new WebClient();

            byte[] imageBytes = new byte[1];
            try
            {
                imageBytes = webClient.DownloadData(url + "?f=xxxs");
            }
            catch (WebException ex)
            {
                InternetFailed(ewh);
                ewh.WaitOne();
                LoadProductImage(url, articleNumber);
                return;
            }

            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageBytes))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();

            imagesBytesByArticles.TryAdd(articleNumber, image);
        }

        public static IkeaProduct GetNextProduct()
        {
            var product = queue.Dequeue();
            if (imagesBytesByArticles.TryRemove(product.Article, out BitmapImage image))
                product.Image = image;

            return product;
        }

    }
}

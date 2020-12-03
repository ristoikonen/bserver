using System;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using System.IO;


namespace bserver.Data
{
    public class WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //https://stackoverflow.com/questions/186062/can-an-asp-net-mvc-controller-return-an-image

        public byte[] GetImage()
        {
            //  string fn = @"C:\Users\risto.ikonen\Pictures\Screenshots\s.png";
            

            var fn = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, @"s.png");


            BuGeRedLister bl = new BuGeRedLister();
            

            System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromFile(fn);
            //return bl.GetBytesFromBitmap(bmp);

            using (var stream = new MemoryStream())
            {
                bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }

            //return (FileResult)((Image) bmp);

        }



        public Image GetImage2()
        {

            var fn = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, @"s.png");


            BuGeRedLister bl = new BuGeRedLister();


            System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromFile(fn);
            return bmp;


        }

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            //
            // TODO: Check RemoteBrowserFileStreamOptions

            
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }
    }
}

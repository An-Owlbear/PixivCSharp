using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PixivCSharp.Tests
{
    public partial class Tests
    {
        // Test viewing illust series context
        private static async Task ViewIllustSeriesContext()
        {
            IllustSeriesContext result;
            Console.Write("Please enter the illust to view the context of\n> ");
            
            // Error handling
            try
            {
                result = await Client.IllustSeriesContextAsync(Console.ReadLine());
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            
            Console.WriteLine("Illust series context:");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Output.TestIllustSeries(result.SeriesDetail);
            Console.WriteLine("Illust context:");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("Content order: {0}", result.SeriesContext.ContentOrder);
            if (result.SeriesContext.Prev != null) Output.TestIllust(result.SeriesContext.Prev);
            if (result.SeriesContext.Next != null) Output.TestIllust(result.SeriesContext.Next);
        }

        // Tests viewing information of an illust series
        private static async Task ViewIllustSeriesInfo()
        {
            IllustSeriesInfo result;
            Console.Write("Please enter the ID of the illust series\n> ");
            
            // Error handling
            try
            {
                result = await Client.IllustSeriesInfoAsync(Console.ReadLine());
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            
            Console.WriteLine("Illust series info:");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Output.TestIllustSeries(result.SeriesDetail);
            Console.WriteLine("First illust:");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Output.TestIllust(result.FirstIllust);
            Console.WriteLine("Illusts:");
            Console.WriteLine("-------------------------------------------------------------------------------");

            foreach (Illust illust in result.Illusts)
            {
                Output.TestIllust(illust);
            }
            
            Console.WriteLine("Next URL: {0}", result.NextUrl);
        }
        
        // Tests viewing information of a novel series
        private static async Task ViewNovelSeriesInfo()
        {
            NovelSeriesInfo result;
            Console.Write("Please enter the ID of the novel series\n> ");
            
            // Error handling
            try
            {
                result = await Client.NovelSeriesInfoAsync(Console.ReadLine());
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            
            Console.WriteLine("Novel series info:");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Output.TestNovelSeries(result.SeriesDetail);
            Console.WriteLine("First novel:");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Output.TestNovel(result.FirstNovel);
            Console.WriteLine("Novels:");
            Console.WriteLine("-------------------------------------------------------------------------------");

            foreach (Novel novel in result.Novels)
            {
                Output.TestNovel(novel);
            }
            
            Console.WriteLine("Next URL: {0}", result.NextUrl);
        }
        
        // Tests viewing all works in a series
        private static async Task ViewAllSeriesIllustsTest()
        {
            IllustSeriesInfo result;
            Console.Write("Please enter the ID of the series to view\n> ");
            string ID = Console.ReadLine();
            bool allFound = false;
            List<Illust> works = new List<Illust>();

            try
            {
                result = await Client.IllustSeriesInfoAsync(ID, filter:FilterType.None);
                if (result.NextUrl == null) allFound = true;
                works.AddRange(result.Illusts);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            
            while (!allFound)
            {
                try
                {
                    result = await Client.RequestAsync<IllustSeriesInfo>(result.NextUrl);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
                
                works.AddRange(result.Illusts);

                if (result.NextUrl == null)
                {
                    allFound = true;
                }
            }

            works.Reverse();

            foreach (Illust illust in works)
            {
                Console.WriteLine(illust.Title);
            }
        }
    }
}
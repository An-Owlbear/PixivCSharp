using System;
using System.Threading.Tasks;

namespace PixivCSharp.Tests
{
    public partial class Tests
    {
        // Tests viewing novel info
        static async Task ViewNovel()
        {
            Console.Write("Please enter the id of the novel to view\n> ");
            Novel novel = await Client.ViewNovel(Console.ReadLine());
            
            Console.WriteLine("Novel:");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("Novel ID: {0}", novel.id);
            Console.WriteLine("Novel title: {0}", novel.title);
            Console.WriteLine("Novel caption: {0}", novel.caption);
            Console.WriteLine("Novel restrict: {0}", novel.restrict);
            Console.WriteLine("Novel x-restrict: {0}", novel.x_restrict);
            Console.WriteLine("Is original: {0}", novel.is_original);
            Console.WriteLine("Novel medium image url: {0}", novel.image_urls.medium);
            Console.WriteLine("Novel square medium url: {0}", novel.image_urls.square_medium);
            Console.WriteLine("Novel large image url: {0}", novel.image_urls.large);
            Console.WriteLine("Novel create date: {0}", novel.create_date);
            Console.WriteLine("Tags:");
            Console.WriteLine("-------------------------------------------------------------------------------");
            foreach (Tag tag in novel.tags)
            {
                Console.WriteLine("Tag name: {0}", tag.name);
                Console.WriteLine("Translated name: {0}", tag.translated_name);
                Console.WriteLine("Added by uploader: {0}", tag.added_by_uploader);
                Console.WriteLine("-------------------------------------------------------------------------------");
            }
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("Page count: {0}", novel.page_count);
            Console.WriteLine("Text count: {0}", novel.text_length);
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("User:");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("User id: {0}", novel.user.id.ToString());
            Console.WriteLine("User name: {0}", novel.user.name);
            Console.WriteLine("User account: {0}", novel.user.account);
            Console.WriteLine("User profile picture url: {0}", novel.user.profile_image_urls.medium);
            Console.WriteLine("Is user followed: {0}", novel.user.is_followed);
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("Series:");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("Series ID: {0}", novel.series.id);
            Console.WriteLine("Series Title: {0}", novel.series.title);
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("Is bookmarked: {0}", novel.is_bookmarked);
            Console.WriteLine("Total bookmarks: {0}", novel.total_bookmarks);
            Console.WriteLine("Total views: {0}", novel.total_view);
            Console.WriteLine("Visible: {0}", novel.visible);
            Console.WriteLine("Total comments: {0}", novel.total_comments);
            Console.WriteLine("Is muted: {0}", novel.is_muted);
            Console.WriteLine("Is my pixiv only: {0}", novel.is_mypixiv_only);
            Console.WriteLine("Is x restricted: {0}", novel.is_x_restricted);
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("\n\n\n");
        }
        
        // Tests novel bookmarks
        static async Task BookmarkNovel()
        {
            Console.Write("Enter 1 to add a bookmark, enter 0 to remove a bookmark\n> ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Enter the id of the novel to bookmark\n> ");
                await Client.AddBookmarkNovel(Console.ReadLine(), "public");
            }
            else if (choice == "2")
            {
                Console.Write("Enter the id of novel to remove from bookmarks");
                await Client.RemoveBookmarkNovel(Console.ReadLine());
            }
        }
    }
    
    
}
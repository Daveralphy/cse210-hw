using System;
using System.Collections.Generic;
using YouTubeVideos; // Added using directive for the namespace

public class Program
{
    public static void Main(string[] args)
    {
        Video video1 = new Video("How to Cook Authentic Jollof Rice", "Chef Tola", 950);
        video1.AddComment(new Comment("Ngozi", "I love this recipe!"));
        video1.AddComment(new Comment("Chinedu", "The best guide for smokey jollof."));
        video1.AddComment(new Comment("Aisha", "So easy to follow. Thank you!"));

        Video video2 = new Video("Lagos Tech Scene: Innovations and Future", "Startup Hub NG", 1520);
        video2.AddComment(new Comment("Femi", "Very insightful overview of Yaba."));
        video2.AddComment(new Comment("Hadiza", "Nigeria is definitely rising in tech."));
        video2.AddComment(new Comment("Obinna", "I'm starting a company now thanks to this motivation."));
        video2.AddComment(new Comment("Kemi", "Great analysis of the fintech space."));

        Video video3 = new Video("Understanding Polymorphism", "OOPGuru", 420);
        video3.AddComment(new Comment("Henry", "Short and to the point. Perfect."));
        video3.AddComment(new Comment("Ivy", "I wish I found this video earlier."));
        video3.AddComment(new Comment("Jack", "Thanks for the real-world examples."));

        List<Video> videos = new List<Video> { video1, video2, video3 };

        foreach (Video video in videos)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Title: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length: {video.GetLength()} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
            Console.WriteLine("Comments:");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.GetCommenterName()}: \"{comment.GetCommentText()}\"");
            }
            Console.WriteLine("------------------------------------------");
        }
    }
}
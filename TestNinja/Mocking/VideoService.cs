using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TestNinja.Mocking
{
    public sealed class VideoService
    {
        public string ReadVideoTitle()
        {
            var str = new FileReader().Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);

            return video == null ? "Error parsing the video." : video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();
            
            using (var context = new VideoContext())
            {
                var videos = 
                    (from video in context.Videos
                    where !video.IsProcessed
                    select video).ToList();

                videoIds.AddRange(videos.Select(v => v.Id));

                return string.Join(",", videoIds);
            }
        }
    }

    public sealed class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public sealed class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}
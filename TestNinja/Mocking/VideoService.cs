using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TestNinja.Mocking
{
    public sealed class VideoService
    {
        private readonly IFileReader _fileReader;
        private readonly IVideoRepository _videoRepository;
        
        // ToDo: use Autofac
        public VideoService(IFileReader fileReader = null, IVideoRepository videoRepository = null)
        {
            CreateVideoFile();
            _fileReader = fileReader ?? new FileReader();
            _videoRepository = videoRepository ?? new VideoRepository();
        }

        public void CreateVideoFile()
        {
            if (File.Exists("video.txt"))
                return;

            using (StreamWriter sw = File.CreateText("video.txt"))
            {
                sw.WriteLine("Hello");
                sw.WriteLine("And");
                sw.WriteLine("Welcome");
            }
        }

        public string ReadVideoTitle()
        {
            var str = _fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);

            return video == null ? "Error parsing the video." : video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();
            var videos = _videoRepository.GetUnprocessedVideos();
            videoIds.AddRange(videos.Select(video => video.Id));

            return string.Join(",", videoIds);
            
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
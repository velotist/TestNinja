using System.Collections.Generic;
using TestNinja.Mocking.VideoService;

namespace TestNinja.Mocking.Interfaces
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetUnprocessedVideos();
    }
}
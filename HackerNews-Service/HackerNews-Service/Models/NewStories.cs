namespace HackerNews_Service.Models
{
    public class NewStories
    {
        public List<int> Stories { get; set; }

        public NewStories()
        {
            Stories = new List<int>();
        }
    }
}

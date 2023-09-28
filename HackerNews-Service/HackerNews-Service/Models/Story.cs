namespace HackerNews_Service.Models
{
    public class Story
    {
        private string? by;
        private int descendants;
        private int id;
        private List<int>? kids;
        private int score;
        private int time;
        private string? title;
        private string? type;
        private string? url;

        public Story()
        {
            Kids = new List<int>();
        }

        public string? By { get => by; private set => by = value; }
        public int Descendants { get => descendants; private set => descendants = value; }
        public int Id { get => id; private set => id = value; }
        public List<int>? Kids { get => kids; private set => kids = value; }
        public int Score { get => score; private set => score = value; }
        public int Time { get => time; private set => time = value; }
        public string? Title { get => title; private set => title = value; }
        public string? Type { get => type; private set => type = value; }
        public string? Url { get => url; private set => url = value; }
    }
}

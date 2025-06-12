namespace RaftLabs.ReqResApiClient.Models
{
    public class SingleApiResponse<T>
    {
        public int Page { get; set; }
        public int Per_Page { get; set; }
        public int Total { get; set; }
        public int Total_Pages { get; set; }
        public User Data { get; set; } = new();
    }
    public class ApiResponse<T>
    {
        public int Page { get; set; }
        public int Per_Page { get; set; }
        public int Total { get; set; }
        public int Total_Pages { get; set; }
        public List<User> Data { get; set; } = new();
    }

}

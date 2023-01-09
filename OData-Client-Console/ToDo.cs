using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ToDos_API.Models
{
    public enum ToDoPriority
    {
        Could,
        Should,
        Must,
    }
    public class ToDo
    {
        public ToDo(int id, string? title, string? descritprion, ToDoPriority priority, bool isDone)
        {
            Id = id;
            Title = title;
            Description = descritprion;
            Priority = priority;
            IsDone = isDone;
        }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Title")]
        public string? Title { get; set; }

        [JsonProperty("Description")]
        public string? Description { get; set; }

        [JsonProperty("Priority")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ToDoPriority Priority { get; set; }

        [JsonProperty("IsDone")]
        public bool IsDone { get; set; }

        public override string? ToString()
        {
            return $"Id: {Id} | Title: {Title} | Description: {Description} | Priority: {Priority} | IsDone: {IsDone}";
        }
    }
}

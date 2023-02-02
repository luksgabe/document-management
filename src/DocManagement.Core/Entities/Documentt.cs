using DocManagement.Core.Enums;

namespace DocManagement.Core.Entities
{
    public class Documentt : BaseEntity
    {
        public long DocumentId { get { return Id; } set { Id = value; } }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Status Status { get; private set; }
        public string Url { get; private set; }
        public override DateTime Created_At { get; set; }
        public override DateTime? Updated_At { get; set; }

        public Documentt(string name, string description, Status status, string url)
        {
            Name = name;
            Description = description;
            Status = status;
            Url = url;
            Created_At = DateTime.UtcNow.ToLocalTime();
        }

        public Documentt(long id, string name, string description, Status status, string url)
        {
            DocumentId = id;
            Name = name;
            Description = description;
            Status = status;
            Url = url;
            Created_At = DateTime.UtcNow.ToLocalTime();
        }


        public void Update(string name, string description, Status status, string url)
        {
            Name = name;
            Description = description;
            Status = status;
            Url = url;
            Updated_At = DateTime.UtcNow.ToLocalTime();
        }

    }
}

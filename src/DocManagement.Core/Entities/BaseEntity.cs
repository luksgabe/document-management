namespace DocManagement.Core.Entities
{
    public abstract class BaseEntity
    {
        public virtual long Id { get; set; }
        public abstract DateTime Created_At { get; set; }
        public abstract DateTime? Updated_At { get; set; }
    }
}

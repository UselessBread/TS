namespace TS.Data
{
    public class VersionedEntityBase
    {
        public Guid ImmutableId { get; set; }
        public int Version { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DeletionDate { get; set; }
    }
}
namespace Aryap.Shared.Abstractions
{
    // Base class for all entities in the application
    public abstract class BaseEntity
    {
        public int Id { get; set; } // Unique identifier for the entity
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp when the entity was created
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Timestamp when the entity was last updated
    }
}
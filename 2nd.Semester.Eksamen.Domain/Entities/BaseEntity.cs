namespace _2nd.Semester.Eksamen.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; protected set; }
        public Guid RefrenceId { get; protected set; }
        public BaseEntity() 
        {
            RefrenceId = Guid.NewGuid(); 
        }
    }
}


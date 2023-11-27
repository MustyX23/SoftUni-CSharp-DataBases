namespace Eventmi.Infrastructure.Data.Contracts
{
    public interface IDeletable
    {
        bool IsActive { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}

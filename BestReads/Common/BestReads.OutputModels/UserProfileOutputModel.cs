namespace BestReads.OutputModels
{
    public class UserProfileOutputModel : BaseDeletableOutputModel
    {
        public string? FirstName { get; set; } = null;

        public string? LastName { get; set; } = null;

        public string? CountryFrom { get; set; } = null;

        public DateTime? DateBorn { get; set; } = null;
    }
}

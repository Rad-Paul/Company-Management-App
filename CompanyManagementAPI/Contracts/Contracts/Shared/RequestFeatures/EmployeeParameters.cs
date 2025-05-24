namespace Contracts.Shared.RequestFeatures
{
    public class EmployeeParameters : RequestParameters
    {
        public EmployeeParameters()
        {
            if (OrderBy is null)
                OrderBy = "name";
        }

        public uint MinAge { get; set; }
        public uint MaxAge { get; set; } = uint.MaxValue;
        public string? SearchTerm { get; set; }
        public bool ValidAgeRange => MaxAge > MinAge;
    }
}

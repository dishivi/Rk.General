#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace General.Models.Database
{
    public class DatabaseProviderConfiguration
    {
        public string Name { get; set; }

        public string TenantConnectionString { get; set; }
 
        public string MasterConnectionString { get; set; }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using System.Data.Entity;

namespace Elmah.EF6
{
    public class ElmahDatabaseInitializer : CreateDatabaseIfNotExists<ElmahContext>
    {
    }
}
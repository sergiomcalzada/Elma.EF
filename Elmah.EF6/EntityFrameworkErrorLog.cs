using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using Elmah.EF;

namespace Elmah.EF6
{
    public class EntityFrameworkErrorLog : BaseEntityFrameworkErrorLog
    {
        public EntityFrameworkErrorLog(IDictionary config) : base(config)
        {
        }

        protected override void Insert(ElmahError elmahError)
        {
            using (var dbContext = new ElmahContext())
            {
                try
                {
                    dbContext.ElmahErrors.Add(elmahError);
                    dbContext.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        protected override ElmahError FindOne(Guid errorGuid)
        {
            using (var dbContext = new ElmahContext())
            {
                return dbContext.ElmahErrors.FirstOrDefault(x => x.ErrorId == errorGuid);
            }
        }

        protected override IEnumerable<ElmahError> GetPaged(int pageIndex, int pageSize, out int total)
        {
            using (var dbContext = new ElmahContext())
            {
                total = dbContext.ElmahErrors.Count();

                var skip = pageIndex * pageSize;
                var elmahErrors = dbContext.ElmahErrors
                                            .OrderBy(x => x.Sequence)
                                            .Skip(() => skip)
                                            .Take(() => pageSize)
                                            .ToArray();
                return elmahErrors;
            }
        }

    }

    public class DbEntityValidationExceptionLogger
    {
        public static void Log(DbEntityValidationException ex)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Db Entity Validation Exception");
            foreach (var item in ex.EntityValidationErrors)
            {
                sb.AppendFormat("Entity: {0} {1}", item.Entry.Entity, Environment.NewLine);
                foreach (var error in item.ValidationErrors)
                {
                    sb.AppendFormat("{0}: {1} {2}", error.PropertyName, error.ErrorMessage, Environment.NewLine);
                }
            }

            var exception = new Exception(sb.ToString(), ex);
            ErrorSignal.FromCurrentContext().Raise(exception);
        }
    }
}
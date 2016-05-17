using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Elmah.EF
{
    public abstract class BaseEntityFrameworkErrorLog : ErrorLog
    {
        protected BaseEntityFrameworkErrorLog(IDictionary config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            this.ConnectionString = ElmahHelper.GetConnectionString(config);
            this.ApplicationName = ElmahHelper.GetAppName(config);
        }

        public string ConnectionString { get; private set; }

        public override string Name => "Entity Framework Error Log";

        public override string Log(Error error)
        {
            if (error == null)
                throw new ArgumentNullException(nameof(error));

            var elmahError = new ElmahError(error);


            this.Insert(elmahError);

            return elmahError.ErrorId.ToString();
        }

        public override ErrorLogEntry GetError(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (id.Length == 0) throw new ArgumentException(null, nameof(id));

            Guid errorGuid;

            try
            {
                errorGuid = new Guid(id);
            }
            catch (FormatException e)
            {
                throw new ArgumentException(e.Message, nameof(id), e);
            }

            var elmahError = this.FindOne(errorGuid);

            if (elmahError == null)
                return null;

            var error = ErrorXml.DecodeString(elmahError.AllXml);
            return new ErrorLogEntry(this, id, error);
        }
        
        public override int GetErrors(int pageIndex, int pageSize, IList errorEntryList)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex), pageIndex, null);
            if (pageSize < 0) throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, null);

            int total;
            var errors = this.GetPaged(pageIndex, pageSize, out total);

            foreach (var elmahError in errors)
            {
                var error = ErrorXml.DecodeString(elmahError.AllXml);
                errorEntryList.Add(new ErrorLogEntry(this, elmahError.ErrorId.ToString(), error));
            }

            return total;
        }

        protected abstract void Insert(ElmahError elmahError);
        protected abstract ElmahError FindOne(Guid errorGuid);
        protected abstract IEnumerable<ElmahError> GetPaged(int pageIndex, int pageSize, out int total);
    }
}
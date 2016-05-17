using System;
using System.ComponentModel.DataAnnotations;

namespace Elmah.EF
{
    public class ElmahError
    {
        public ElmahError()
        {
        }

        public ElmahError(Exception ex)
            : this(new Error(ex))
        {
        }

        public ElmahError(Error error)
        {
            this.ErrorId = Guid.NewGuid();
            this.Application = error.ApplicationName;
            this.Host = error.HostName;
            this.Type = error.Type;
            this.Source = error.Source;
            this.Message = error.Message;
            this.User = error.User;
            this.TimeUtc = error.Time;

            this.AllXml = ErrorXml.EncodeString(error);
        }

        public Guid ErrorId { get; set; }

        [Required]
        [StringLength(ElmahHelper.MaxAppLength)]
        public string Application { get; set; }

        [Required]
        [StringLength(ElmahHelper.MaxHostName)]
        public string Host { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }

        [Required]
        [StringLength(60)]
        public string Source { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        [StringLength(50)]
        public string User { get; set; }

        [Required]
        public int StatusCode { get; set; }

        [Required]
        public DateTime TimeUtc { get; set; }

        public int Sequence { get; set; }

        [Required]
        public string AllXml { get; set; }
    }
}
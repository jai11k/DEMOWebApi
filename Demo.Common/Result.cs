using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common
{
    public class Result<T> where T : class
    {
        public ResponseStatus Status { get; set; }
        public T Output { get; set; }
        public Guid EntityId { get; set; }
        public List<Error> Errors { get; set; }
    }

    public class MultipleRecordsAndCount<T> where T : class 
    {
        public T Records { get; set; }
        public int Count { get; set; }
    }

    public class ResultMultipleRecords<T> where T : class
    {
        public ResponseStatus Status { get; set; }
        public T Output { get; set; }
        public int TotalCount { get; set; }
        public List<Error> Errors { get; set; } = new List<Error>();
    }
    public class Error
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}

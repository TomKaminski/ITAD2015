using System.Collections.Generic;

namespace Itad2015.Contract.Common
{
    public class SingleServiceResult<T>
    {
        public SingleServiceResult(T result)
        {
            Result = result;
        }

        public SingleServiceResult(T result, List<string> errors)
        {
            Result = result;
            ValidationErrors = errors;
        }
        public T Result { get; set; }
        public List<string> ValidationErrors { get; set; }
    }

    public class SingleServiceResult<T1,T2>
    {
        public SingleServiceResult(T1 firstResult, T2 secondResult)
        {
            FirstResult = firstResult;
            SecondResult = secondResult;
        }

        public SingleServiceResult(T1 firstResult, T2 secondResult, List<string> errors)
        {
            FirstResult = firstResult;
            SecondResult = secondResult;
            ValidationErrors = errors;
        }
        public T1 FirstResult { get; set; }
        public T2 SecondResult { get; set; }
        public List<string> ValidationErrors { get; set; }
    }

    public class MultipleServiceResult<T>
    {
        public MultipleServiceResult(IEnumerable<T> result)
        {
            Result = result;
        }
        public MultipleServiceResult(IEnumerable<T> result, List<string> errors)
        {
            Result = result;
            ValidationErrors = errors;
        }
        public IEnumerable<T> Result { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}

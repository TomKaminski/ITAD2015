using System.Collections.Generic;

namespace Itad2015.ViewModels.Base
{
    public class BaseReturnViewModel<T>
    {
        public T Result { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
    }
}
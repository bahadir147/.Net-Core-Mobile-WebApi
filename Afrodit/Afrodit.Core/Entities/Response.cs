using Afrodit.Core.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Afrodit.Core.Entities
{
    public class Response<T>
    {
        public int StatusCode { get; set; }
        public bool Error { get; set; }
        public string ErrorDescription { get; set; }
        public PagingHeader PagingHeader { get; set; }
        public T ResponseModel { get; set; }
      
    }
}

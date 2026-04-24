using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Business.Exceptions
{
    public class UnauthorizedException : BusinessException { 
        public UnauthorizedException(string m) : base(m) { } 
    }
}

using ccct.Core;
using ccct.Core.Helpers;

namespace ccct.Program;

class Program
{
    static void Main(string[] args)
    {
        ApplicationHelper.HandleSuccess(CT.Compress(args));
    }
}

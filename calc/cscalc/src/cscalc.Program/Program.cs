using cscalc.Core;
using cscalc.Core.Helpers;
using cscalc.Core.State;
using cscalc.Core.Utilities;

namespace cscalc.Program;

static class Program
{
    static void Main(string[] arguments)
    {
        Result<Arguments> argumentResult = ArgumentsHelper.Validate(arguments);
        if (!argumentResult.IsSuccess) ExecutionHelper.ExitError(argumentResult.ErrorMessage);

        Result<string> evaluatedResult = Calculator.Evaluate(argumentResult.Data);
        if (!evaluatedResult.IsSuccess) ExecutionHelper.ExitError(evaluatedResult.ErrorMessage);

        Calculator.Output(evaluatedResult.Data);
    }
}

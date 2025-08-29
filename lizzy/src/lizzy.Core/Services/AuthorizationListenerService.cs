using lizzy.Core.State;

namespace lizzy.Core.Services;

public class AuthorizationListenerService()
{
    private TaskCompletionSource<AccessTokenDTO> _taskCompletionSource = new();

    public Task<AccessTokenDTO> WaitForAuthorize()
    {
        return _taskCompletionSource.Task;
    }

    public void ResetAuthorizeWait()
    {
        _taskCompletionSource = new();
    }

    public Task<AccessTokenDTO> SetAuthorizeCompleted(AccessTokenDTO accessTokenDTO)
    {
        _taskCompletionSource.TrySetResult(accessTokenDTO);

        return _taskCompletionSource.Task;
    }
}
using Docker.DotNet;
using Docker.DotNet.Models;

namespace Hosting.Api.Services;

public class DockerService
{
    private readonly IDockerClient _dockerClient;

    public DockerService(IDockerClient dockerClient)
    {
        _dockerClient = dockerClient;
    }

    public Task<VersionResponse> ObtenerVersionAsync()
    {
        return _dockerClient.System.GetVersionAsync();
    }

    public async Task<IList<ContainerListResponse>> ObtenerContenedoresAsync()
    {
        return await _dockerClient.Containers.ListContainersAsync(
            new ContainersListParameters
            {
                All = true
            }
        );
    }
}
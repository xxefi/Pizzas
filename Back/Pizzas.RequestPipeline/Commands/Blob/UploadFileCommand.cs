using MediatR;
using Microsoft.AspNetCore.Http;

namespace Pizzas.RequestPipeline.Commands.Blob;

public record UploadFileCommand(IFormFile File): IRequest<string>;
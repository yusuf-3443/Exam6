using Domain.DTOs.MaterialDTO;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Services.MaterialService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("material")]
[ApiController]
public class MaterialController : ControllerBase
{
    private readonly IMaterialService materialService;

    public MaterialController(IMaterialService materialService)
    {
        this.materialService = materialService;
    }

    [HttpGet("get-materials-of-one-course")]
    public async Task<Response<List<GetMaterialsOfOneCourseDto>>> GetMatrialsofonecourse()
    {
        return await materialService.GetMaterialsOfOneCourse();
    }

    [HttpGet("get-materials")]
    public async Task<Response<List<GetMaterialDto>>> GetMaterialsAsync([FromQuery] MaterialFilter filter)
    {
        return await materialService.GetMaterials(filter);
    }

    [HttpGet("{materialId:int}")]
    public async Task<Response<GetMaterialDto>> GetMaterialByIdAsync(int materialId)
    {
        return await materialService.GetMaterialById(materialId);
    }

    [HttpPost("add-material")]
    public async Task<Response<string>> AddMaterialAsync(AddMaterialDto materialDto)
    {
        return await materialService.AddMaterial(materialDto);
    }

    [HttpPut("update-material")]
    public async Task<Response<string>> UpdateMaterialAsync(UpdateMaterialDto materialDto)
    {
        return await materialService.UpdateMaterial(materialDto);
    }

    [HttpDelete("{materialId:int}")]
    public async Task<Response<bool>> DeleteMaterialAsync(int materialId)
    {
        return await materialService.DeleteMaterial(materialId);
    }
}

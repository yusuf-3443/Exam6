using Domain.DTOs.MaterialDTO;
using Domain.Filter;
using Domain.Responses;

namespace Infrastructure.Services.MaterialService;

public interface IMaterialService
{
    Task<Response<List<GetMaterialsOfOneCourseDto>>> GetMaterialsOfOneCourse();
    Task<PagedResponse<List<GetMaterialDto>>> GetMaterials(MaterialFilter filter);
    Task<Response<GetMaterialDto>> GetMaterialById(int id);
    Task<Response<string>> AddMaterial(AddMaterialDto material);
    Task<Response<string>> UpdateMaterial(UpdateMaterialDto material);
    Task<Response<bool>> DeleteMaterial(int id);
}


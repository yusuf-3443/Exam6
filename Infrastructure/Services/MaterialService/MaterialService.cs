using System.Net;
using AutoMapper;
using Domain.DTOs.MaterialDTO;
using Domain.Entities;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.MaterialService;
public class MaterialService(DataContext context, IMapper mapper) : IMaterialService
{

    public async Task<Response<List<GetMaterialsOfOneCourseDto>>> GetMaterialsOfOneCourse()
    {
        try
        {
        string courseTitle = "Java";
        var query =await (from m in context.Materials
        join c in context.Courses on m.CourseId equals c.Id
        where c.Title == courseTitle 
        select new GetMaterialsOfOneCourseDto
        {
            MaterialTitle = m.Title
        }).ToListAsync();
          var list = new List<GetMaterialsOfOneCourseDto>();
        foreach (var q in query)
        {
            var course = new GetMaterialsOfOneCourseDto()
            {
                MaterialTitle = q.MaterialTitle
            };
            list.Add(course);
        }
        return new Response<List<GetMaterialsOfOneCourseDto>>(list);
    
            
        }
        catch (Exception e)
        {
                return new Response<List<GetMaterialsOfOneCourseDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetMaterialDto>>> GetMaterials(MaterialFilter filter)
    {
        try
        {
            var materials = context.Materials.AsQueryable();

            if(!string.IsNullOrEmpty(filter.Title))
                materials = materials.Where(x => x.Title.ToLower().Contains(filter.Title.ToLower()));

            var response = await materials
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();

            var totalRecord = materials.Count();
            var mapped = mapper.Map<List<GetMaterialDto>>(response);

            return new PagedResponse<List<GetMaterialDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetMaterialDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetMaterialDto>> GetMaterialById(int id)
    {
        try
        {
            var material = await context.Materials.FirstOrDefaultAsync(x => x.Id == id);
            if (material == null)
                return new Response<GetMaterialDto>(HttpStatusCode.BadRequest, "Not found");

            var mapped = mapper.Map<GetMaterialDto>(material);
            return new Response<GetMaterialDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetMaterialDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> AddMaterial(AddMaterialDto material)
    {
        try
        {
            var mapped = mapper.Map<Material>(material);
            await context.Materials.AddAsync(mapped);

            var save = await context.SaveChangesAsync();
            if (save > 0)
                return new Response<string>("Successfully");

            return new Response<string>(HttpStatusCode.BadRequest, "Failed");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateMaterial(UpdateMaterialDto material)
    {
        try
        {
            var mapped = mapper.Map<Material>(material);
            context.Materials.Update(mapped);

            var save = await context.SaveChangesAsync();
            if (save > 0)
                return new Response<string>("Successfully");

            return new Response<string>(HttpStatusCode.BadRequest, "Failed");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }    
    }

    public async Task<Response<bool>> DeleteMaterial(int id)
    {
        try
        {
            var material = await context.Materials.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (material > 0)
                return new Response<bool>(true);

            return new Response<bool>(HttpStatusCode.BadRequest, "Not found");
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }


}
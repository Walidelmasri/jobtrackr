using Jobtrackr.Application.DTOs;
using Jobtrackr.Application.Interfaces;
using Jobtrackr.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jobtrackr.Application.Services;

public class JobApplicationService : IJobApplicationService
{
    private readonly IApplicationDbContext _context;

    public JobApplicationService(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResponse<JobApplicationListItemResponse>>
    GetAllAsync(
        JobApplicationQueryParameters query,
        CancellationToken ct = default)
    {
        var page = query.Page < 1
            ? 1
            : query.Page;

        var pageSize = query.PageSize < 1
            ? 10
            : query.PageSize;

        pageSize = pageSize > 50
            ? 50
            : pageSize;

        var jobsQuery = _context.JobApplications
            .Include(x => x.Company)
            .AsQueryable();

        if (query.Status.HasValue)
        {
            jobsQuery = jobsQuery.Where(
                x => x.Status == query.Status.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.CompanyName))
        {
            var companyName = query.CompanyName.Trim().ToLower();

            jobsQuery = jobsQuery.Where(
                x => x.Company.Name.ToLower().Contains(companyName));
        }

        var totalCount = await jobsQuery.CountAsync(ct);

        var items = await jobsQuery
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new JobApplicationListItemResponse
            {
                Id = x.Id,
                CompanyName = x.Company.Name,
                RoleTitle = x.RoleTitle,
                Status = x.Status,
                WorkMode = x.WorkMode,
                EmploymentType = x.EmploymentType,
                SponsorsVisa = x.Company.SponsorsVisa,
                CreatedAt = x.CreatedAt,
                AppliedAt = x.AppliedAt
            })
            .ToListAsync(ct);

        return new PagedResponse<JobApplicationListItemResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<JobApplicationResponse?>
        GetByIdAsync(
            Guid id,
            CancellationToken ct = default)
    {
        var job = await _context.JobApplications
            .Include(x => x.Company)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                ct);

        return job is null
            ? null
            : Map(job);
    }

    public async Task<JobApplicationResponse>
        CreateAsync(
            CreateJobApplicationRequest request,
            CancellationToken ct = default)
    {
        var company = await _context.Companies
            .FirstOrDefaultAsync(
                x => x.Name.ToLower() ==
                     request.CompanyName.ToLower(),
                ct);

        if (company is null)
        {
            company = new Company(
                request.CompanyName,
                request.CompanyWebsite,
                request.SponsorsVisa);

            _context.Companies.Add(company);
        }

        var job = new JobApplication(
            company.Id,
            request.RoleTitle,
            request.WorkMode,
            request.EmploymentType);

        job.UpdateSalary(
            request.SalaryMin,
            request.SalaryMax);

        _context.JobApplications.Add(job);

        await _context.SaveChangesAsync(ct);

        job = await _context.JobApplications
            .Include(x => x.Company)
            .FirstAsync(
                x => x.Id == job.Id,
                ct);

        return Map(job);
    }
    public async Task<JobApplicationResponse?>
        UpdateAsync(
            Guid id,
            UpdateJobApplicationRequest request,
            CancellationToken ct = default)
    {
        var job = await _context.JobApplications
            .Include(x => x.Company)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                ct);

        if (job is null)
        {
            return null;
        }

        job.SetRoleTitle(
            request.RoleTitle);

        job.UpdateSalary(
            request.SalaryMin,
            request.SalaryMax);

        await _context.SaveChangesAsync(ct);

        return Map(job);
    }

    public async Task<JobApplicationResponse?>
        UpdateStatusAsync(
            Guid id,
            UpdateApplicationStatusRequest request,
            CancellationToken ct = default)
    {
        var job = await _context.JobApplications
            .Include(x => x.Company)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                ct);

        if (job is null)
        {
            return null;
        }

        var previousStatus = job.Status;

        job.ChangeStatus(
            request.Status);

        var statusHistory = new ApplicationStatusHistory(
            job.Id,
            previousStatus,
            request.Status);

        _context.ApplicationStatusHistories.Add(statusHistory);

        await _context.SaveChangesAsync(ct);

        return Map(job);
    }
    public async Task<IReadOnlyList<ApplicationStatusHistoryResponse>>
        GetStatusHistoryAsync(
            Guid jobApplicationId,
            CancellationToken ct = default)
    {
        return await _context.ApplicationStatusHistories
            .Where(x => x.JobApplicationId == jobApplicationId)
            .OrderBy(x => x.ChangedAt)
            .Select(x => new ApplicationStatusHistoryResponse
            {
                Id = x.Id,
                FromStatus = x.FromStatus,
                ToStatus = x.ToStatus,
                ChangedAt = x.ChangedAt
            })
            .ToListAsync(ct);
    }
    public async Task<bool>
        DeleteAsync(
            Guid id,
            CancellationToken ct = default)
    {
        var job = await _context.JobApplications
            .FirstOrDefaultAsync(
                x => x.Id == id,
                ct);

        if (job is null)
        {
            return false;
        }

        _context.JobApplications.Remove(job);

        await _context.SaveChangesAsync(ct);

        return true;
    }

    private static JobApplicationResponse Map(
        JobApplication job)
    {
        return new JobApplicationResponse
        {
            Id = job.Id,
            CompanyId = job.CompanyId,
            CompanyName = job.Company.Name,
            CompanyWebsite = job.Company.Website,
            SponsorsVisa = job.Company.SponsorsVisa,
            RoleTitle = job.RoleTitle,
            JobUrl = job.JobUrl,
            SalaryMin = job.SalaryMin,
            SalaryMax = job.SalaryMax,
            WorkMode = job.WorkMode,
            EmploymentType = job.EmploymentType,
            Status = job.Status,
            CreatedAt = job.CreatedAt,
            AppliedAt = job.AppliedAt
        };
    }
}
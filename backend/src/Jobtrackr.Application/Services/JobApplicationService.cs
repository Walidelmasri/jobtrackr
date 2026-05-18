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

    public async Task<IReadOnlyList<JobApplicationListItemResponse>>
        GetAllAsync(CancellationToken ct = default)
    {
        return await _context.JobApplications
            .Include(x => x.Company)
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

        job.ChangeStatus(
            request.Status);

        await _context.SaveChangesAsync(ct);

        return Map(job);
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
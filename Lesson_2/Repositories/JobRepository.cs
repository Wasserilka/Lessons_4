using System.Collections.Generic;
using Lesson_2.Models;
using Lesson_2.Requests;

namespace Lesson_2.Repositories
{
    public interface IJobRepository
    {
        void CreateJob(CreateJobRequest request);
        void DeleteJob(DeleteJobRequest request);
        void AddEmployeeToJob(AddEmployeeToJobRequest request);
        void RemoveEmployeeFromJob(RemoveEmployeeFromJobRequest request);
        List<Job> GetAllJobs();
        Job GetJobById(GetJobByIdRequest request);
    }
    public class JobRepository : IJobRepository
    {
        IDataRepository _data;

        public JobRepository(IDataRepository data)
        {
            _data = data;
        }

        public void AddEmployeeToJob(AddEmployeeToJobRequest request)
        {
            Job _job = null;
            Employee _employee = null;

            foreach (Job item in _data.jobs)
            {
                if (item.Id == request.JobId)
                {
                    _job = item;
                    break;
                }
            }

            foreach (Employee item in _data.employees)
            {
                if (item.Id == request.EmployeeId)
                {
                    _employee = item;
                    break;
                }
            }

            if (_job != null && _employee != null && !_job.Employees.Contains(_employee))
            {
                _job.Employees.Add(_employee);
            }
        }

        public void CreateJob(CreateJobRequest request)
        {
            _data.jobCounter++;
            _data.jobs.Add(new Job 
            { 
                Id = _data.jobCounter, 
                Start = request.Start, 
                End = request.End, 
                PricePerHour = request.PricePerHour, 
                Employees = new List<Employee>() 
            });
        }

        public void DeleteJob(DeleteJobRequest request)
        {
            foreach (Job item in _data.jobs)
            {
                if (item.Id == request.Id)
                {
                    _data.jobs.Remove(item);
                    break;
                }
            }
        }

        public List<Job> GetAllJobs()
        {
            return _data.jobs;
        }

        public Job GetJobById(GetJobByIdRequest request)
        {
            foreach (Job job in _data.jobs)
            {
                if (job.Id == request.Id)
                {
                    return job;
                }
            }

            return null;
        }

        public void RemoveEmployeeFromJob(RemoveEmployeeFromJobRequest request)
        {
            Job _job = null;
            Employee _employee = null;

            foreach (Job item in _data.jobs)
            {
                if (item.Id == request.JobId)
                {
                    _job = item;
                    break;
                }
            }

            foreach (Employee item in _data.employees)
            {
                if (item.Id == request.EmployeeId)
                {
                    _employee = item;
                    break;
                }
            }

            if (_job != null && _employee != null && _job.Employees.Contains(_employee))
            {
                _job.Employees.Remove(_employee);
            }
        }
    }
}

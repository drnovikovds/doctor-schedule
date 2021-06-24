using System;
using System.Collections.Generic;
using Dapper;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DoctorScheduleWebAPI.Models;

namespace DoctorScheduleWebAPI.Repositories

{
    public class DoctorRepository
    {
        private Dictionary<int, Doctor> _doctorDict;
        private const string _connectionString = "Server=DESKTOP-ELI56PS;Initial Catalog=ID_db;Integrated Security=True";


        public DoctorRepository()
        {
            _doctorDict = new Dictionary<int, Doctor>();
        }

        public async Task<Doctor> CreateDoctorAsync(string firstName, string lastName, string speciality)
        {
            var doc = await AddToDbASync(firstName, lastName, speciality);
            _doctorDict.Add(doc.Id, doc);

            return doc;
        }

        private async Task<Doctor> AddToDbASync(string firstName, string lastName, string speciality)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FirstName", firstName);
                parameters.Add("@LastName", lastName);
                parameters.Add("@Speciality", speciality);

                var doc = new Doctor();
                var commandDefinition = new CommandDefinition(StoredProcedures.CreateDoctor, parameters, commandType: System.Data.CommandType.StoredProcedure);

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var reader = await connection.QueryMultipleAsync(commandDefinition))
                    {
                        return (await reader.ReadAsync<Doctor>()).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                // TODO Log errors in file;
                return null;
            }
        }

        public async Task<Doctor> UpdateDoctorByIdAsync(int id, string firstName, string lastName, string speciality)
        {
            var doc = await UpdateInDbAsync(id, firstName, lastName, speciality);

            if (!_doctorDict.ContainsKey(id))
            {
                _doctorDict.Add(id, null);
            }

            _doctorDict[id] = doc;
            return doc;
        }
        private async Task<Doctor> UpdateInDbAsync(int id, string firstName, string lastName, string speciality)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                parameters.Add("@FirstName", firstName);
                parameters.Add("@LastName", lastName);
                parameters.Add("@Speciality", speciality);
                var commandDefinition = new CommandDefinition(StoredProcedures.UpdateDoctor, parameters, commandType: System.Data.CommandType.StoredProcedure);

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var reader = await connection.QueryMultipleAsync(commandDefinition))
                    {
                        return (await reader.ReadAsync<Doctor>()).FirstOrDefault();
                    }
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                // TODO Log errors in file;

                return null;
            }
        }
        public async Task DeleteByIdAsync(int doctorId)
        {
            var result = await DeleteByIdFromDbAsync(doctorId);
            if (result && _doctorDict.ContainsKey(doctorId))
            {
                _doctorDict.Remove(doctorId);
            }
        }

        private async Task<bool> DeleteByIdFromDbAsync(int doctorId)
        {
            try
            {
                const string returnValueName = "return_value";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", doctorId);
                parameters.Add(returnValueName, 0, System.Data.DbType.Int32, System.Data.ParameterDirection.ReturnValue);
                var commandDefinition = new CommandDefinition(StoredProcedures.DeleteDoctorById, parameters, commandType: System.Data.CommandType.StoredProcedure);
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var reader = await connection.QueryMultipleAsync(commandDefinition))
                    {
                        var returnValue = parameters.Get<int>(returnValueName);
                        return returnValue == 1;
                    }
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                // TODO Log errors in file;

                return false;
            }
        }

        public async Task<Doctor> GetByIdAsync(int doctorId)
        {
            if (_doctorDict.ContainsKey(doctorId))
            {
                return _doctorDict[doctorId];
            }

            _doctorDict[doctorId] = await GetByIdFromDbAsync(doctorId);
            return _doctorDict[doctorId];
        }

        private async Task<Doctor> GetByIdFromDbAsync(int doctorId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", doctorId);
                var commandDefinition = new CommandDefinition(StoredProcedures.GetDoctorById, parameters, commandType: System.Data.CommandType.StoredProcedure);
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var reader = await connection.QueryMultipleAsync(commandDefinition))
                    {
                        return (await reader.ReadAsync<Doctor>()).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                // TODO Log errors in file;

                return null;
            }
        }
    }
}

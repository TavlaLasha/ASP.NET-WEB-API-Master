using Lesson_4.EF;
using Lesson_4.Reusables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Lesson_4.Models
{
    public class LecturersModel
    {
        #region Methods
        public LecturersResponse GetLecturers()
        {
            var Result = new LecturersResponse();

            using (var db = new LearningCenterEntities())
            {
                Result.Lecturers = db.Lecturers.Select(i => new LecturerDTO
                {
                    ID = i.ID,
                    LastName = i.LastName,
                    FirstName = i.FirstName,
                    IDNumber = i.IDNumber,
                    BirthDate = i.BirthDate,
                    Degree = i.Degree,
                    Status = i.Status

                }).ToList();
                Result.IsSuccess = true;
            }

            return Result;
        }

        public ApiResponseBase AddLecturer(LecturerDTO SubmitModel)
        {
            var Result = new ApiResponseBase();
            if (SubmitModel != null)
            {
                using (var db = new LearningCenterEntities())
                {
                    Lecturer lecturer = new Lecturer
                    {
                        LastName = SubmitModel.LastName,
                        FirstName = SubmitModel.FirstName,
                        IDNumber = SubmitModel.IDNumber,
                        BirthDate = SubmitModel.BirthDate,
                        Degree = SubmitModel.Degree,
                        Status = SubmitModel.Status
                    };
                    db.Lecturers.Add(lecturer);
                    db.SaveChanges();
                    Result.IsSuccess = true;
                }
            }
            else
            {
                Result.IsSuccess = false;
                Result.ErrorMessage = Constants.ApiResponseMessages.NoDataGiven;
                Result.StatusCode = HttpStatusCode.BadRequest;
            }
            return Result;
        }

        public ApiResponseBase UpdateLecturer(LecturerDTO SubmitModel)
        {
            var Result = new ApiResponseBase();

            if (SubmitModel != null)
            {
                using (var db = new LearningCenterEntities())
                {
                    if (SubmitModel.ID != null && !db.Lecturers.Any(i => i.ID == SubmitModel.ID))
                    {
                        Result.ErrorMessage = Constants.ApiResponseMessages.NotFound;
                        Result.StatusCode = HttpStatusCode.NotFound;
                    }
                    else
                    {
                        var LecturerToUpdate = db.Lecturers.Where(i => i.ID == SubmitModel.ID).First();

                        LecturerToUpdate.LastName = SubmitModel.LastName;
                        LecturerToUpdate.FirstName = SubmitModel.FirstName;
                        LecturerToUpdate.IDNumber = SubmitModel.IDNumber;
                        LecturerToUpdate.BirthDate = SubmitModel.BirthDate;
                        LecturerToUpdate.Degree = SubmitModel.Degree;
                        LecturerToUpdate.Status = SubmitModel.Status;
                        db.SaveChanges();
                        Result.IsSuccess = true;
                    }
                }
            }

            return Result;
        }

        public ApiResponseBase DeleteLecturer(int? LecturerID)
        {
            var Result = new ApiResponseBase();

            if (LecturerID != null)
            {
                using (var db = new LearningCenterEntities())
                {
                    if (!db.Lecturers.Any(i => i.ID == LecturerID))
                    {
                        Result.ErrorMessage = Constants.ApiResponseMessages.NotFound;
                        Result.StatusCode = HttpStatusCode.NotFound;
                    }
                    else
                    {
                        var r = db.Lecturers.Where(i => i.ID == LecturerID).First();
                        db.Lecturers.Remove(r);
                        db.SaveChanges();
                        Result.IsSuccess = true;
                    }
                }
            }

            return Result;
        }
        #endregion

        #region Sub Classes
        public class LecturerDTO
        {
            public int? ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string IDNumber { get; set; }
            public DateTime BirthDate { get; set; }
            public string Degree { get; set; }
            public bool Status { get; set; }
        }

        public class LecturersResponse : ApiResponseBase
        {
            public List<LecturerDTO> Lecturers { get; set; }
        }
        #endregion
    }
}
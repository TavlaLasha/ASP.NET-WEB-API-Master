using Lesson_4.EF;
using Lesson_4.Reusables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Lesson_4.Models
{
    public class SubjectsModel
    {
        #region Methods
        public SubjectsResponse GetSubjects()
        {
            var Result = new SubjectsResponse();

            using (var db = new LearningCenterEntities())
            {
                Result.Subjects = db.Subjects.Select(i => new SubjectDTO
                {
                    ID = i.ID,
                    Name = i.Name,
                    Description = i.Description,
                    Code = i.Code,
                    Credits = i.Credits,
                    Hours = i.Hours

                }).ToList();
                Result.IsSuccess = true;
            }

            return Result;
        }

        public ApiResponseBase AddSubject(SubjectDTO SubmitModel)
        {
            var Result = new ApiResponseBase();
            if (SubmitModel != null)
            {
                using (var db = new LearningCenterEntities())
                {
                    Subject Subject = new Subject
                    {
                        Name = SubmitModel.Name,
                        Description = SubmitModel.Description,
                        Code = SubmitModel.Code,
                        Credits = SubmitModel.Credits,
                        Hours = SubmitModel.Hours
                    };
                    db.Subjects.Add(Subject);
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

        public ApiResponseBase UpdateSubject(SubjectDTO SubmitModel)
        {
            var Result = new ApiResponseBase();

            if (SubmitModel != null)
            {
                using (var db = new LearningCenterEntities())
                {
                    if (SubmitModel.ID != null && !db.Subjects.Any(i => i.ID == SubmitModel.ID))
                    {
                        Result.ErrorMessage = Constants.ApiResponseMessages.NotFound;
                        Result.StatusCode = HttpStatusCode.NotFound;
                    }
                    else
                    {
                        var SubjectToUpdate = db.Subjects.Where(i => i.ID == SubmitModel.ID).First();

                        SubjectToUpdate.Name = SubmitModel.Name;
                        SubjectToUpdate.Description = SubmitModel.Description;
                        SubjectToUpdate.Code = SubmitModel.Code;
                        SubjectToUpdate.Credits = SubmitModel.Credits;
                        SubjectToUpdate.Hours = SubmitModel.Hours;
                        db.SaveChanges();
                        Result.IsSuccess = true;
                    }
                }
            }

            return Result;
        }

        public ApiResponseBase DeleteSubject(int? SubjectID)
        {
            var Result = new ApiResponseBase();

            if (SubjectID != null)
            {
                using (var db = new LearningCenterEntities())
                {
                    if (!db.Subjects.Any(i => i.ID == SubjectID))
                    {
                        Result.ErrorMessage = Constants.ApiResponseMessages.NotFound;
                        Result.StatusCode = HttpStatusCode.NotFound;
                    }
                    else
                    {
                        var r = db.Subjects.Where(i => i.ID == SubjectID).First();
                        db.Subjects.Remove(r);
                        db.SaveChanges();
                        Result.IsSuccess = true;
                    }
                }
            }

            return Result;
        }
        #endregion

        #region Sub Classes
        public class SubjectDTO
        {
            public int? ID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Code { get; set; }
            public int Credits { get; set; }
            public int? Hours { get; set; }
        }

        public class SubjectsResponse : ApiResponseBase
        {
            public List<SubjectDTO> Subjects { get; set; }
        }
        #endregion
    }
}